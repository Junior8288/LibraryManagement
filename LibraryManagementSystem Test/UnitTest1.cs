using Xunit;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem_Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1_AddBook_Succuessful()
        {
            //Create a new book
            var initialCount = BookDataStore.GetAllBooks().Count;

            var newBook = new Book
            {
                Title = "Test Driven Development",
                Author = "Test Author",
                Category = "Software Engineering",
                ISBN = "123-456-7890",
                Description = "A book about TDD practices",
                SubmittedBy = "Test User",
            };

            //Perform the action
            BookDataStore.AddBook(newBook);

            //Get the new count
            var newCount = BookDataStore.GetAllBooks().Count;
            Assert.Equal(initialCount + 1, newCount);

            Assert.True(newBook.Id > 0, "Book should have an ID assigned");

            Assert.Equal(BookStatus.Pending, newBook.Status);

            //Verify if we can retrieve the book
            var retrievedBook = BookDataStore.GetBookById(newBook.Id);

        }
    }
}