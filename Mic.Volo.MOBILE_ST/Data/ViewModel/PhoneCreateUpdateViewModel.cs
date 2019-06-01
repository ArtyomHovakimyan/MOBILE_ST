using Mic.Volo.MOBILE_ST.Data.Models;
using Mic.Volo.MOBILE_ST.Data.Smod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data.ViewModel
{
    public class PhoneCreateUpdateViewModel
    {
        public IEnumerable<Company> Companies { get; set; }
        public PhoneSMod PhoneSMod { get; set; }

        public PhoneCreateUpdateViewModel()
        {
            Companies = new List<Company>();
        }
    }
}
