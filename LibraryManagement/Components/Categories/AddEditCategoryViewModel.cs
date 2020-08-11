using LibraryManagement.Components.Confirm;
using LibraryManagement.Core;
using LibraryManagement.Core.Exceptions;
using LibraryManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Components.Categories
{
    public class AddEditCategoryViewModel:BindableBaseViewModel
    {
        #region Properties
        private CategoryViewModel _category;
        public CategoryViewModel Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private bool _editMode { get; set; }

        private bool _hasChanged { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private readonly ICategoryRepository _categoryRepository;
        #endregion

        #region Commands
        public RelayCommand Save { get; set; }
        public RelayCommand Cancel { get; set; }
        #endregion
        #region Events

        public event Action Done = delegate { };
        #endregion


        public AddEditCategoryViewModel(ICategoryRepository categoryRepository)
        {
            Save = new RelayCommand(OnSave,CanSave);
            Cancel = new RelayCommand(OnCancel);
            _categoryRepository = categoryRepository;
        }

        private bool CanSave()
        {
            return _hasChanged && Category != null && !Category.HasErrors;
        }
        private async void OnSave()
        {
            try
            {
                var category = new Category { Id = Category.Id, Name = Category.Name };
                if (_editMode)
                    await _categoryRepository.Update(category);
                else
                    await _categoryRepository.Add(category);
                Done.Invoke();
            }
            catch(DatabaseException ex)
            {
                //show error message
            }catch (Exception)
            {

            }
          
        }
        private void OnCancel()
        {
            if(_hasChanged)
            {
                //show modal
                var confirmInput = new ConfirmViewModel { Message = "You have unsaved changes.\nAre you sure you want to cancel?" };
                var confirmationWidnow = new ConfirmView() { DataContext = confirmInput };
                confirmInput.Result += (response) => {
                    confirmationWidnow.Close();
                    if (response) Done.Invoke();
                };
                confirmationWidnow.Show();
            }
            else
            {
                Done.Invoke();
            }

        }

        public void OnChange()
        {
            _hasChanged = true;
            Save.RaiseCanExecuteChanged();
        }
        public void OnLoad()
        {
            _hasChanged = false;
            if (Category == null)
            {
                Title ="Add Category";
                _editMode = false;
                Category = new CategoryViewModel();
            }
            else
            {
                _editMode = true;
                Title = "Edit Category";
            }
        }
    }
}
