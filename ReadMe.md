TaskManager - WPF Clean Architecture Application



A modern Task Manager built using WPF (.NET 9), following Clean Architecture principles, MVVM pattern, and EF Core with PostgreSQL.



Architecture Overview



The solution is structured into four layers:

TaskManager.UI

TaskManager.Application

TaskManager.Domain

TaskManager.Infrastructure





Layer Responsibilities



1- UI Layer (WPF - MVVM)

\- WPF Views (XAML)

\- ViewModels

\- Commands

\- Data binding

\- No business logic



2- Application Layer

\- Business use cases

\- Service interfaces

\- DTOs

\- Orchestration logic



3- Domain Layer

\- Core business entities (`TodoTask`)

\- Enums (`Priority`, `TaskStatus`)

\- Validation logic (`TaskValidator`)

\- Domain methods (e.g. `MarkCompleted`, `Rename`)



4- Infrastructure Layer

\- EF Core DbContext

\- PostgreSQL configuration

\- Data persistence implementation



Tech Stack

\- .NET 9

\- WPF (Windows Presentation Foundation)

\- MVVM Pattern

\- Entity Framework Core

\- PostgreSQL (Npgsql provider)

\- Microsoft DI / Host Builder



Features (MVP)

1-Task Management

\- Create task

\- Update task

\- Delete task

\- Mark task as completed / reopened

\- View all tasks

completed/reopened



2- Domain Rules

\- All validation is handled inside `TaskValidator`

\- Domain entities encapsulate behavior (not just properties)

\- No direct UI or database logic in domain layer



3- Data Flow

UI -> ViewModel -> AppLayer -> Domain -> Infrastructure(Persistence)





Database



\- PostgreSQL

\- EF Core Code First

\- Migrations supported



How to Run



1- Setup PostgreSQL

Create database: TaskManagerDb



2- Update connection string

Make an .env file and create Connection string: 

Host=localhost;Port=5432;Database=TaskManagerDb;Username=postgres;Password=your\_password



3- Run migrations

*dotnet ef migrations add InitialCreate -p TaskManager.Infrastructure*

*dotnet ef database update -p TaskManager.Infrastructure*





\### 4. Start application

Run `TaskManager.UI`





Design Principles Used

\- SOLID Principles

\- DRY Principle

\- Clean Architecture

\- MVVM Pattern

\- Domain-Driven Design (lightweight)



Known Limitations (MVP)



\- No authentication / RBAC yet

\- No domain event dispatching

\- No async UI virtualization

\- No offline caching



Future Improvements

\- Role-Based Access Control (RBAC)

\- Domain Event Dispatcher

\- Logging + Audit trail

\- Task filtering \& search

\- Popup-based editing UI













