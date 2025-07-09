<div align="center">
   <h1>TaskDisc - Task Manager</h1>

![GitHub last commit (branch)](https://img.shields.io/github/last-commit/ArthurSchallenberger/TaskDisc/master)
[![Code Coverage](https://img.shields.io/badge/coverage-77.5%25-green)](https://github.com/ArthurSchallenberger/TaskDisc)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com/ArthurSchallenberger/TaskDisc)
</div>

<div align="center">
  <strong>Built with the tools and technologies:</strong>
</br></br>

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript)](https://www.javascript.com/)
[![NuGet](https://img.shields.io/badge/NuGet-004880?logo=nuget)](https://www.nuget.org/)
[![npm](https://img.shields.io/badge/npm-CB3837?logo=npm)](https://www.npmjs.com/)
[![Axios](https://img.shields.io/badge/Axios-5A29E4?logo=axios)](https://axios-http.com/)
[![discord.js](https://img.shields.io/badge/discord.js-14.15.3-5865F2?logo=discord)](https://discord.js.org/)
</br></br>
</div>


<div align="center">
  <img src="https://github.com/user-attachments/assets/cb026c14-6e80-40f9-a29a-d9a7c7114c18" alt="esseaqui2">
</div>
</br></br>

TaskDisc is a project built following the **Clean Architecture** pattern, ensuring separation of concerns, testability, and ease of maintenance. It consists of three main components:

- **Backend/API**: Developed in .NET 8, handling business logic, data persistence, and RESTful endpoints.
- **Frontend**: Implemented as interactive modals in Discord, leveraging the [discord.js](https://discord.js.org/) library.
- **Tests**: A dedicated solution for unit tests to ensure system quality and robustness.

---

## Project Structure

```
TaskDisc/
├── TaskDisc/                    # Main project (ASP.NET Core 8 API)
│   ├── Controllers/             # API controllers for handling requests
│   ├── Domain/                  # Business logic and entities
│   ├── Application/             # Application services and logic
│   ├── Infrastructure/          # Data access, external services, and configurations
│   └── appsettings.json         # Configuration settings for the API
├── ModalDiscord/                # Frontend (Discord bot with discord.js)
│   ├── index.js                 # Main entry point for the Discord bot
│   ├── .env                     # Environment variables for bot configuration
│   └── ...                      # Additional bot-related files
├── Tests/                       # Unit testing solution
│   └── UnitTests/               # Unit test projects
│       └── AuthControllerTests.cs # Tests for authentication controller
└── README.md                    # Project documentation
```

---

## Backend (.NET 8)

- **Clean Architecture Pattern**: Clear separation between domain, application, infrastructure, and presentation layers.
- **RESTful API**: Endpoints for task management, authentication, and more.
- **Configuration**: Environment variables and settings managed in `appsettings.json`.

### How to Run

1. Install the [.NET 8 SDK](https://dotnet.microsoft.com/download).
2. Navigate to the `TaskDisc/` directory and run:
   ```bash
   dotnet run
   ```
3. The API will be available at `http://localhost:7012` (or the configured port).

---

## Frontend (Discord.js)

- **Discord Bot**: Interacts with users via modals and commands, consuming the .NET API.
- **How to create Discord Bot**: [Documentation](https://gist.github.com/h0gsmoke/3071a638a4bf7b340cc4723cc4bc7cc7).
- **Configuration**: Set the following variables in the `.env` file:
  - `TOKEN`: Discord bot token
  - `CLIENT_ID`: Discord application ID
  - `GUILD_ID`: Discord server ID
  - `API_URL`: URL of the .NET API

### How to Run

1. Install [Node.js](https://nodejs.org/).
2. Navigate to the `ModalDiscord/` directory and install dependencies:
   ```bash
   npm install
   ```
3. Run the bot:
   ```bash
   npm run start
   ```

---

## Tests

- **Unit Tests**: Implemented in C# using frameworks like xUnit or NUnit.
- **Coverage**: Tests for controllers, services, and business rules.

### How to Run Tests

1. Navigate to the `Tests/` directory and run:
   ```bash
   dotnet test
   ```
