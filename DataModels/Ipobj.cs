using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Ipobj : Base
    {
        public string IPListname { get; set; }
        public string entityname { get; set; }

        public string logos { get; set; }
        public string status { get; set; }
        public string entity { get; set; }
        public string country { get; set; }
        public string lawyer { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        [DataType(DataType.Date)]
        public DateTime renewaldate { get; set; }
        public int cost { get; set; }
        public string classification { get; set; }

    }
}
