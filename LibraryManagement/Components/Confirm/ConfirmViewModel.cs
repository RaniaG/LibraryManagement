using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Components.Confirm
{
    public class ConfirmViewModel
    {
        public string Message { get; set; }
        public event Action<bool> Result = delegate { };
        public RelayCommand<bool> UserResponse { get; set; }

        public ConfirmViewModel()
        {
            UserResponse = new RelayCommand<bool>(OnUserResponse);
        }

        public void OnUserResponse(bool result)
        {
            Result.Invoke(result);
        }
    }
}
