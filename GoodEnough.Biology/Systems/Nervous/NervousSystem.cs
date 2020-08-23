using GoodEnough.Biology.Systems.Nervous.Central;
using GoodEnough.Biology.Systems.Nervous.Peripheral;

namespace GoodEnough.Biology.Systems.Nervous
{
    public class NervousSystem : BaseSystem
    {
        public PeripheralNervousSystem PNS { get; } = new PeripheralNervousSystem();
        public CentralNervousSystem CNS { get; } = new CentralNervousSystem();
    }
}
