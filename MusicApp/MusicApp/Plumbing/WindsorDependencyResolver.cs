using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace MusicApp.Plumbing
{
    /// <summary>
    /// 
    /// </summary>
    internal class WindsorDependencyResolver
        : IDependencyResolver
    {
        private readonly IKernel container;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public WindsorDependencyResolver(IKernel container)
        {
            this.container = container;
        }

        /// <summary>
        /// Begins the scope.
        /// </summary>
        /// <returns></returns>
        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(this.container);
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return this.container.HasComponent(serviceType) ? this.container.Resolve(serviceType) : null;
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.ResolveAll(serviceType).Cast<object>();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose() { }
    }
}