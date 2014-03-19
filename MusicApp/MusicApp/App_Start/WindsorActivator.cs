using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(MusicApp.WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethodAttribute(typeof(MusicApp.WindsorActivator), "Shutdown")]

namespace MusicApp
{
    /// <summary>
    /// 
    /// </summary>
    public static class WindsorActivator
    {
        static ContainerBootstrapper bootstrapper;

        /// <summary>
        /// Preferences the start.
        /// </summary>
        public static void PreStart()
        {
            bootstrapper = ContainerBootstrapper.Bootstrap();
        }

        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        public static void Shutdown()
        {
            if (bootstrapper != null)
            {
                bootstrapper.Dispose();
            }
        }
    }
}