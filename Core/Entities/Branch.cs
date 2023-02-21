using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PhoneNumber1 { get; set; }
        public int PhoneNumber2 { get; set; }
        public int Long { get; set; }
        public int Lat { get; set; }
        public Province Province { get; set; }
        public int ProvinceId { get; set; }
        public string FullAddress { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}