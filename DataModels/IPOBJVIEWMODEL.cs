using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class IPOBJVIEWMODEL : Ipobj
    {

        public Ipobj Ipobj { get; set; }
        public List<countries> countries { get; set; }
        public List<classification> classifications { get; set; }
        public List<commentsobj> commentslist { get; set; }

        public commentsobj comments { get; set; }
        public IFormFile image { get; set; }


        // public List<>


    }
}
