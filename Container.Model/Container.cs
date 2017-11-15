namespace Container.Model
{
    public class Container<T> where T : IContainer<T>, new()
    {
        public T Current { get; set; }

        public Container()
        {
            Current = new T();
        }

        public void Configure(ContainerSettings settings)
        {
            Current.RegisterDependencies(settings);
        }

        public Container<T> PostRegister()
        {
            Current.PostRegister();

            return this;
        }

        public Container<T> Verify()
        {
            Current.VerifyContainer();

            return this;
        }
    }
}