// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorControllerFactory.cs" company="Arne Klein">
//   Arne Klein
// </copyright>
// <summary>
//   Stellt Methoden zur Verfügung um MVC Controller über Dependency Injection Instanziieren zu können.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CastleWindsorSnap
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Castle.Core;
    using Castle.Core.Logging;
    using Castle.Windsor;

    /// <summary>
    /// Stellt Methoden zur Verfügung um MVC Controller über Dependency Injection Instanziieren zu können.
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Stellt Methoden zum aufloesen von abhängigkeiten  zur Verfügung
        /// </summary>
        private readonly IWindsorContainer container;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="WindsorControllerFactory"/> Klasse.
        /// </summary>
        /// <param name="container">Stellt alle Windsorfunktionalitaeten bereit</param>
        public WindsorControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Holt oder setzt einen Logger
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gibt einen Controller wieder frei um Memory Leaks zu verhindern
        /// </summary>
        /// <param name="controller">Gibt einen Controller wieder frei</param>
        public override void ReleaseController(IController controller)
        {
            this.container.Release(controller);
        }

        /// <summary>
        /// Erstellt eine Instanz eines MVC Controllers
        /// </summary>
        /// <param name="requestContext">Die Anfrage vom Client</param>
        /// <param name="controllerType">Der angefragte Controller</param>
        /// <returns>Gibt einen MVC Controller zurück</returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            var controller = (Controller)this.container.Resolve(controllerType);
            //if (controller != null)
            //{
            //    controller.ActionInvoker = this.container.Resolve<IActionInvoker>();
            //}

            return controller;
        }
    }
}