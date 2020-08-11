using LibraryManagement.Core;
using LibraryManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Components.Categories
{
    public class CategoriesListViewModel : BindableBaseViewModel
    {
        #region Properties
        private readonly ICategoryRepository _categoryRepository;

        private ObservableCollection<CategoryViewModel> _categories;
        public ObservableCollection<CategoryViewModel> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }
        #endregion


        #region Commands

        public RelayCommand<CategoryViewModel> AddEditCommand { get; set; }
        public RelayCommand<int> DeleteCommand { get; set; }
        #endregion


        #region Events
        public event Action NavigateToAdd=delegate { };
        public event Action<CategoryViewModel> NavigateToEdit = delegate { };
        public event Action Done = delegate { };
        #endregion

        public CategoriesListViewModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            AddEditCommand = new RelayCommand<CategoryViewModel>(OnAddEditClick);
            DeleteCommand = new RelayCommand<int>(OnDelete);
        }

        public async void LoadCategories()
        {
            Categories = new ObservableCollection<CategoryViewModel>(
                    (await _categoryRepository.GetAll()).Select(e=>new CategoryViewModel {Id=e.Id, Name=e.Name }));
        }
        public async void OnAddEditClick(CategoryViewModel category)
        {
            if (category == null)
                NavigateToAdd.Invoke();
            else
                NavigateToEdit.Invoke(category);
        }
        public async void OnDelete (int categoryId)
        {
            var result=await _categoryRepository.Delete(categoryId);
            if (result)
                LoadCategories();
        }
    }
}
