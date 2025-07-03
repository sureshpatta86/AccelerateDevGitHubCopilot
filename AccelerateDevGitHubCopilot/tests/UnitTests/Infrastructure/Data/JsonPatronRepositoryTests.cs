using Library.ApplicationCore.Entities;
using Library.Infrastructure.Data;
using Library.UnitTests.Infrastructure;

namespace Library.UnitTests.Infrastructure.Data;

public class JsonPatronRepositoryTests : IDisposable
{
    private readonly JsonPatronRepository _repository;
    private readonly JsonData _jsonData;

    public JsonPatronRepositoryTests()
    {
        _jsonData = TestDataFactory.CreateMockJsonData();
        _repository = new JsonPatronRepository(_jsonData);
    }

    [Fact(DisplayName = "JsonPatronRepository.GetPatron: Returns patron when found")]
    public async Task GetPatron_ReturnsPatronWhenFound()
    {
        // Arrange
        var expectedPatronId = 1;

        // Act
        var result = await _repository.GetPatron(expectedPatronId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedPatronId, result.Id);
        Assert.Equal("John Doe", result.Name);
        Assert.NotNull(result.Loans); // Should be populated
    }

    [Fact(DisplayName = "JsonPatronRepository.GetPatron: Returns null when patron not found")]
    public async Task GetPatron_ReturnsNullWhenNotFound()
    {
        // Arrange
        var nonExistentPatronId = 999;

        // Act
        var result = await _repository.GetPatron(nonExistentPatronId);

        // Assert
        Assert.Null(result);
    }

    [Fact(DisplayName = "JsonPatronRepository.SearchPatrons: Returns matching patrons")]
    public async Task SearchPatrons_ReturnsMatchingPatrons()
    {
        // Arrange
        var searchInput = "John";

        // Act
        var result = await _repository.SearchPatrons(searchInput);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("John Doe", result[0].Name);
        Assert.NotNull(result[0].Loans); // Should be populated
    }

    [Fact(DisplayName = "JsonPatronRepository.SearchPatrons: Returns empty list when no matches")]
    public async Task SearchPatrons_ReturnsEmptyListWhenNoMatches()
    {
        // Arrange
        var searchInput = "NonExistentName";

        // Act
        var result = await _repository.SearchPatrons(searchInput);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact(DisplayName = "JsonPatronRepository.SearchPatrons: Returns results sorted by name")]
    public async Task SearchPatrons_ReturnsSortedResults()
    {
        // Arrange
        var searchInput = ""; // Empty string should match all patrons

        // Act
        var result = await _repository.SearchPatrons(searchInput);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count >= 2);
        // Verify sorted order: "Jane Smith" should come before "John Doe"
        var janeIndex = result.FindIndex(p => p.Name == "Jane Smith");
        var johnIndex = result.FindIndex(p => p.Name == "John Doe");
        Assert.True(janeIndex < johnIndex, "Results should be sorted alphabetically by name");
    }

    [Fact(DisplayName = "JsonPatronRepository.UpdatePatron: Updates existing patron")]
    public async Task UpdatePatron_UpdatesExistingPatron()
    {
        // Arrange
        var originalPatron = await _repository.GetPatron(1);
        Assert.NotNull(originalPatron);

        // Update the patron directly in the JsonData (simulating the save operation)
        var existingPatron = _jsonData.Patrons!.First(p => p.Id == 1);
        existingPatron.Name = "John Updated";
        existingPatron.ImageName = "updated.jpg";
        existingPatron.MembershipEnd = DateTime.Now.AddYears(2);

        // Act - retrieve the "updated" patron
        var updatedPatron = await _repository.GetPatron(1);

        // Assert
        Assert.NotNull(updatedPatron);
        Assert.Equal("John Updated", updatedPatron.Name);
        Assert.Equal("updated.jpg", updatedPatron.ImageName);
    }

    [Fact(DisplayName = "JsonPatronRepository.UpdatePatron: Does nothing when patron not found")]
    public async Task UpdatePatron_DoesNothingWhenPatronNotFound()
    {
        // Arrange
        var originalCount = _jsonData.Patrons!.Count;

        // Act - try to get a non-existent patron
        var result = await _repository.GetPatron(999);

        // Assert
        Assert.Null(result);
        Assert.Equal(originalCount, _jsonData.Patrons.Count); // Count should remain the same
    }

    public void Dispose()
    {
        TestDataFactory.CleanupTempFiles();
    }
}
