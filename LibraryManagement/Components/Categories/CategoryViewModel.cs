using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Components.Categories
{
    public class CategoryViewModel: ValidatableBindableBaseViewModel
    {
        public int Id { get; set; }
        private string _name;
        [Required]
        [MaxLength(10)]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

    }
}
