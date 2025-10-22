using Xunit;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using System.Text;

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

        [Fact]
        public async Task Test2_EncyptionFile_Successful()
        {
            var originalContent = "this is a secret file content thar should be encrypted";
            var originalBytes = Encoding.UTF8.GetBytes(originalContent);
            var inputStream = new MemoryStream(originalBytes);
            var tempFile = Path.GetTempFileName();
            var encryptionServices = new FileEncryptionService();

            try
            {
                await encryptionServices.EncryptFileAsync(inputStream, tempFile);
                Assert.True(File.Exists(tempFile), "Encrypted file should exist");

                //Read the encryption file
                var encryptedBytes = await File.ReadAllBytesAsync(tempFile);

                //Assert that the encrypted data is not the same as the original
                Assert.NotEqual(originalBytes, encryptedBytes);

                //Verify the encrypted
                Assert.True(encryptedBytes.Length > 0, "Encrypted file should have content");

                //Verify an account read the original text from the encryption file
                var encryptedText = Encoding.UTF8.GetString(encryptedBytes);
                Assert.DoesNotContain("this is a secret file content thar should be encrypted", encryptedText);
            }
            finally
            {
                if (File.Exists(tempFile)) File.Delete(tempFile);    
            }
        }
    }
}