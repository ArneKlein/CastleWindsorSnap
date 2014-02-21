using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CastleWindsorSnap.Controllers;
using CastleWindsorSnap.Interceptor;
using Snap;
using Snap.CastleWindsor;

namespace CastleWindsorSnap
{
    /// <summary>
    /// Registriert alle Klassen und deren Abhängigkeiten.
    /// </summary>
    public class WindsorInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Der Container von Castle Windsor, in diesem werden alle Objekte registriert werden.
        /// </summary>
        private IWindsorContainer container;

        /// <summary>
        /// Registriert die Klassen und deren Abhängigkeiten, damit diese zur Laufzeit aufgeloest werden können.
        /// </summary>
        /// <param name="container">Stellt alle Windsor Funktionalitaeten bereit</param>
        /// <param name="store">Der Vertrag für den Kernel für externe Abhängigkeiten</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            this.container = container;

            this.RegisterAspects(container);

            this.container.Register(Classes
                 .FromAssemblyContaining<HomeController>()
                 .BasedOn<IController>()
                 .Configure(r => r.LifestylePerWebRequest()));
        }

        /// <summary>
        /// Initialisiert den Aspect-Container von SNAP
        /// </summary>
        /// <param name="container">Der Castle-Windsor-Container</param>
        public void RegisterAspects(IWindsorContainer container)
        {
            SnapConfiguration.For(new CastleAspectContainer(container.Kernel)).Configure(c =>
            {
                c.IncludeNamespace("CastleWindsorSnap*");
                c.Bind<NullPropertyInterceptor>().To<NotNullAttribute>();
                c.Bind<NotEmptyPropertyInterceptor>().To<NotEmptyAttribute>();
                c.AllAspects().KeepInContainer();
            });

            container.Register(Component.For<NullPropertyInterceptor>().Instance(new NullPropertyInterceptor()).LifeStyle.Transient);
            container.Register(Component.For<NotEmptyPropertyInterceptor>().Instance(new NotEmptyPropertyInterceptor()).LifeStyle.Transient);
        }
    }
}
