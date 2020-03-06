using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBX.Models.Custom
{

    public class Report : CheckInOut
    {
        private double _Time;

        public double Time
        {
            get
            {
                if (checkOutTime != null)
                {
                    return Math.Round((checkOutTime.Value.Subtract(checkInTime.Value).TotalHours),2);
                }
                return 0;
            }
            set { _Time = value; }
        }

        public Report()
        {
            //
        }
    }
}

