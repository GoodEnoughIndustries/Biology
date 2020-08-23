using System;

namespace GoodEnough.Biology.Abstractions
{
    /// <summary>
    /// All types inherit from <see cref="IComponent"/>.
    /// </summary>
    public interface IComponent : IEquatable<IComponent>
    {
        Guid Id { get; }

        bool IEquatable<IComponent>.Equals(IComponent? other)
            => this.Id.Equals(other?.Id);
    }
}
