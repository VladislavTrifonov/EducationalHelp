using EducationalHelp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Services.Calendars
{
    public class CalendarEvent
    {
        public string Summary { get; set; }

        public string Description { get; set; }
        
        public DateTime DateStart { get; set; }
       
        public DateTime DateEnd { get; set; }
        
        public BaseEntity EventObject { get; set; }

        public string Type { get; set; }
    }
}
