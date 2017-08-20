using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

namespace MvcBasic.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public int? RecCd { get; set; }
        public int? KbnCd { get; set; }

        //public IEnumerable<string> SettingH1 { get; set; }
        //public IEnumerable<string> SettingI1 { get; set; }
        //public IEnumerable<string> SettingI2 { get; set; }

        public IEnumerable<dynamic> SettingH1 { get; set; }
        public IEnumerable<dynamic> SettingI1 { get; set; }
        public IEnumerable<dynamic> SettingI2 { get; set; }
        public IEnumerable<dynamic> dataH1_I1_I2 { get; set; }


    }
}