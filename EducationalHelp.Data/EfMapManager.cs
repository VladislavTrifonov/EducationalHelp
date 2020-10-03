using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using EducationalHelp.Data.Mapping;

namespace EducationalHelp.Data
{
    internal class EfMapManager
    {
        public static void MappingAllData(ModelBuilder builder)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(DataMapper<>));

            foreach (var type in types)
            {
                var t = Activator.CreateInstance(type);
                var type_t = t.GetType();
                
                // DataMapper<T> - it variable saves T
                var genericParameterInInterface = type_t.BaseType.GenericTypeArguments.First();
                
                // Call builder.Entity<T>(), result id EntityTypeBuilder<T>
                var typeBuilder = builder.GetType().GetMethods().First(t => t.Name == nameof(builder.Entity) && t.IsGenericMethod)
                    .MakeGenericMethod(genericParameterInInterface).Invoke(builder, null);

                // Call Map(EntityTypeBuilder<T>) method
                type_t.GetMethod("Map").Invoke(t, new object[] { typeBuilder });
            }
        }
    }
}
