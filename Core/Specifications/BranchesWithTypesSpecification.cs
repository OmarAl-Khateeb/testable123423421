using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class BranchesWithTypesSpecification : BaseSpecification<Branch>
    {
        public BranchesWithTypesSpecification()
        {
            AddInclude(x => x.Province);
        }

        public BranchesWithTypesSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Province);
        }
    }
}