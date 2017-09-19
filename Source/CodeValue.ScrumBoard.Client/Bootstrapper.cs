using Caliburn.Micro;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CodeValue.ScrumBoard.Client.ViewModels;
using System.IO;
using System.Reflection;
using CodeValue.ScrumBoard.Client.Common;
using CodeValue.ScrumBoard.Client.Events;
using CodeValue.ScrumBoard.Client.Navigation;

namespace CodeValue.ScrumBoard.Client
{
    internal class Bootstrapper : BootstrapperBase
    {
        private const string ModuleSearchPattern = "*.exe";
        private const string ModuleFilePrefix = "CodeValue.ScrumBoard.Client";

        private IContainer _container;

        protected override void Configure()
        {
            base.Configure();

            var builder = new ContainerBuilder();
            // Register infrastructure.
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            RegisterViewModels(builder);

           _container = builder.Build();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return GetAllDllEntries().Select(Assembly.LoadFrom);
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        protected override object GetInstance(Type service, string key)
        {
            return key == null ? _container.Resolve(service) : _container.ResolveKeyed(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var enumerableOfServiceType = typeof(IEnumerable<>).MakeGenericType(service);
            return (IEnumerable<object>)_container.Resolve(enumerableOfServiceType);
        }

        private static string[] GetAllDllEntries()
        {
            var runtimeDir = AppDomain.CurrentDomain.BaseDirectory;
            var files = Directory.GetFiles(runtimeDir, ModuleSearchPattern)
                .Where(x =>
                {
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(x);
                    return fileNameWithoutExtension != null && fileNameWithoutExtension.StartsWith(ModuleFilePrefix, StringComparison.Ordinal);
                }).ToArray();

            return files;
        }

      

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().SingleInstance();                      
            builder.RegisterType<LoginViewModel>().As<ILoginViewModel<object>>();
            builder.RegisterType<BoardsViewModel>().As<IBoardsViewModel<object>>();
            builder.RegisterType<TaskViewModel>().As<ITaskViewModel<object>>();
            builder.RegisterType<BoardItemViewModel>().As<IBoardItemViewModel<BoardActivePayload>>();
        }

      

    }

}
