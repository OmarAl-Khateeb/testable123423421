using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class NewsComment : BaseEntity
    {
        public string Comment { get; set; }
        public int NewsId { get; set; }
        public int UserId { get; set; }
    }
}