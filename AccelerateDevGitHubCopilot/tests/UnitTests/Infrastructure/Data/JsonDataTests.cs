using System.Text.Json;
using Library.ApplicationCore.Entities;
using Library.Infrastructure.Data;
using Library.UnitTests.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Library.UnitTests.Infrastructure.Data;

public class JsonDataTests : IDisposable
{
    private readonly List<string> _tempFiles = new List<string>();
    private readonly JsonData _jsonData;

    public JsonDataTests()
    {
        var configData = new Dictionary<string, string>
        {
            {"JsonPaths:Authors", CreateTempFile("[]")},
            {"JsonPaths:Books", CreateTempFile("[]")},
            {"JsonPaths:BookItems", CreateTempFile("[]")},
            {"JsonPaths:Patrons", CreateTempFile("[]")},
            {"JsonPaths:Loans", CreateTempFile("[]")}
        };

        var configuration = TestDataFactory.CreateTestConfiguration(configData);
        _jsonData = new JsonData(configuration);
    }

    private string CreateTempFile(string content)
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, content);
        _tempFiles.Add(tempFile);
        return tempFile;
    }

    [Fact(DisplayName = "JsonData.LoadData: Loads all JSON files successfully")]
    public async Task LoadData_LoadsAllJsonFilesSuccessfully()
    {
        // Arrange
        var authors = TestDataFactory.CreateTestAuthors();
        var books = TestDataFactory.CreateTestBooks();
        var bookItems = TestDataFactory.CreateTestBookItems();
        var patrons = TestDataFactory.CreateTestPatrons();
        var loans = TestDataFactory.CreateTestLoans();

        // Write test data to temp files
        await File.WriteAllTextAsync(_tempFiles[0], JsonSerializer.Serialize(authors));
        await File.WriteAllTextAsync(_tempFiles[1], JsonSerializer.Serialize(books));
        await File.WriteAllTextAsync(_tempFiles[2], JsonSerializer.Serialize(bookItems));
        await File.WriteAllTextAsync(_tempFiles[3], JsonSerializer.Serialize(patrons));
        await File.WriteAllTextAsync(_tempFiles[4], JsonSerializer.Serialize(loans));

        // Act
        await _jsonData.LoadData();

        // Assert
        Assert.NotNull(_jsonData.Authors);
        Assert.NotNull(_jsonData.Books);
        Assert.NotNull(_jsonData.BookItems);
        Assert.NotNull(_jsonData.Patrons);
        Assert.NotNull(_jsonData.Loans);

        Assert.Equal(2, _jsonData.Authors.Count);
        Assert.Equal(2, _jsonData.Books.Count);
        Assert.Equal(2, _jsonData.BookItems.Count);
        Assert.Equal(2, _jsonData.Patrons.Count);
        Assert.Equal(2, _jsonData.Loans.Count);
    }

    [Fact(DisplayName = "JsonData.EnsureDataLoaded: Loads data when not already loaded")]
    public async Task EnsureDataLoaded_LoadsDataWhenNotAlreadyLoaded()
    {
        // Arrange
        Assert.Null(_jsonData.Patrons); // Initially null

        // Act
        await _jsonData.EnsureDataLoaded();

        // Assert
        Assert.NotNull(_jsonData.Patrons);
        Assert.NotNull(_jsonData.Authors);
        Assert.NotNull(_jsonData.Books);
        Assert.NotNull(_jsonData.BookItems);
        Assert.NotNull(_jsonData.Loans);
    }

    [Fact(DisplayName = "JsonData.EnsureDataLoaded: Does not reload when data already loaded")]
    public async Task EnsureDataLoaded_DoesNotReloadWhenDataAlreadyLoaded()
    {
        // Arrange
        await _jsonData.LoadData();
        var originalPatrons = _jsonData.Patrons;

        // Act
        await _jsonData.EnsureDataLoaded();

        // Assert
        Assert.Same(originalPatrons, _jsonData.Patrons); // Should be the same reference
    }

    [Fact(DisplayName = "JsonData.GetPopulatedPatron: Returns patron with populated loans")]
    public async Task GetPopulatedPatron_ReturnsPatronWithPopulatedLoans()
    {
        // Arrange
        var testData = TestDataFactory.CreateMockJsonData();
        _jsonData.Authors = testData.Authors;
        _jsonData.Books = testData.Books;
        _jsonData.BookItems = testData.BookItems;
        _jsonData.Patrons = testData.Patrons;
        _jsonData.Loans = testData.Loans;

        var patron = _jsonData.Patrons!.First(p => p.Id == 1);

        // Act
        var populatedPatron = _jsonData.GetPopulatedPatron(patron);

        // Assert
        Assert.NotNull(populatedPatron);
        Assert.NotNull(populatedPatron.Loans);
        Assert.Single(populatedPatron.Loans); // Patron 1 should have 1 loan
        
        var loan = populatedPatron.Loans.First();
        Assert.NotNull(loan.BookItem);
        Assert.NotNull(loan.BookItem.Book);
        Assert.NotNull(loan.BookItem.Book.Author);
    }

    [Fact(DisplayName = "JsonData.GetPopulatedLoan: Returns loan with populated relationships")]
    public async Task GetPopulatedLoan_ReturnsLoanWithPopulatedRelationships()
    {
        // Arrange
        var testData = TestDataFactory.CreateMockJsonData();
        _jsonData.Authors = testData.Authors;
        _jsonData.Books = testData.Books;
        _jsonData.BookItems = testData.BookItems;
        _jsonData.Patrons = testData.Patrons;
        _jsonData.Loans = testData.Loans;

        var loan = _jsonData.Loans!.First(l => l.Id == 1);

        // Act
        var populatedLoan = _jsonData.GetPopulatedLoan(loan);

        // Assert
        Assert.NotNull(populatedLoan);
        Assert.NotNull(populatedLoan.Patron);
        Assert.Equal(loan.PatronId, populatedLoan.Patron.Id);
        
        Assert.NotNull(populatedLoan.BookItem);
        Assert.Equal(loan.BookItemId, populatedLoan.BookItem.Id);
        
        Assert.NotNull(populatedLoan.BookItem.Book);
        Assert.Equal(populatedLoan.BookItem.BookId, populatedLoan.BookItem.Book.Id);
        
        Assert.NotNull(populatedLoan.BookItem.Book.Author);
        Assert.Equal(populatedLoan.BookItem.Book.AuthorId, populatedLoan.BookItem.Book.Author.Id);
    }

    [Fact(DisplayName = "JsonData.SavePatrons: Saves patrons to file")]
    public async Task SavePatrons_SavesPatronsToFile()
    {
        // Arrange
        var patrons = TestDataFactory.CreateTestPatrons();

        // Act
        await _jsonData.SavePatrons(patrons);

        // Assert
        var savedContent = await File.ReadAllTextAsync(_tempFiles[3]);
        var savedPatrons = JsonSerializer.Deserialize<List<Patron>>(savedContent);
        
        Assert.NotNull(savedPatrons);
        Assert.Equal(2, savedPatrons.Count);
        Assert.Equal("John Doe", savedPatrons.First(p => p.Id == 1).Name);
        Assert.Equal("Jane Smith", savedPatrons.First(p => p.Id == 2).Name);
    }

    [Fact(DisplayName = "JsonData.SaveLoans: Saves loans to file")]
    public async Task SaveLoans_SavesLoansToFile()
    {
        // Arrange
        var loans = TestDataFactory.CreateTestLoans();

        // Act
        await _jsonData.SaveLoans(loans);

        // Assert
        var savedContent = await File.ReadAllTextAsync(_tempFiles[4]);
        var savedLoans = JsonSerializer.Deserialize<List<Loan>>(savedContent);
        
        Assert.NotNull(savedLoans);
        Assert.Equal(2, savedLoans.Count);
        Assert.Equal(1, savedLoans.First(l => l.Id == 1).BookItemId);
        Assert.Equal(2, savedLoans.First(l => l.Id == 2).PatronId);
    }

    public void Dispose()
    {
        foreach (var tempFile in _tempFiles)
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}
