using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MusicApp.Plumbing
{
    /// <summary>
    /// 
    /// </summary>
    public class CastleWindsorDependencyResolver
        : IDependencyResolver
    {
        private readonly IWindsorContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="CastleWindsorDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public CastleWindsorDependencyResolver(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <param name="serviceType">The type of the requested service or object.</param>
        /// <returns>
        /// The requested service or object.
        /// </returns>
        public Object GetService(Type serviceType)
        {
            return container.Kernel.HasComponent(serviceType) ? container.Resolve(serviceType) : null;
        }

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>
        /// The requested services.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.Kernel.HasComponent(serviceType) ? container.ResolveAll(serviceType).Cast<object>() : new object[] { };
        }
    }
}