using Container.Model;

namespace Container
{
    public static class ContainerFactory
    {
        public static IContainer<T> Create<T>() where T : IContainer<T>, new()
        {
            return new Container<T>().Current;
        }
    }
}