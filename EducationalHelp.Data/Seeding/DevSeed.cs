using EducationalHelp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Data.Seeding
{
    internal static class DevSeed
    {
        public static void Seed(ModelBuilder builder)
        {
            var subjects = new List<Subject>()
            {
                new Subject() { Id = new Guid("FE3E3D54-E538-47AF-914B-F7B621F395F5"), Name="Алгоритмы и алгоритмические языки", Teacher="Горюнов Ю.Ю.", Description="алгоритмы"},
                new Subject() { Id = new Guid("7623BD52-72F5-42D1-BF85-11A62D9B19A7"), Name="Математический анализ (1 семестр)", Teacher="Васюнина О.Б.", Description="матан"},
                new Subject() { Id = new Guid("30100ECF-741C-4B64-B842-6EAEF3D60B2B"), Name="Математический анализ (2 семестр)", Teacher="Хорошева Э.А.", Description="матан"},
                new Subject() { Id = new Guid("8E76B156-CA17-48EC-9D9F-FF9ED066F41D"), Name="Программирование (1 семестр)", Teacher="Шибанов С.В.", Description="прогр-е"},
                new Subject() { Id = new Guid("5EE8DB95-8284-40EE-9D36-8E172FD9B1FD"), Name="Программирование (2 семестр)", Teacher="Шибанов С.В.", Description="прогр-е"},
            };

            builder.Entity<Subject>().HasData(subjects);
            
        }
    }
}
