using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class NewsLikes : BaseEntity
    {
        public string Like { get; set; } //what is this
        public string Type { get; set; } //what?
        public int NewsId { get; set; }
        public int UserId { get; set; }
    }
}