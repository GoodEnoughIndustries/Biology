using GoodEnough.Biology.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodEnough.Biology.Tissues
{
    public class Tissue : ITissue
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
