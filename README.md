# Library App

## Description

Library App is a comprehensive library management system built with .NET that allows librarians to manage patrons, books, and loan operations. The application provides functionality for patron search, membership renewal, book loan tracking, and return processing through a console-based interface.

## Project Structure

- [GuidedProjectApp.sln](GuidedProjectApp.sln)
- [README.md](README.md)
- AccelerateDevGitHubCopilot/
  - src/
    - Library.ApplicationCore/
      - [Library.ApplicationCore.csproj](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Library.ApplicationCore.csproj)
      - Entities/
        - [Author.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Entities/Author.cs)
        - [Book.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Entities/Book.cs)
        - [BookItem.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Entities/BookItem.cs)
        - [Loan.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Entities/Loan.cs)
        - [Patron.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Entities/Patron.cs)
      - Enums/
        - [EnumHelper.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Enums/EnumHelper.cs)
        - [LoanExtensionStatus.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Enums/LoanExtensionStatus.cs)
        - [LoanReturnStatus.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Enums/LoanReturnStatus.cs)
        - [MembershipRenewalStatus.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Enums/MembershipRenewalStatus.cs)
      - Interfaces/
        - [ILoanRepository.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Interfaces/ILoanRepository.cs)
        - [ILoanService.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Interfaces/ILoanService.cs)
        - [IPatronRepository.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Interfaces/IPatronRepository.cs)
        - [IPatronService.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Interfaces/IPatronService.cs)
      - Services/
        - [LoanService.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Services/LoanService.cs)
        - [PatronService.cs](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Services/PatronService.cs)
    - Library.Console/
      - [Library.Console.csproj](AccelerateDevGitHubCopilot/src/Library.Console/Library.Console.csproj)
      - [appSettings.json](AccelerateDevGitHubCopilot/src/Library.Console/appSettings.json)
      - [CommonActions.cs](AccelerateDevGitHubCopilot/src/Library.Console/CommonActions.cs)
      - [ConsoleApp.cs](AccelerateDevGitHubCopilot/src/Library.Console/ConsoleApp.cs)
      - [ConsoleState.cs](AccelerateDevGitHubCopilot/src/Library.Console/ConsoleState.cs)
      - [Program.cs](AccelerateDevGitHubCopilot/src/Library.Console/Program.cs)
      - Json/
        - [Authors.json](AccelerateDevGitHubCopilot/src/Library.Console/Json/Authors.json)
        - [Books.json](AccelerateDevGitHubCopilot/src/Library.Console/Json/Books.json)
        - [BookItems.json](AccelerateDevGitHubCopilot/src/Library.Console/Json/BookItems.json)
        - [Loans.json](AccelerateDevGitHubCopilot/src/Library.Console/Json/Loans.json)
        - [Patrons.json](AccelerateDevGitHubCopilot/src/Library.Console/Json/Patrons.json)
    - Library.Infrastructure/
      - [Library.Infrastructure.csproj](AccelerateDevGitHubCopilot/src/Library.Infrastructure/Library.Infrastructure.csproj)
      - Data/
        - [JsonData.cs](AccelerateDevGitHubCopilot/src/Library.Infrastructure/Data/JsonData.cs)
        - [JsonLoanRepository.cs](AccelerateDevGitHubCopilot/src/Library.Infrastructure/Data/JsonLoanRepository.cs)
        - [JsonPatronRepository.cs](AccelerateDevGitHubCopilot/src/Library.Infrastructure/Data/JsonPatronRepository.cs)
  - tests/
    - UnitTests/
      - [UnitTests.csproj](AccelerateDevGitHubCopilot/tests/UnitTests/UnitTests.csproj)
      - [LoanFactory.cs](AccelerateDevGitHubCopilot/tests/UnitTests/LoanFactory.cs)
      - [PatronFactory.cs](AccelerateDevGitHubCopilot/tests/UnitTests/PatronFactory.cs)
      - ApplicationCore/
        - LoanService/
          - [ExtendLoan.cs](AccelerateDevGitHubCopilot/tests/UnitTests/ApplicationCore/LoanService/ExtendLoan.cs)
          - [ReturnLoan.cs](AccelerateDevGitHubCopilot/tests/UnitTests/ApplicationCore/LoanService/ReturnLoan.cs)
        - PatronService/
          - [RenewMembership.cs](AccelerateDevGitHubCopilot/tests/UnitTests/ApplicationCore/PatronService/RenewMembership.cs)
      - Infrastructure/
        - [TestDataFactory.cs](AccelerateDevGitHubCopilot/tests/UnitTests/Infrastructure/TestDataFactory.cs)
        - Data/
          - [JsonDataTests.cs](AccelerateDevGitHubCopilot/tests/UnitTests/Infrastructure/Data/JsonDataTests.cs)
          - [JsonLoanRepositoryTests.cs](AccelerateDevGitHubCopilot/tests/UnitTests/Infrastructure/Data/JsonLoanRepositoryTests.cs)
          - [JsonPatronRepositoryTests.cs](AccelerateDevGitHubCopilot/tests/UnitTests/Infrastructure/Data/JsonPatronRepositoryTests.cs)
          - [RepositoryTestBase.cs](AccelerateDevGitHubCopilot/tests/UnitTests/Infrastructure/Data/RepositoryTestBase.cs)

## Key Classes and Interfaces

- **[`Patron`](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Entities/Patron.cs)**  
  Represents a library patron with membership details and loan history.

- **[`Book`](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Entities/Book.cs)**  
  Represents a book in the library with title, author, genre, and ISBN information.

- **[`Loan`](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Entities/Loan.cs)**  
  Represents a book loan with due dates, return status, and associated patron and book item.

- **[`IPatronService`](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Interfaces/IPatronService.cs)**  
  Interface defining patron-related operations such as membership renewal.

- **[`ILoanService`](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Interfaces/ILoanService.cs)**  
  Interface defining loan operations including book returns and loan extensions.

- **[`PatronService`](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Services/PatronService.cs)**  
  Service implementing patron management functionality including membership renewal logic.

- **[`LoanService`](AccelerateDevGitHubCopilot/src/Library.ApplicationCore/Services/LoanService.cs)**  
  Service implementing loan management operations with business rules for extensions and returns.

- **[`JsonData`](AccelerateDevGitHubCopilot/src/Library.Infrastructure/Data/JsonData.cs)**  
  Data access layer for JSON file-based storage of library data.

## Testing Architecture

This project implements comprehensive unit testing across both ApplicationCore and Infrastructure layers using **xUnit**, **NSubstitute**, and **Coverlet** for code coverage.

### Test Organization

- **ApplicationCore Tests**: Focus on business logic validation
  - Service method testing (LoanService, PatronService)
  - Business rule verification
  - Mock repository dependencies

- **Infrastructure Tests**: Focus on data access layer validation
  - Repository operations testing
  - JSON data loading and saving
  - Entity relationship population
  - File I/O operations

### Testing Patterns

- **AAA Pattern**: Arrange-Act-Assert structure for all tests
- **Factory Pattern**: Consistent test data creation with `TestDataFactory`, `LoanFactory`, and `PatronFactory`
- **Descriptive Naming**: Clear test method names with `DisplayName` attributes
- **Isolation**: Each test runs independently with mocked dependencies
- **Comprehensive Coverage**: Multiple scenarios per method (happy path, edge cases, error conditions)

### Test Statistics

- **Total Tests**: 37 (17 ApplicationCore + 20 Infrastructure)
- **ApplicationCore Tests**: 17 tests covering LoanService and PatronService
- **Infrastructure Tests**: 20 tests covering JsonData, JsonPatronRepository, and JsonLoanRepository
- **Test Execution**: Fast in-memory execution with deterministic results
- **Coverage Areas**: Business logic, data operations, entity relationships, error handling

## Usage

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/library-app.git
   ```

2. Navigate to the project directory:
   ```sh
   cd AccelerateDevGitHubCopilot
   ```

3. Build the solution:
   ```sh
   dotnet build
   ```

4. Run the console application:
   ```sh
   dotnet run --project src/Library.Console
   ```

5. Run the unit tests:
   ```sh
   dotnet test
   ```

6. Run specific test categories:
   ```sh
   # Run only ApplicationCore tests
   dotnet test --filter "FullyQualifiedName~ApplicationCore"
   
   # Run only Infrastructure tests
   dotnet test --filter "FullyQualifiedName~Infrastructure"
   ```

7. Generate test coverage report:
   ```sh
   dotnet test --collect:"XPlat Code Coverage"
   ```

## Recent Updates

### Infrastructure Unit Tests (Latest)
- **Added comprehensive Infrastructure layer testing** with 20 new unit tests
- **JsonPatronRepository Tests**: 7 tests covering patron search, retrieval, and update operations
- **JsonLoanRepository Tests**: 6 tests covering loan operations and relationship preservation
- **JsonData Tests**: 7 tests covering data loading, saving, and entity population
- **Enhanced test infrastructure** with `TestDataFactory` for consistent test data creation
- **Improved test coverage** from 17 to 37 total tests, covering both business logic and data access layers

### Key Testing Features Added
- **Isolation**: Tests run independently with mocked dependencies
- **File I/O Testing**: Validates JSON serialization/deserialization operations
- **Relationship Testing**: Ensures entity relationships are properly maintained
- **Error Handling**: Tests cover missing entities and invalid operations
- **Performance**: Fast execution with in-memory test data

## License

This project is licensed under the MIT License. See the LICENSE file for