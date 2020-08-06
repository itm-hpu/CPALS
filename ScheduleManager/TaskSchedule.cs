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
        public int TaskID { get; set; }
        public double PickTime { get; set; }
        public double Duration { get; set; }
        public double ConsTime { get; set; }
    }
}
