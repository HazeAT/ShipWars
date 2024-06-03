using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SemesterProject
{
    public class Job
    {
        public string id { get; set; }
        public string job { get; set; }
        public string planetId { get; set; }
        public string planetName { get; set; }
        public string legalStatus { get; set; }
        public string risk { get; set; }
        public int min_success { get; set; }
        public int max_success { get; set; }
        public int real_success { get; set; }
        public string success_string { get; set; }
        public string description { get; set; }
        public int salary { get; set; }

        public override string ToString()
        {
            return "job: " + job + "\nlegal?: " + legalStatus + "\nrisk: " + risk + "\nsuccess: " + success_string +  "\nplanet: " + planetName + "\nsalary: " + salary;
        }
    }
}
