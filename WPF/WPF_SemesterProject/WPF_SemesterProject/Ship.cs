using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WPF_SemesterProject
{
    public class Ship
    {
        public string id { get; set; }
        public string model { get; set; }
        public string starship_class { get; set; }
        public string manufacturer { get; set; }
        public string cost_in_credits { get; set; }
        public string length { get; set; }
        public string crew { get; set; }
        public string passengers { get; set; }
        public string max_atmosphering_speed { get; set; }
        public string hyperdrive_rating { get; set; }
        public string MGLT { get; set; }
        public string cargo_capacity { get; set; }
        public string consumables { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return "name: " + name + "\nmodel: " + model + "\nclass: " + starship_class + "\nmanufacturer: " + manufacturer + "\ncosts: " + cost_in_credits + "\nlength: " + length + "\ncrew: " + crew + "\npassengers: " + passengers + "\nmax speed: " + max_atmosphering_speed + "\nhyperdrive-rating: " + hyperdrive_rating + "\nMGLT: " + MGLT + "\ncargo-capacity: " + cargo_capacity + "\nconsumeables: " + consumables;
        }
    }
}
