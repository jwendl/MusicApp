using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using MusicApp.Data;
using MusicApp.Plumbing;
using SharpRepository.EfRepository;
using SharpRepository.Repository;
using SharpRepository.Repository.Caching;
using SharpRepository.Repository.Configuration;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MusicApp
{
    /// <summary>
    /// 
    /// </summary>
    public class ContainerBootstrapper
        : IContainerAccessor, IDisposable
    {
        readonly IWindsorContainer container;

        /// <summary>
        /// Prevents a default instance of the <see cref="ContainerBootstrapper"/> class from being created.
        /// </summary>
        /// <param name="container">The container.</param>
        ContainerBootstrapper(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IWindsorContainer Container
        {
            get { return container; }
        }

        /// <summary>
        /// Bootstraps this instance.
        /// </summary>
        /// <returns></returns>
        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer()
                .Install(FromAssembly.This());

            // Register mvc container.
            var resolver = new CastleWindsorDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);

            // Register web api container.
            container.Install(FromAssembly.This());
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(container.Kernel);

            //register Ioc components.
            container.Register(Component.For<HttpContextBase>()
                .UsingFactoryMethod(k => (HttpContextBase)new HttpContextWrapper(HttpContext.Current))
                .LifestylePerWebRequest());

            RegisterRepositories(container);

            return new ContainerBootstrapper(container);
        }

        private static void RegisterRepositories(IWindsorContainer container)
        {
            // Register SharpRepository items.
            container.Register(Component.For<DbContext>().ImplementedBy<MusicAppEntities>().LifestylePerWebRequest());

            var sharpRepositoryConfiguration = new SharpRepositoryConfiguration();
            sharpRepositoryConfiguration.AddRepository(new EfRepositoryConfiguration("MusicAppEntities", "DataContext", typeof(MusicAppEntities)));

            // Register repositories here!
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(EfRepository<>)).LifestylePerWebRequest());

            // Register caching strategies here. 
            // Default NoCache Registration.
            container.Register(Classes.FromAssemblyContaining(typeof(ICachingStrategy<,>)).BasedOn(typeof(NoCachingStrategy<,>)).WithService.FromInterface().LifestylePerWebRequest());

            // Register caching strategies here.
            //var timeout = 300;
            //container.Register(Component.For<ICachingStrategy<User, int>>()
            //    .ImplementedBy<TimeoutCachingStrategy<User, int>>()
            //    .DependsOn(Dependency.OnValue(typeof(int), timeout))
            //    .LifestyleSingleton());
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
            container.Dispose();
        }
    }
}