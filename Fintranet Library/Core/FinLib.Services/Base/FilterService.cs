using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Enums;
using System.Linq.Dynamic.Core;
using ValueType = FinLib.Models.Enums.ValueType;

namespace FinLib.Services.Base
{
    internal static class FilterService
    {
        internal static IQueryable<T> ParseFilter<T, TSearchFilter>(IQueryable<T> query, TSearchFilter searchFilterModel)
            where T : class, new()
            where TSearchFilter : BaseEntitySearchFilter, new()
        {
            query.ThrowIfNull();

            searchFilterModel.ThrowIfNull();
            
            var itsProperties = searchFilterModel.GetType().GetProperties();
            foreach (var theProperty in itsProperties)
            {
                var theSearchFilterItemName = theProperty.Name;

                var thePropertyType = theProperty.PropertyType;
                var theSearchFilterItemObject = Common.Helpers.TypeHelper.Cast(theProperty.GetValue(searchFilterModel), thePropertyType);

                // اگه نال هست >>>> ینی درست اینیشالایز نشده سمت کلاینت یا سرور-ساید
                if (theSearchFilterItemObject == null)
                {
                    throw new InvalidImplementationException("SearchFilterItem cannot be null : " + theSearchFilterItemName);
                }

#pragma warning disable S1125 // Boolean literals should not be redundant
                // It's a Dyanmic type, so "== true" should be here explicitly
                if (theSearchFilterItemObject.IsIgnore == true)
#pragma warning restore S1125 // Boolean literals should not be redundant
                    continue;

                // اگه نام ستون مجزا از نام خود پراپرتی تعیین نشه، . 
                // از نام خود پراپرتی استفاده میشه در شرط
                if (!string.IsNullOrWhiteSpace(theSearchFilterItemObject.ColumnName))
                {
                    theSearchFilterItemName = theSearchFilterItemObject.ColumnName;
                }

                // get its valueType
                ValueType? itsValueType = getFilterItemType(theSearchFilterItemObject.Value);
                if (itsValueType == null)
                    continue;

                // if has NOT a valid value, continue
                if (!hasValidSearchFilterItemValue(theSearchFilterItemObject, itsValueType.Value))
                    continue;

                // validation
                validateSearchFilterItem(theSearchFilterItemName, theSearchFilterItemObject);

                // making query
                switch (theSearchFilterItemObject.ConditionType)
                {
                    case ConditionType.Equals:
                        if (itsValueType == ValueType.Enum)
                        {
                            var theEnumValue = (int)Convert.ToInt32(theSearchFilterItemObject.Value);
                            query = query.Where($"{theSearchFilterItemName} == @0", theEnumValue);
                        }
                        else
                        {
                            query = query.Where($"{theSearchFilterItemName} == {theSearchFilterItemObject.Value}");
                        }
                        break;

                    case ConditionType.NumericContains:
                        {
                            var theNumberValue = (string)Convert.ToString(theSearchFilterItemObject.Value);
                            query = query.Where($"@0.Contains({theSearchFilterItemName}.ToString())", theNumberValue);
                            break;
                        }

                    case ConditionType.Contains:
                        {
                            query = query.Where($"{theSearchFilterItemName}.Contains(\"{theSearchFilterItemObject.Value}\")");
                            break;
                        }

                    case ConditionType.LessThan:
                    case ConditionType.GreatherThan:
                        {
                            string lessThanGreatherThanOperator =
                                    theSearchFilterItemObject.ConditionType == ConditionType.LessThan
                                    ? "<="
                                    : ">=";

                            switch (itsValueType)
                            {
                                case ValueType.Number:
                                case ValueType.DecimalNumber:
                                    var theNumberValue = (string)theSearchFilterItemObject.Value;
                                    query = query.Where($"{theSearchFilterItemName} {lessThanGreatherThanOperator} @0", theNumberValue);
                                    break;

                                case ValueType.Date:
                                    var theDateValue = (DateTime)theSearchFilterItemObject.Value;
                                    query = query.Where($"{theSearchFilterItemName} {lessThanGreatherThanOperator} @0", theDateValue);
                                    break;

                                case ValueType.Enum:
                                    var theEnumValue = (int)theSearchFilterItemObject.Value;
                                    query = query.Where($"{theSearchFilterItemName} {lessThanGreatherThanOperator} @0", theEnumValue);
                                    break;

                                case ValueType.Text:
                                    var theStringValue = (string)theSearchFilterItemObject.Value;
                                    query = query.Where($"{theSearchFilterItemName} {lessThanGreatherThanOperator} @0", theStringValue);
                                    break;

                                case ValueType.Boolean:
                                case ValueType.List:
                                default:
                                    {
                                        throw new InvalidModelException("SearchFilter Parsing failed. Invalid operator for the value type: " + itsValueType);
                                    }
                            }

                            break;
                        }

                    case ConditionType.Between:
                        {
                            var theValue = (string)theSearchFilterItemObject.Value;
                            var theValue2 = (string)theSearchFilterItemObject.Value2;
                            query = query.Where($"{theSearchFilterItemName} >= @0 && {theSearchFilterItemName} <= @1", theValue, theValue2);
                            break;
                        }

                    default:
                        {
                            throw new InvalidModelException(nameof(theSearchFilterItemObject.ConditionType));
                        }
                }
            }

            return query;
        }

        private static ValueType? getFilterItemType(dynamic value)
        {
            if (value is null)
                return null;

            if (value.GetType().FullName.ToUpper() == "SYSTEM.STRING")
                return ValueType.Text;

            var itsBaseType = value.GetType().BaseType.FullName.ToUpper();
            if (itsBaseType == "SYSTEM.ENUM")
            {
                return ValueType.Enum;
            }
            else if (itsBaseType != "SYSTEM.VALUETYPE")
            {
                throw new InvalidModelException("Invalid SearchFilterItem base value type! " + itsBaseType);
            }

            //           
            ValueType retval;
            var itsType = value.GetType().Name.ToUpper();

            switch (itsType)
            {
                case "INT16":
                case "INT32":
                case "INT64":
                    retval = ValueType.Number;
                    break;

                case "DOUBLE":
                case "FLOAT":
                case "SINGLE":
                    retval = ValueType.DecimalNumber;
                    break;

                case "CHAR":
                case "STRING":
                    retval = ValueType.Text;
                    break;

                case "BOOLEAN":
                    retval = ValueType.Boolean;
                    break;

                case "DATETIME":
                    retval = ValueType.Date;
                    break;

                default:
                    {
                        throw new InvalidModelException("Invalid SearchFilterItem value type! " + itsType);
                    }
            }

            return retval;
        }

        private static void validateSearchFilterItem(string searchFilterItemName, dynamic searchFilterItemObject)
        {
            if (searchFilterItemObject.ConditionType == ConditionType.Between)
            {
                // age k Between has, bayad hatman Value2 meghdar dashte bashe
                if (searchFilterItemObject.Value2 == null)
                {
                    throw new NullModelException("SearchFilterItem.Value2 is null for the " + searchFilterItemName);
                }
                else
                {
                    // ..
                }
            }
        }

        private static bool hasValidSearchFilterItemValue(dynamic searchFilterItemObject, ValueType valueType)
        {
            if (searchFilterItemObject is null)
                throw new NullModelException(nameof(searchFilterItemObject));

            switch (valueType)
            {
                case ValueType.Text:
                    if (string.IsNullOrWhiteSpace(searchFilterItemObject.Value))
                        return false;
                    break;

                case ValueType.Number:
                case ValueType.DecimalNumber:
                    if (searchFilterItemObject.Value == null || searchFilterItemObject.Value <= -1)
                        return false;
                    break;

                case ValueType.Enum:
                    if (searchFilterItemObject.Value == null)
                        return false;

                    if (searchFilterItemObject.Value?.ToString() == "-1")
                        return false;
                    break;

                case ValueType.List:
                case ValueType.Date:
                case ValueType.Boolean:
                    if (searchFilterItemObject.Value == null)
                        return false;

                    break;

                default:
                    {
                        throw new InvalidModelException("Invalid SearchFilterItem valueType : " + valueType);
                    }
            }

            return true;
        }
    }
}
