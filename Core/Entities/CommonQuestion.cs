using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CommonQuestion : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}