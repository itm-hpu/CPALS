using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleManager
{
    /// <summary>
    /// Create(Add) Standard Logistics Task
    /// </summary>
    class StandardTask
    {
        public int TaskID { get; set; }
        public double DeliveryTime { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public double Deviation { get; set; }
    }
}
