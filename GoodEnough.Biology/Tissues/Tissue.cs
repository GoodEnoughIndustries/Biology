using GoodEnough.Biology.Abstractions;
using System;

namespace GoodEnough.Biology.Tissues
{
    public class Tissue : ITissue
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
