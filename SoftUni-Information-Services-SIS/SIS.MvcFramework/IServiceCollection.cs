using System;

namespace SIS.MvcFramework
{
    public interface IServiceCollection
    {
                // <Interface, Class>  
        void Add<TSource, TDestination>()
            where TDestination : TSource; /*only successors of the given interface can be TDestination*/

        object CreateInstance(Type type);

        T CreateInstance<T>();
    }
}