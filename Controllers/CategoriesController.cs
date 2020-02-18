using BookApiProject.Dtos;
using BookApiProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private ICategoryRepository _categoriesRepository;
        private IBookRepository _bookRepository;

        public CategoriesController(ICategoryRepository categoriesRepository, IBookRepository bookRepository)
        {
            _categoriesRepository = categoriesRepository;
             _bookRepository = bookRepository;
        }

        //api/categories
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
        public IActionResult GetCategories()
        {
            var categories = _categoriesRepository.GetCategories().ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            var categoriesDto = new List<CategoryDto>();

            foreach (var category in categories)
            {
                categoriesDto.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Ok(categoriesDto);

        }

        //api/categories/categoryId
        [HttpGet("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoriesRepository.CategoryExists(categoryId))
                return NotFound();

            var category = _categoriesRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryDto = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name

            };

            return Ok(categoryDto);

        }


    
         //api/categories/books/bookId
        [HttpGet("books/{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetAllCategoriesForABook(int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
                return NotFound();

            var categories = _categoriesRepository.GetAllCategoriesForABook(bookId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriesDto = new List<CategoryDto>();
            foreach(var category in categories)
            {
                categoriesDto.Add(new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }            

            return Ok(categoriesDto);
        }

        //api/categories/categoryId/books
        [HttpGet("{categoryId}/books")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetAllBooksForCategory(int categoryId)
        {
            if (!_categoriesRepository.CategoryExists(categoryId))
                return NotFound();

            var books = _categoriesRepository.GetAllBooksForCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booksDto = new List<BookDto>();

            foreach (var book in books)
            {
                booksDto.Add(new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Isbn = book.Isbn,
                    DatePublished = book.DatePublished
                    
                });
            }

            return Ok(booksDto);
        }





}
}
