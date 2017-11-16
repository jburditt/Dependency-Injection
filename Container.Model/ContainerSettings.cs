namespace Inject.Model
{
    public class ContainerSettings
    {
        public ScopedLifetime DefaultScopedLifetime { get; set; }
        public bool DisableVerify { get; set; }
        public bool AllowOverridingRegistrations { get; set; }
    }
}
