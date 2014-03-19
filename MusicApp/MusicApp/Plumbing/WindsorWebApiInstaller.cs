using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MusicApp.Plumbing
{
    /// <summary>
    /// 
    /// </summary>
    public class WindsorWebApiInstaller
        : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleScoped());
        }
    }
}