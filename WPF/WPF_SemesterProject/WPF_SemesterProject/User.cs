using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SemesterProject
{
    public class User
    {
        public string username { get; set; }
        public int level { get; set; }
        public string role { get; set; }
        public long credits { get; set; }
        public List<UserID_DTO> friends { get; set; }
        public List<Ship_DTO> ownedships { get; set; }

        public Job currentJob { get; set; }
    }
}
