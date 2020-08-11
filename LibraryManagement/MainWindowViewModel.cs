using LibraryManagement.Components;
using LibraryManagement.Components.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class MainWindowViewModel:BindableBaseViewModel
    {
        #region Properties
        private readonly CategoriesListViewModel _categoriesListViewModel;

        private readonly AddEditCategoryViewModel _addEditCategoryViewModel;

        private BindableBaseViewModel _currentViewModel;
        public BindableBaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }
        #endregion

        #region Commands
        public RelayCommand<string> NavigateCommand { get; set; }
        #endregion

        public MainWindowViewModel(CategoriesListViewModel categoriesListViewModel,
            AddEditCategoryViewModel addEditCategoryViewModel)
        {
            _categoriesListViewModel = categoriesListViewModel;
            _addEditCategoryViewModel = addEditCategoryViewModel;
            NavigateCommand = new RelayCommand<string>(Navigate);
            _categoriesListViewModel.NavigateToAdd += () => { _addEditCategoryViewModel.Category = null; Navigate("addEditCategory"); };
            _categoriesListViewModel.NavigateToEdit += (category) => { _addEditCategoryViewModel.Category = category; Navigate("addEditCategory"); };
            _addEditCategoryViewModel.Done += () => { Navigate("categories"); };
        }

        public void Navigate(string destintion)
        {
            switch (destintion)
            {
                case "categories":
                    CurrentViewModel = _categoriesListViewModel;
                    break;
                case "addEditCategory":
                    CurrentViewModel = _addEditCategoryViewModel;
                    break;
                default:
                    break;
            }
        }
    }

}
