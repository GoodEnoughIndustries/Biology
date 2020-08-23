using GoodEnough.Biology.Abstractions;
using System;

namespace GoodEnough.Biology.Cells
{
    public class Cell : ICell
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
