using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestGame.Services
{
    public interface IServiceProvider { }

    public interface IServiceCollection
    {
        public void Add<T>(object[] args = null) where T : IServiceProvider;
        public void Remove<T>() where T : IServiceProvider;
        public T GetService<T>() where T : IServiceProvider;
    }

    public class ServiceCollection : IServiceCollection
    {
        public List<IServiceProvider> Collection { get; }

        public ServiceCollection()
        {
            Collection = new List<IServiceProvider>();
        }

        public void Add<T>(object[] args = null) where T : IServiceProvider
        {
            if (Collection.Any(x => x.GetType().Equals(typeof(T))))
            {
                throw new InvalidOperationException("Current provider already exist in service collecion!");
            }

            var provider = (IServiceProvider)Activator.CreateInstance(typeof(T), args);

            Collection.Add(provider);
        }

        public void Remove<T>() where T : IServiceProvider
        {
            var provider = Collection.Find(x => x.GetType().Equals(typeof(T)));

            if (provider != null)
            {
                Collection.Remove(provider);
            }
        }

        public T GetService<T>() where T : IServiceProvider
        {
            return (T)Collection.Find(x => x.GetType().Equals(typeof(T)));
        }
    }
}
