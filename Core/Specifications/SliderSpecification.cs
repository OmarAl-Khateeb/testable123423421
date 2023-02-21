using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class SliderSpecification : BaseSpecification<Slider>
    {
        public SliderSpecification()
        {
        }

        public SliderSpecification(int id) : base(x => x.Id == id)
        {
        }
    }
}