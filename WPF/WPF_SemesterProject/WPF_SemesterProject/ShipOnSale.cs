using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SemesterProject
{
    public class ShipOnSale
    {
        public string id { get; set; }
        public Ship_DTO ship { get; set; }
        public string userID { get; set; }
        public string price { get; set; }
        public string created { get; set; }
        public string expired { get; set; }

        public override string ToString()
        {
            return "ship: \n" + ship.OnlineShipToString() + "\nuser: " + userID + "\nprice: " + price + "\ncreated: " + created + "\nexpired: " + expired;
        }
    }
}
