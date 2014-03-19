using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace MusicApp.Plumbing
{
    /// <summary>
    /// 
    /// </summary>
    public class WindsorControllerFactory
        : DefaultControllerFactory
    {
        readonly IWindsorContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorControllerFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public WindsorControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>
        /// The controller instance.
        /// </returns>
        /// <exception cref="System.Web.HttpException">404</exception>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                throw new HttpException(404, String.Format(CultureInfo.CurrentCulture, "The requested controller for path '{0}' could not be found", requestContext.HttpContext.Request.Path));
            return (IController)container.Resolve(controllerType);
        }

        /// <summary>
        /// Releases the specified controller.
        /// </summary>
        /// <param name="controller">The controller to release.</param>
        public override void ReleaseController(IController controller)
        {
            container.Release(controller);
        }
    }
}