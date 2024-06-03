using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SemesterProject
{
    public class Ship_DTO
    {
        public string id { get; set; }
        public int amount { get; set; }
        public string model { get; set; }
        public string value {  get; set; }
        public int health { get; set; }
        public string rented { get; set; }

        public override string ToString()
        {
            return " - model: " + model + "\n - value: " + value + "\n - amount: " + amount + "\n - health: " + health + "\n - rented: " + rented;
        }

        public string OnlineShipToString()
        {
            string s = " - model: " + model + (value != "0" ? "\n - value: " + value : "\n - rented: true") + "\n - amount: " + amount + "\n - health: " + health + "\n";
            return s;
        }


    }
}
