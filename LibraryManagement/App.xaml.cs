using LibraryManagement.Components.Categories;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace LibraryManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ICategoryRepository, CategoryRepository>(new ContainerControlledLifetimeManager());


            var mainWindowViewModel = container.Resolve<MainWindowViewModel>();

            //mainWindowViewModel.CategoriesListViewModel = container.Resolve<CategoriesListViewModel>();
            //mainWindowViewModel.AddEditCategoryViewModel = container.Resolve<AddEditCategoryViewModel>();


            var window = new MainWindow() { DataContext=mainWindowViewModel };
            window.Show();
        }
    }
}
