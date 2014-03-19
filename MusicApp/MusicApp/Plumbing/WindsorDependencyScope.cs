using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;

namespace MusicApp.Plumbing
{
    /// <summary>
    /// 
    /// </summary>
    public class WindsorDependencyScope
        : IDependencyScope
    {
        private readonly IKernel container;
        private readonly IDisposable scope;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorDependencyScope"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public WindsorDependencyScope(IKernel container)
        {
            this.container = container;
            this.scope = container.BeginScope();
        }

        /// <summary>
        /// Retrieves a service from the scope.
        /// </summary>
        /// <param name="serviceType">The service to be retrieved.</param>
        /// <returns>
        /// The retrieved service.
        /// </returns>
        public object GetService(Type serviceType)
        {
            return this.container.HasComponent(serviceType) ? this.container.Resolve(serviceType) : null;
        }

        /// <summary>
        /// Retrieves a collection of services from the scope.
        /// </summary>
        /// <param name="serviceType">The collection of services to be retrieved.</param>
        /// <returns>
        /// The retrieved collection of services.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.ResolveAll(serviceType).Cast<object>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (this.scope != null)
                {
                    this.scope.Dispose();
                }
            }
        }
    }
}