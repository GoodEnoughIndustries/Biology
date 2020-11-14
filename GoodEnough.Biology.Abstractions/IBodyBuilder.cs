using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodEnough.Biology.Abstractions
{
    public interface IBodyBuilder
    {
        IBodyPlan Build();
    }
}
