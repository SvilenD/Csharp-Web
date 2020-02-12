using System;

namespace SIS.MvcFramework
{
    public interface IServiceCollection
    {
                // <Interface, Class>
        void Add<TSource, TDestination>()
            where TDestination : TSource;

        object CreateInstance(Type type);

        T CreateInstance<T>();
    }
}