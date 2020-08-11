using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> Add(Category category);
        Task<Category> Update(Category category);
        Task<bool> Delete(int categoryId);
        Task<IEnumerable<Category>> GetAll();
    }
}
