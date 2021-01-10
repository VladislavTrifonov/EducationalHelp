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
            testLessons.Add(new Lesson() 
            { 
                Id = new Guid("0B5A3D64-394F-4EB0-BD82-D6C9F068BADE"), 
                SubjectId = new Guid("FE3E3D54-E538-47AF-914B-F7B621F395F5"), 
                DateStart = DateTime.Now, 
                DateEnd = DateTime.Now.AddDays(3), 
                Description = "Test", 
                Homework = "nichego", 
                Label = "edu", 
                Notes = "nothing", 
                Title = "test lesson"});

            var testFile = new File()
            {
                Id = new Guid("ABB1F22F-4046-4088-BF0E-983976EA930F"),
                OriginalName = "testfile.txt",
                FullPath = Path.Combine("D:", Path.GetRandomFileName()),
            };

            var lessonFile = new LessonFiles()
            {
                Id = new Guid("70EBE8AF-7070-40A3-9CEA-01F0F2219752"),
                FileId = new Guid("ABB1F22F-4046-4088-BF0E-983976EA930F"),
                LessonId = new Guid("0B5A3D64-394F-4EB0-BD82-D6C9F068BADE"),
                UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D")
            };

            var subjects = new List<Subject>()
            {
                new Subject() { Id = new Guid("FE3E3D54-E538-47AF-914B-F7B621F395F5"), UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), GroupId = new Guid("B5BF64C2-133C-4F06-9478-60EA97166AF6"), Name="Алгоритмы и алгоритмические языки", Teacher="Горюнов Ю.Ю.", Description="алгоритмы"},
                new Subject() { Id = new Guid("7623BD52-72F5-42D1-BF85-11A62D9B19A7"), UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), GroupId = new Guid("B5BF64C2-133C-4F06-9478-60EA97166AF6"), Name="Математический анализ (1 семестр)", Teacher="Васюнина О.Б.", Description="матан"},
                new Subject() { Id = new Guid("30100ECF-741C-4B64-B842-6EAEF3D60B2B"), UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), GroupId = new Guid("B5BF64C2-133C-4F06-9478-60EA97166AF6"), Name="Математический анализ (2 семестр)", Teacher="Хорошева Э.А.", Description="матан"},
                new Subject() { Id = new Guid("8E76B156-CA17-48EC-9D9F-FF9ED066F41D"), UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), GroupId = new Guid("B5BF64C2-133C-4F06-9478-60EA97166AF6"), Name="Программирование (1 семестр)", Teacher="Шибанов С.В.", Description="прогр-е"},
                new Subject() { Id = new Guid("5EE8DB95-8284-40EE-9D36-8E172FD9B1FD"), UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"), GroupId = new Guid("B5BF64C2-133C-4F06-9478-60EA97166AF6"), Name="Программирование (2 семестр)", Teacher="Шибанов С.В.", Description="прогр-е"},
            };

            var users = new List<User>()
            {
                new User() 
                { 
                    Id = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"),
                    Pseudonym = "Жмышенко Валерий",
                    Login = "admin",
                    PassHash = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3"
                },
                new User()
                {
                    Id = new Guid("3EBFBF90-5644-42FE-9CB6-1D0F0ED668BF"),
                    Pseudonym = "Жмышенко Альберт",
                    Login = "batya_admina",
                    PassHash = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3"
                },
                new User()
                {
                    Id = new Guid("E22975CA-C262-48A9-8474-E3D0811FA027"),
                    Pseudonym = "Богданов Евгений",
                    Login = "bogdanov",
                    PassHash = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3"
                }
                
            };

            var groups = new List<Group>()
            {
                new Group()
                {
                    Id = new Guid("B5BF64C2-133C-4F06-9478-60EA97166AF6"),
                    Title = "19ВМ1_ПГУ",
                    Description = "Тестовая группа"
                },
                new Group()
                {
                    Id = new Guid("7B6B70CB-72B4-4508-8978-B8DD2391404F"),
                    Title = "54ГМ2_ГГУ",
                    Description = "Тестовая группа 2"
                }
            };


            var groupUsers = new List<GroupUsers>() 
            {
                new GroupUsers()
                {
                    Id = Guid.NewGuid(),
                    UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"),
                    GroupId = new Guid("B5BF64C2-133C-4F06-9478-60EA97166AF6")
                },
                new GroupUsers()
                {
                    Id = Guid.NewGuid(),
                    UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"),
                    GroupId = new Guid("7B6B70CB-72B4-4508-8978-B8DD2391404F")
                },
                new GroupUsers()
                {
                    Id = Guid.NewGuid(),
                    UserId = new Guid("3EBFBF90-5644-42FE-9CB6-1D0F0ED668BF"),
                    GroupId = new Guid("B5BF64C2-133C-4F06-9478-60EA97166AF6")
                },
                new GroupUsers()
                {
                    Id = Guid.NewGuid(),
                    UserId = new Guid("E22975CA-C262-48A9-8474-E3D0811FA027"),
                    GroupId = new Guid("B5BF64C2-133C-4F06-9478-60EA97166AF6")
                },

            };
                        

            var lessonUser = new LessonUsers()
            {
                Id = Guid.NewGuid(),
                UserId = new Guid("331DF5C7-9FBE-45E4-AF6D-02AFCFCB9C1D"),
                LessonId = new Guid("0B5A3D64-394F-4EB0-BD82-D6C9F068BADE"),
                IsVisited = true,
                Mark = Mark.Good
            };

            builder.Entity<Subject>().HasData(subjects);
            builder.Entity<Lesson>().HasData(testLessons);
            builder.Entity<File>().HasData(testFile);
            builder.Entity<LessonFiles>().HasData(lessonFile);
            builder.Entity<User>().HasData(users);
            builder.Entity<Group>().HasData(groups);
            builder.Entity<GroupUsers>().HasData(groupUsers);
            builder.Entity<LessonUsers>().HasData(lessonUser);
        }
    }
}
