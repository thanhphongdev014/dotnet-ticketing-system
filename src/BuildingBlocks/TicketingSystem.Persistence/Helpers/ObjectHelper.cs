using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace TicketingSystem.Persistence.Helpers;
public static class ObjectHelper
{
    private static readonly ConcurrentDictionary<string, PropertyInfo?> _cachedObjectProperties = new();

    public static void TrySetProperty<TObject, TValue>(
        TObject obj,
        Expression<Func<TObject, TValue>> propertySelector,
        Func<TValue> valueFactory,
        params Type[] ignoreAttributeTypes)
    {
        TrySetProperty(obj, propertySelector, x => valueFactory(), ignoreAttributeTypes);
    }

    public static void TrySetProperty<TObject, TValue>(
        TObject obj,
        Expression<Func<TObject, TValue>> propertySelector,
        Func<TObject, TValue> valueFactory,
        params Type[]? ignoreAttributeTypes)
    {
        var cacheKey =
            $"{obj?.GetType().FullName}-{propertySelector}-{(ignoreAttributeTypes != null ? "-" + string.Join("-", ignoreAttributeTypes.Select(x => x.FullName)) : "")}";

        var property = _cachedObjectProperties.GetOrAdd(cacheKey, PropertyFactory);

        property?.SetValue(obj, valueFactory(obj));
        return;

        PropertyInfo? PropertyFactory(string _)
        {
            MemberExpression? memberExpression;
            switch (propertySelector.Body.NodeType)
            {
                case ExpressionType.Convert:
                    {
                        memberExpression = ((UnaryExpression)propertySelector.Body).Operand as MemberExpression;
                        break;
                    }
                case ExpressionType.MemberAccess:
                    {
                        memberExpression = (MemberExpression)propertySelector.Body;
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }

            if (memberExpression == null)
            {
                return null;
            }

            var propertyInfo = obj?.GetType()
                .GetProperties()
                .FirstOrDefault(x => x.Name == memberExpression.Member.Name);

            if (propertyInfo == null)
            {
                return null;
            }

            var propPrivateSetMethod = propertyInfo.GetSetMethod(true);
            if (propPrivateSetMethod == null)
            {
                return null;
            }

            if (ignoreAttributeTypes != null && ignoreAttributeTypes.Any(ignoreAttribute => propertyInfo.IsDefined(ignoreAttribute, true)))
            {
                return null;
            }

            return propertyInfo;
        }
    }
}
