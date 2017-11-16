using Inject.Model;
using SimpleInjector;

namespace Inject.SimpleInjector
{
    public static class LifetimeExtensions
    {
        public static Lifestyle ToLifestyle(this Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Scoped:
                    return Lifestyle.Scoped;
                case Lifetime.Singleton:
                    return Lifestyle.Singleton;
                case Lifetime.Default:
                case Lifetime.Transient:
                default:
                    return Lifestyle.Transient;
            }
        }
    }
}