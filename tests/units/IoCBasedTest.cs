using Autofac;
using Autofac.Core;

namespace Tests
{
    public class IoCBasedTest<TModule> where TModule : IModule, new()
    {
        protected IContainer container;

        public IoCBasedTest()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TModule());
            container = builder.Build();
        }

        protected TSvc Resolve<TSvc>()
        {
            return container.Resolve<TSvc>();
        }

        protected TSvc Resolve<TSvc>(string key, object value)
        {
            return container.Resolve<TSvc>(new NamedParameter(key, value));
        }

        protected void Teardown()
        {
            
        }
    }
}