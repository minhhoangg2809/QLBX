using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBX.Models.Custom
{
    public class SpelldutyCustom : Spellduty
    {
        private string _Date;

        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        private string _TimeSt;

        public string TimeSt
        {
            get { return _TimeSt; }
            set { _TimeSt = value; }
        }

        private string _TimeEn;

        public string TimeEn
        {
            get { return _TimeEn; }
            set { _TimeEn = value; }
        }

        public SpelldutyCustom()
        {
            //
        }

        public static SpelldutyCustom MapSpellCus(Spellduty sp)
        {
            SpelldutyCustom spCus = new SpelldutyCustom();
            spCus.id = sp.id;
            spCus.startTime = sp.startTime;
            spCus.endTime = sp.endTime;
            spCus.TimeSt = sp.startTime.Value.ToShortTimeString();
            spCus.TimeEn = sp.endTime.Value.ToShortTimeString();
            spCus.Date = sp.startTime.Value.ToShortDateString();
            return spCus;
        }

        public static Spellduty MapSpell(SpelldutyCustom spCus)
        {
            Spellduty sp = new Spellduty();
            sp.id = spCus.id;
            sp.startTime = Resource.MyStaticMethods.ConvertTimeSpell(spCus.TimeSt);
            sp.endTime = Resource.MyStaticMethods.ConvertTimeSpell(spCus.TimeEn);
            return sp;
        }
    }
}
