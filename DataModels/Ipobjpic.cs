using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Ipobjpic
    {
        public Ipobj ipobject { get; set;  }
        public IFormFile image { get; set; }
    }
}
