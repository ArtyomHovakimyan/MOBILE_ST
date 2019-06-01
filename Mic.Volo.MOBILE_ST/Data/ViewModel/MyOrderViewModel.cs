using Mic.Volo.MOBILE_ST.Data.Smod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data.ViewModel
{
    public class MyOrderViewModel
    {
        public OrderSMod OrderPlaceDetails { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlacedTime { get; set; }
        public IEnumerable<MyPhoneOrderInfo> PhoneOrderInfos { get; set; }
    }
    public class MyPhoneOrderInfo
    {
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
    }
}
