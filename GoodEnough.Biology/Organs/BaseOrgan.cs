using GoodEnough.Biology.Abstractions;
using System;

namespace GoodEnough.Biology.Organs
{
    public class BaseOrgan : IOrgan
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
