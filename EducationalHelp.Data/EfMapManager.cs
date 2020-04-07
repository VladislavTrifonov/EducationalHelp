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
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDataMapper<>)).Count() != 0);

            foreach (var type in types)
            {
                var t = Activator.CreateInstance(type);
                var type_t = t.GetType();
                
                // IDataMapper<T> - it variable saves T
                var genericParameterInInterface = type_t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDataMapper<>)).First().GenericTypeArguments.First();
                
                // Call builder.Entity<T>(), result id EntityTypeBuilder<T>
                var typeBuilder = builder.GetType().GetMethods().Where(t => t.Name == nameof(builder.Entity) && t.IsGenericMethod).First()
                    .MakeGenericMethod(genericParameterInInterface).Invoke(builder, null);

                // Call Map(EntityTypeBuilder<T>) method
                type_t.GetMethod("Map").Invoke(t, new object[] { typeBuilder });
            }
        }
    }
}
