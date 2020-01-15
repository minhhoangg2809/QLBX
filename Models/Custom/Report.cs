using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBX.Models.Custom
{

    public class Report : CheckInOut
    {
        private int _Time;

        public int Time
        {
            get
            {
                if (checkOutTime != null)
                {
                    return (Convert.ToInt32(checkOutTime.Value.Hour)) - (Convert.ToInt32(checkInTime.Value.Hour));
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

