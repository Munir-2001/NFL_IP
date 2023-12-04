using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class commentsobj 
    //: Base
    {

        [Key]
        public int Id { get; set; }

        public string name { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public string CommentContent { get; set; }
        public int IPid { get; set; }
    }
}
