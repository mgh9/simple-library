using AutoMapper;
using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using FinLib.DomainClasses.Base;
using FinLib.Models.Base;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace FinLib.Mappings
{
    public static class Extensions
    {
        public static T MapTo<T>(this object entity)
        {
            return MapperHelper.MapTo<T>(entity);
        }

        public static T MapTo<T>(this ExpandoObject entity)
        {
            return MapperHelper.MapTo<T>(entity);
        }

        public static List<TDestination> ProjectToList<TDestination>(this IQueryable<object> source)
        {
            return MapTo<List<TDestination>>(source);
        }

        public static TDestination ProjectToSingleOrDefault<TDestination>(this IQueryable<object> source)
        {
            return ProjectToList<TDestination>(source).SingleOrDefault();
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, string columnName)
        {
            return OrderBy(source.AsQueryable(), columnName);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string columnName)
        {
            if (columnName.IsEmpty())
                columnName = "Id";

            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            PropertyInfo property;
            Expression propertyAccess;

            bool isAscending = true;
            columnName.ThrowIfNull();

            var orderBy_Parts = columnName.Split(' ');
            if (orderBy_Parts.Length > 1)
            {
                if (string.Equals(orderBy_Parts[1], "ASC", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    // ok
                }
                else if (string.Equals(orderBy_Parts[1], "DESC", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    isAscending = false;
                }
                else
                {
                    throw new InvalidModelException(nameof(columnName) + " doesnt have ASC or DESC keyword");
                }

                columnName = orderBy_Parts[0];
            }

            columnName = columnName.Substring(0, 1).ToUpper() + columnName[1..];

            if (columnName.Contains('.'))
            {
                // support to be sorted on child fields.
                string[] childProperties = columnName.Split('.');
                var strProp = childProperties[0];

                property = type.GetProperty(strProp);

                if (property == null && strProp.ToUpperInvariant() == "ID")
                {
                    property = type.GetProperty(childProperties[0]);
                }

                if (property == null)
                {
                    throw new InvalidSortException($"Property of '{strProp}' cannot be null");
                }

                propertyAccess = Expression.MakeMemberAccess(parameter, property);
                for (int i = 1; i < childProperties.Length; i++)
                {
                    property = property.PropertyType.GetProperty(childProperties[i]);
                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                var strProp = columnName;
                property = typeof(T).GetProperty(strProp);

                if (property == null)
                {
                    if (strProp.ToUpperInvariant() == "ID")
                    {
                        strProp = "Id";
                        property = type.GetProperty(strProp);
                    }
                    else
                    {
                        throw new InvalidSortException("امکان مرتب سازی بر اساس این ستون وجود ندارد! : " + strProp);
                    }
                }

                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }

            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable),
                                                             isAscending ? "OrderBy" : "OrderByDescending",
                                                             new[]
                                                             {
                                                                 type,
                                                                 property.PropertyType
                                                             },
                                                             source.Expression,
                                                             Expression.Quote(orderByExp));

            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static List<TitleValue<int>> ProjectEntityToTitleValueList<TSource>(this List<TSource> list)
            where TSource : IBaseEntity, new()
        {
            return list.Select(item => new TitleValue<int>()
            {
                Value = item.Id,
                Title = item.ToString(),
            }).ToList();
        }

        public static List<TitleValue<int>> ProjectDtoToTitleValueList<TSource>(this List<TSource> list)
            where TSource : Models.Base.Dto.BaseEntityDto, new()
        {
            return list.Select(item => new TitleValue<int>()
            {
                Value = item.Id,
                Title = item.ToString(),
            }).ToList();
        }

        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
                            (this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }

            return expression;
        }
    }
}
