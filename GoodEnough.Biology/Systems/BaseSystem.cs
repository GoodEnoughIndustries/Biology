using GoodEnough.Biology.Abstractions;
using System;

namespace GoodEnough.Biology.Systems
{
    public class EndocrineSystem : BaseSystem { }

    public class RespiratorySystem : BaseSystem { }

    public class DigestiveSystem : BaseSystem { }

    public class CardiovascularSystem : BaseSystem { }

    public class UrinarySystem : BaseSystem { }

    public class IntegumentarySystem : BaseSystem { }

    public class SkeletalSystem : BaseSystem { }

    public class MuscularSystem : BaseSystem { }

    public class LymphaticSystem : BaseSystem { }

    public class ReproductiveSystem : BaseSystem { }

    public class BaseSystem : ISystem
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
