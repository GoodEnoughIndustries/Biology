namespace GoodEnough.Biology.Systems.Nervous.Peripheral.Autonomic
{
    /// <summary>
    /// Part of the <seealso cref="PeripheralNervousSystem"/> that is involved
    /// with primarily unconscious control of various internal systems.
    /// </summary>
    public class AutonomicNervousSystem : BaseSystem
    {
        public SympatheticNervousSystem SympatheticNervousSystem { get; } = new SympatheticNervousSystem();
        public ParaSympatheticNervousSystem ParaSympatheticNervousSystem { get; } = new ParaSympatheticNervousSystem();
    }
}
