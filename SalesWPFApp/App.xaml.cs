using Microsoft.Extensions.DependencyInjection;
using SalesWPFAppLibrary.DataAccess;
using SalesWPFAppLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            //Config for Dependency Injection (DI)
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository)); services.AddSingleton<LoginWindown>();
            services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository)); services.AddSingleton<RegisterWindown>();
            services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository)); services.AddSingleton<MemberWindown>();
            services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository)); services.AddSingleton<ProductWindown>();
            services.AddSingleton(typeof(IOrderDetailRepository), typeof(OrderDetailRepository)); services.AddSingleton<OrderWindown>();
            services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository)); services.AddSingleton<ProfileWindown>();
        }
        //--
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var windown = serviceProvider.GetService<MemberWindown>();
            windown.Show();
        }
    }
}
