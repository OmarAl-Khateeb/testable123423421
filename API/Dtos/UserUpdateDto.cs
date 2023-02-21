using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public int UserTypeId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateBirth { get; set; }
        public int ProvinceId { get; set; }
        public string FullAddress { get; set; }
        public bool IsDelete { get; set; }
    }
}