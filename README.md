# WeGotThis

A community goal-sharing app built with ASP.NET Core Razor Pages. Members submit goals to a shared pool, then help each other by completing or rejecting goals pulled from it each day. A token system keeps the pool balanced: you earn tokens by completing other people's goals and spend them to post your own.

Built as a learning project to get hands-on with C#, ASP.NET Core, and Entity Framework Core.

## Features

- **Accounts:** sign up and log in with cookie-based authentication.
- **Shared goal pool:** every submitted goal goes into a common pool, with live counters for total, completed, and rejected goals.
- **Daily goal list:** each day the app shows 5 goals from the pool, chosen with a date-based seed so the selection is stable throughout the day and refreshes at midnight UTC.
- **Complete or reject:** act on a goal once per 24 hours. Completing a goal earns you 2 tokens.
- **Token economy:** posting a goal costs 1 token.
- **Input limits:** goals are capped at 120 characters, enforced in the browser and on the server.
- **Daily quote:** a random self-improvement or Stoic quote (Marcus Aurelius, Seneca, Epictetus) from 10 seeded quotes.
- **Privacy policy page** covering the data the site collects (IP address, timestamps, cookies).
- **Custom styling:** a parchment-themed UI with fade-in animations and a responsive layout.

## Tech stack

- C# / .NET 10
- ASP.NET Core Razor Pages
- Entity Framework Core with SQLite
- Cookie authentication with a custom `MemberId` claim
- Plain CSS (no front-end framework beyond Bootstrap's defaults)

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- The EF Core CLI tool:

```bash
dotnet tool install --global dotnet-ef
```

### Run locally

```bash
git clone https://github.com/EliWorkedOnThat/WeGotThis.git
cd WeGotThis

# Create the database (see the note below if there is no Migrations folder)
dotnet ef database update

dotnet run
```

Then open the URL printed in the terminal (usually `https://localhost:xxxx`) and sign up for an account.

> **Note:** the `Migrations` folder may not be tracked in this repository. If `dotnet ef database update` reports that no migrations were found, generate them first:
>
> ```bash
> dotnet ef migrations add InitialCreate
> dotnet ef database update
> ```

### Resetting the database

With the app stopped, delete `wegotthis.db` and run `dotnet ef database update` again. All data is wiped and the seeded quotes are restored.

## Project structure

```
WeGotThis/
├── Data/            # AppDbContext and seed data
├── Models/          # Goal, GoalAction, Member, Pool, Quote
├── Pages/           # Razor Pages (Index, Login, Signup, Privacy, ...)
├── wwwroot/         # CSS, JS, static files
└── Program.cs       # App startup and configuration
```

## How the daily list works

The goal list uses a seeded random generator, with the current UTC date as the seed. Everyone sees the same 5 goals all day, and only goals created before today are eligible, so brand-new submissions appear the next day.

## Roadmap

- Admin page for adding and editing quotes
- "New quote" button
- Character counter on the goal input
- Deployment with a live demo link
