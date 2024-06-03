using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SemesterProject
{
    public class Planet
    {
        public string id { get; set; }
        public string diameter { get; set; }
        public string rotation_period { get; set; }
        public string orbital_period { get; set; }
        public string gravity { get; set; }
        public string population { get; set; }
        public string climate { get; set; }
        public string terrain { get; set; }
        public string surface_water { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return "name: " + name + "\ndiameter: " + diameter + "\nsurface water: " + surface_water + "\nterrain: " + terrain + "\nclimate: " + climate + "\ngravity: " + gravity + "\nrotation period: " + rotation_period + "\norbital period: " + orbital_period;
        }
    }
}
