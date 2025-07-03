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

## License

This project is licensed under the MIT License. See the LICENSE file for