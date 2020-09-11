using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleManager
{
    /// <summary>
    /// Create Logistics Schedule
    /// </summary>
    class TaskSchedule
    {
        public string ScheduleID { get; set; }
        public string TaskName { get; set; }
        public string TaskType { get; set; }

        public double PickTime { get; set; }
        public double Duration { get; set; }
        public double ConsTime { get; set; }
        
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
    }
}
