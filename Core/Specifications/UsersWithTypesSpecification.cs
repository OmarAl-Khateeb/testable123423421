using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class UsersWithTypesSpecification : BaseSpecification<User>
    {
        public UsersWithTypesSpecification()
        {
            AddInclude(x => x.UserType);
            AddInclude(x => x.Province);
        }

        public UsersWithTypesSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.UserType);
            AddInclude(x => x.Province);
        }
    }
}