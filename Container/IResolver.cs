namespace Container
{
    public interface IResolver
    {
        T ResolveInstance<T>() where T : class;
    }
}