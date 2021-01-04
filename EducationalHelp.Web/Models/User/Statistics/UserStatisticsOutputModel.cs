using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Models.User.Statistics
{
    public class UserStatisticsOutputModel
    {
        public int SubjectsCount { get; set; }
        public double AvgMarkLessonAll { get; set; }

        public IEnumerable<UserStatisticsSubject> subjectsStatistics { get; set; }
    }

    public class UserStatisticsSubject
    {
        public string SubjectTitle { get; set; }
        public int LessonsCount { get; set; }
        public int LessonsMissedCount { get; set; }
        public double AvgLessonsMark { get; set; }
    }
}
