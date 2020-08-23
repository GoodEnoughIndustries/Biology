namespace GoodEnough.Biology.Systems.Nervous.Peripheral
{
    public class PeripheralNervousSystem : BaseSystem
    {
        public SomaticNervousSystem SomaticNervousSystem { get; } = new SomaticNervousSystem();
        public AutonomicNervousSystem AutonomicNervousSystem { get; } = new AutonomicNervousSystem();
    }
}
