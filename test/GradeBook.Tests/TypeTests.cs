using System;
using Xunit;

namespace GradeBook.Tests
{
    public class TypeTests
    {
        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "Dillon";
            var upper = MakeUppercase(name);

            Assert.Equal("Dillon", name);
            Assert.Equal("DILLON", upper);
        }

        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void ChangeValueTypeUsingReference()
        {
            var x = GetInt();
            SetInt(ref x);
 
            Assert.Equal(42, x);
        }

        private void SetInt(ref int z)
        {
            z = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            // 1. Arrange
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name");
 
            // 2. Act

            // 3. Assert
            Assert.Equal("New Name", book1.Name);
        }

        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            // 1. Arrange
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");
 
            // 2. Act

            // 3. Assert
            Assert.Equal("Book 1", book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            // 1. Arrange
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");
 
            // 2. Act

            // 3. Assert
            Assert.Equal("New Name", book1.Name);
        }

        private void SetName(InMemoryBook book, string name)
        {
        book.Name = name;
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            // 1. Arrange
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            // 2. Act

            // 3. Assert
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            // 1. Arrange
            var book1 = GetBook("Book 1");
            var book2 = book1;

            // 2. Act

            // 3. Assert
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook(string name)
            {
                return new InMemoryBook(name);
            }
    }
}
