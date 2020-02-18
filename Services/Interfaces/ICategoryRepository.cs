using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApiProject.Models;

namespace BookApiProject.Services.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        
        Category GetCategory(int categoryId);

        ICollection<Category> GetAllCategoriesForABook(int bookId);

        Book GetBookForCategory(int categoryId);

        ICollection<Book> GetAllBooksForCategory (int categoryId);

        bool CategoryExists(int categoryId);

        bool IsDuplicateCategoryName(int categoryId, string categoryName);

    }
}
