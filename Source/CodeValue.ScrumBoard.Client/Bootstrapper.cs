using Caliburn.Micro;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CodeValue.ScrumBoard.Client.ViewModels;
using System.IO;
using System.Reflection;
using CodeValue.ScrumBoard.Client.Navigation;
using CodeValue.ScrumBoard.Client.Messages;
using CodeValue.ScrumBoard.Client.Models;
using CodeValue.ScrumBoard.Client.Apis;
using Refit;
using System.Net.Http;
using System.Threading;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CodeValue.ScrumBoard.Client.Common;

namespace CodeValue.ScrumBoard.Client
{
    internal class Bootstrapper : BootstrapperBase
    {
        private const string ModuleSearchPattern = "*.exe";
        private const string ModuleFilePrefix = "CodeValue.ScrumBoard.Client";

        private IContainer _container;

        public static object GetToken { get; private set; }

        protected override void Configure()
        {
            base.Configure();

            var builder = new ContainerBuilder();
            // Register infrastructure.
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<TokenStore>().As<ITokenStore>().SingleInstance();

            ResgiterApi<IUserApi>(builder);
            ResgiterApi<IBoardApi>(builder);
            ResgiterApi<ITaskApi>(builder);

            RegisterViewModels(builder);

            _container = builder.Build();
        }

        class AuthenticatedHttpClientHandler : HttpClientHandler
        {
            private readonly Func<Task<string>> getToken;

            public AuthenticatedHttpClientHandler(Func<Task<string>> getToken)
            {
                if (getToken == null) throw new ArgumentNullException("getToken");
                this.getToken = getToken;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                // See if the request has an authorize header
                var auth = request.Headers.Authorization;
                if (auth != null)
                {
                    var token = await getToken().ConfigureAwait(false);
                    request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
                }

                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
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

        private static void ResgiterApi<TApi>(ContainerBuilder builder)
        {
            builder.Register(x =>
            {
                var tokenStore = x.Resolve<ITokenStore>();
                return RestService.For<TApi>(
                    new HttpClient(new AuthenticatedHttpClientHandler(() => Task.FromResult(tokenStore.Token)))
                    {
                        BaseAddress = new Uri(Constants.ServerUri)
                    });

            }).As<TApi>();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().SingleInstance();                      
            builder.RegisterType<LoginViewModel>().As<ILoginViewModel<object>>();
            builder.RegisterType<BoardsViewModel>().As<IBoardsViewModel<object>>();
            builder.RegisterType<BoardViewModel>().As<IBoardViewModel<BoardActiveMessage>>();
            builder.RegisterType<TaskViewModel>().As<ITaskViewModel<object>>();
            builder.RegisterType<BoardItemViewModel>().As< IBoardItemViewModel<object>>();
        }
    }

}
