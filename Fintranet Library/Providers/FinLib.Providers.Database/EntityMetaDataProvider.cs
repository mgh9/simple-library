using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using FinLib.Models.Base;

namespace FinLib.Providers.Database
{
    public static class EntityMetaDataProvider
    {
        public static List<Column> GetColumns(Type type)
        {
            type.ThrowIfNull();

            var columns = new List<Column>();
            foreach (var prop in type.GetProperties())
            {
                var propName = prop.Name;
                var firstChar = char.ToLowerInvariant(propName[0]);

                propName = firstChar + propName.Substring(1, propName.Length - 1);

                foreach (var viewColumn in prop.GetCustomAttributes(false).OfType<ViewColumnAttribute>())
                {
                    var filter = viewColumn.Filter;

                    if (filter.IsEmpty())
                    {
                        if (prop.PropertyType == typeof(bool))
                        {
                            filter = "toActiveDeactive";
                        }
                        else if (prop.PropertyType.IsEnum)
                        {
                            filter = $"toEnumString,{prop.PropertyType.Name}";
                        }
                    }

                    columns.Add(new Column()
                    {
                        Title = viewColumn.Title,
                        OrderNumber = viewColumn.OrderNumber != -99 ? viewColumn.OrderNumber : (columns.Count > 0 ? columns.Max(item => item.OrderNumber) + 1 : 0),
                        Field = propName,
                        Filter = filter,
                    });
                }
            }

            if (columns.Count == 0)
            {
                throw new InvalidImplementationException("There are no columns to view");
            }

            return columns.OrderBy(item => item.OrderNumber).ToList();
        }
    }
}
