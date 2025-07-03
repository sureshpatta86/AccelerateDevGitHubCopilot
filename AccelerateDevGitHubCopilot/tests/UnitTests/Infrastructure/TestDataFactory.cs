using System.Text.Json;
using Library.ApplicationCore.Entities;
using Library.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace Library.UnitTests.Infrastructure;

public static class TestDataFactory
{
    private static readonly List<string> _tempFiles = new List<string>();

    public static JsonData CreateMockJsonDataWithFiles()
    {
        // Create temporary files with test data
        var authorsFile = CreateTempJsonFile(CreateTestAuthors());
        var booksFile = CreateTempJsonFile(CreateTestBooks());
        var bookItemsFile = CreateTempJsonFile(CreateTestBookItems());
        var patronsFile = CreateTempJsonFile(CreateTestPatrons());
        var loansFile = CreateTempJsonFile(CreateTestLoans());

        var configuration = CreateTestConfiguration(new Dictionary<string, string>
        {
            {"JsonPaths:Authors", authorsFile},
            {"JsonPaths:Books", booksFile},
            {"JsonPaths:BookItems", bookItemsFile},
            {"JsonPaths:Patrons", patronsFile},
            {"JsonPaths:Loans", loansFile}
        });

        var jsonData = new JsonData(configuration);
        return jsonData;
    }

    public static void CleanupTempFiles()
    {
        foreach (var tempFile in _tempFiles.ToList())
        {
            if (File.Exists(tempFile))
            {
                try
                {
                    File.Delete(tempFile);
                    _tempFiles.Remove(tempFile);
                }
                catch
                {
                    // Ignore cleanup errors
                }
            }
        }
    }

    public static JsonData CreateMockJsonData()
    {
        var configuration = Substitute.For<IConfiguration>();
        var section = Substitute.For<IConfigurationSection>();
        
        configuration.GetSection("JsonPaths").Returns(section);
        section["Authors"].Returns("test-authors.json");
        section["Books"].Returns("test-books.json");
        section["BookItems"].Returns("test-book-items.json");
        section["Patrons"].Returns("test-patrons.json");
        section["Loans"].Returns("test-loans.json");

        var jsonData = new JsonData(configuration);
        
        // Initialize with test data
        jsonData.Authors = CreateTestAuthors();
        jsonData.Books = CreateTestBooks();
        jsonData.BookItems = CreateTestBookItems();
        jsonData.Patrons = CreateTestPatrons();
        jsonData.Loans = CreateTestLoans();

        return jsonData;
    }

    public static List<Author> CreateTestAuthors()
    {
        return new List<Author>
        {
            new Author { Id = 1, Name = "Test Author 1" },
            new Author { Id = 2, Name = "Test Author 2" }
        };
    }

    public static List<Book> CreateTestBooks()
    {
        return new List<Book>
        {
            new Book { Id = 1, Title = "Test Book 1", AuthorId = 1, Genre = "Fiction", ImageName = "book1.jpg", ISBN = "1234567890" },
            new Book { Id = 2, Title = "Test Book 2", AuthorId = 2, Genre = "Non-Fiction", ImageName = "book2.jpg", ISBN = "0987654321" }
        };
    }

    public static List<BookItem> CreateTestBookItems()
    {
        return new List<BookItem>
        {
            new BookItem { Id = 1, BookId = 1, AcquisitionDate = DateTime.Now.AddYears(-1), Condition = "New" },
            new BookItem { Id = 2, BookId = 2, AcquisitionDate = DateTime.Now.AddYears(-1), Condition = "Good" }
        };
    }

    public static List<Patron> CreateTestPatrons()
    {
        return new List<Patron>
        {
            new Patron 
            { 
                Id = 1, 
                Name = "John Doe", 
                MembershipStart = DateTime.Now.AddYears(-1),
                MembershipEnd = DateTime.Now.AddYears(1),
                ImageName = "patron1.jpg"
            },
            new Patron 
            { 
                Id = 2, 
                Name = "Jane Smith", 
                MembershipStart = DateTime.Now.AddYears(-2),
                MembershipEnd = DateTime.Now.AddDays(-30), // Expired membership
                ImageName = "patron2.jpg"
            }
        };
    }

    public static List<Loan> CreateTestLoans()
    {
        return new List<Loan>
        {
            new Loan 
            { 
                Id = 1, 
                BookItemId = 1, 
                PatronId = 1,
                LoanDate = DateTime.Now.AddDays(-14),
                DueDate = DateTime.Now.AddDays(7),
                ReturnDate = null
            },
            new Loan 
            { 
                Id = 2, 
                BookItemId = 2, 
                PatronId = 2,
                LoanDate = DateTime.Now.AddDays(-30),
                DueDate = DateTime.Now.AddDays(-9),
                ReturnDate = DateTime.Now.AddDays(-5)
            }
        };
    }

    public static string CreateTempJsonFile<T>(T data)
    {
        var tempFile = Path.GetTempFileName();
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(tempFile, json);
        _tempFiles.Add(tempFile);
        return tempFile;
    }

    public static IConfiguration CreateTestConfiguration(Dictionary<string, string> configData)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(configData!)
            .Build();
    }
}
