using LibraryManagement.Core;
using LibraryManagement.Core.Exceptions;
using LibraryManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Category> categories = new List<Category>
        {
            new Category{ Id=1, Name="Fiction"},
            new Category{ Id=2, Name="Drama"},
            new Category{ Id=3, Name="Thriller"},
        };
        public async Task<Category> Add(Category category)
        {
            await Task.Delay(2000);
            var isExisting= categories.FirstOrDefault(e => e.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase))!=null;
            if (isExisting)
            {
                throw new DatabaseException("Category already exists");
            }
            else
            {
                var last=categories.LastOrDefault();
                category.Id = last == null ? 1 : last.Id + 1;
                categories.Add(category);
                return category;
            }
        }

        public async  Task<bool> Delete(int categoryId)
        {
            await Task.Delay(2000);
            var categoryToDelete = categories.FirstOrDefault(e => e.Id== categoryId);
            if (categoryToDelete==null)
            {
                return false;
            }
            else
            {
                return categories.Remove(categoryToDelete);
            }
        }

        public async Task<Category> Update(Category category)
        {
            await Task.Delay(2000);
            var categoryToUpdate = categories.FirstOrDefault(e => e.Id==category.Id);
            if (categoryToUpdate==null)
            {
                throw new DatabaseException("Category doesn't exists");
            }
            else
            {
                categoryToUpdate.Name = category.Name;
                return category;
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            await Task.Delay(2000);
            return categories;
        }
    }
}
