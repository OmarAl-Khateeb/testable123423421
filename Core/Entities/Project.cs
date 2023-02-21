using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Project : BaseEntity
    {
        public string Caption { get; set; }
        public string ImageUrl { get; set; }
    }
}