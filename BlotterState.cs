using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaewooBot_v2
{
    public class BlotterState
    {
        public DateTime OrderTime { get; set; }
        public string OrderNumber { get; set; }
        public string ShortCode { get; set; }
        public string KrName { get; set; }
        public string OrderType { get; set; }
        public string Type { get; set; }
        public double OrderQty { get; set; }
        public double FilledQty { get; set; }
        public double OrderPrice { get; set; }
        public double FilledPrice { get; set; }

        public BlotterState(DateTime orderTime, 
                            string orderNumber, 
                            string shortCode, 
                            string krName, 
                            string orderType, 
                            string type, 
                            double orderQty, 
                            double filledQty, 
                            double orderPrice, 
                            double filledPrice)
        {
            OrderTime = orderTime;
            OrderNumber = orderNumber;
            ShortCode = shortCode;
            KrName = krName;
            OrderType = orderType;
            Type = type;
            OrderQty = orderQty;
            FilledQty = filledQty;
            OrderPrice = orderPrice;
            FilledPrice = filledPrice;
        }

    }
}
