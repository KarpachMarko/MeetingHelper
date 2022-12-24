using System.Collections;
using Base.Contracts.Domain;
using Base.Domain;

namespace Base.Extensions;

public static class MultiLangStringExtension
{
    public static void UpdateWithTranslations(this IDomainEntityId entityFromDb, IDomainEntityId updatedEntity) =>
        UpdateWithTranslations<Guid>(entityFromDb, updatedEntity);

    public static void UpdateWithTranslations<TKey>(this IDomainEntityId<TKey> entityFromDb,
        IDomainEntityId<TKey> updatedEntity)
        where TKey : IEquatable<TKey>
    {
        foreach (var propertyInfo in updatedEntity.GetType().GetProperties())
        {
            var entityFromDbProp = entityFromDb.GetType().GetProperty(propertyInfo.Name);

            if (entityFromDbProp == null) continue;

            if (entityFromDbProp.PropertyType == typeof(MultiLangString))
            {
                var value = propertyInfo.GetValue(updatedEntity)!.ToString()!;

                ((MultiLangString) entityFromDbProp.GetValue(entityFromDb)!).SetTranslation(value);
            }
            else
            {
                if (entityFromDbProp.GetSetMethod() == null && propertyInfo.GetSetMethod(true) == null) continue;
                if (entityFromDbProp.PropertyType != typeof(string) &&
                    typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType)) continue;
                if (entityFromDbProp.Name == "ConcurrencyStamp") continue;

                var value = propertyInfo.GetValue(updatedEntity);
                entityFromDbProp.SetValue(entityFromDb, value);
            }
        }
    }
}