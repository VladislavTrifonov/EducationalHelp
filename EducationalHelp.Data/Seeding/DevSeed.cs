using File = EducationalHelp.Core.Entities.File;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EducationalHelp.Core.Entities;

namespace EducationalHelp.Data.Seeding
{
    internal static class DevSeed
    {
        public static void Seed(ModelBuilder builder)
        {
            var testLessons = new List<Lesson>();
            testLessons.Add(new Lesson() { Id = new Guid("0B5A3D64-394F-4EB0-BD82-D6C9F068BADE"), UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), SubjectId = new Guid("FE3E3D54-E538-47AF-914B-F7B621F395F5"), DateStart = DateTime.Now, DateEnd = DateTime.Now.AddDays(3), Description = "Test", Homework = "nichego", IsVisited = false, Label = "edu", Notes = "nothing", SelfMark = Mark.Satisfactory, Title = "test lesson"});

            var testFile = new File()
            {
                Id = new Guid("ABB1F22F-4046-4088-BF0E-983976EA930F"),
                OriginalName = "testfile.txt",
                FullPath = Path.Combine("D:", Path.GetRandomFileName()),
                UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D")
            };

            var lessonFile = new LessonFiles()
            {
                Id = new Guid("70EBE8AF-7070-40A3-9CEA-01F0F2219752"),
                FileId = new Guid("ABB1F22F-4046-4088-BF0E-983976EA930F"),
                LessonId = new Guid("0B5A3D64-394F-4EB0-BD82-D6C9F068BADE")
            };

            var subjects = new List<Subject>()
            {
                new Subject() { Id = new Guid("FE3E3D54-E538-47AF-914B-F7B621F395F5"), UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), Name="Алгоритмы и алгоритмические языки", Teacher="Горюнов Ю.Ю.", Description="алгоритмы"},
                new Subject() { Id = new Guid("7623BD52-72F5-42D1-BF85-11A62D9B19A7"), UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), Name="Математический анализ (1 семестр)", Teacher="Васюнина О.Б.", Description="матан"},
                new Subject() { Id = new Guid("30100ECF-741C-4B64-B842-6EAEF3D60B2B"), UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), Name="Математический анализ (2 семестр)", Teacher="Хорошева Э.А.", Description="матан"},
                new Subject() { Id = new Guid("8E76B156-CA17-48EC-9D9F-FF9ED066F41D"), UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), Name="Программирование (1 семестр)", Teacher="Шибанов С.В.", Description="прогр-е"},
                new Subject() { Id = new Guid("5EE8DB95-8284-40EE-9D36-8E172FD9B1FD"), UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), Name="Программирование (2 семестр)", Teacher="Шибанов С.В.", Description="прогр-е"},
            };

            var users = new List<User>()
            {
                new User() { Id = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), Pseudonym = "Жмышенко Валерий", Login = "admin", PassHash = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3" }
            };

            builder.Entity<Subject>().HasData(subjects);
            builder.Entity<Lesson>().HasData(testLessons);
            builder.Entity<File>().HasData(testFile);
            builder.Entity<LessonFiles>().HasData(lessonFile);
            builder.Entity<User>().HasData(users);


        }
    }
}
