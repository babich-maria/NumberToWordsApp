using System.Web.Mvc;
using ProcessService;
using ProcessService.Interfaces;
using Unity;
using Unity.Mvc5;

namespace NumberToWordApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ITranslator, EnglishTranslator>();
            container.RegisterType<IConverterService, ConverterService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}