# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Install dependencies
dotnet restore

# Run in development
dotnet run

# Run with hot reload
dotnet watch run

# Build for release
dotnet build -c Release

# Docker build and run
docker build -t co4029-be:latest .
docker run -p 80:80 co4029-be:latest
```

- Swagger UI is the default page at `http://localhost:5123` (HTTP) or `https://localhost:7178` (HTTPS)
- No test project exists in this solution

## Architecture

The project uses a **4-layer clean architecture** with a Facade pattern for HTTP entry points:

```
API/Facades/          → HTTP endpoints (instead of Controllers)
Application/Services/ → Business logic
Domain/Entities/      → Database models
Infrastructure/Repository/ → Data access via Supabase Postgrest
Utils/                → Shared utilities (JWT, Email, OTP, DateTime)
```

### Namespace convention

Files mix two namespace roots — **do not change either**:
- `AgenticAR.*` — Infrastructure, Application, and some Repository namespaces
- `CO4029_BE.*` — API, Domain, Utils namespaces

### Adding a new feature

1. Entity in `Domain/Entities/` — extend `BaseModel`, use `[Table]`, `[Column]`, `[PrimaryKey]` attributes
2. Repository interface + class in `Infrastructure/Repository/` — extend `Repository<T>`; register in `Infrastructure/DependencyInjection.cs`
3. Service in `Application/Services/` — register as `Scoped` in `Application/DependencyInjection.cs`
4. Request/Response DTOs in `API/Contracts/Requests/` and `API/Contracts/Responses/`
5. Mapper extension method in `API/Contracts/Mappers/`
6. Facade class in `API/Facades/` — use `[ApiController]`, `[Route]` attributes

### Database & ORM

- **Database**: Supabase PostgreSQL (cloud-hosted)
- **ORM**: Supabase C# Postgrest client — entities must extend `BaseModel`
- **Client**: Singleton `Supabase.Client` registered in `Infrastructure/DependencyInjection.cs`; repositories use `Client.Postgrest.Table<T>()` via the base `Repository<T>` property `Table`
- Common query pattern:
  ```csharp
  await Table.Filter("column", Constants.Operator.Equals, value).Get();
  await Table.Filter("column", Constants.Operator.Equals, value).Single();
  await Table.Insert(entity);
  await Table.Filter("id", Constants.Operator.Equals, id).Update(entity);
  await Table.Filter("id", Constants.Operator.Equals, id).Delete();
  ```

### Authentication & Authorization

- **Auth provider**: Supabase Auth (email/password)
- **Tokens**: JWT Bearer tokens; extract with `AccessToken.GetAccessToken(HttpContext)` in facades, validate with `AccessToken.GetUser(client, token)` in services
- **Roles**: Checked via `AuthorizeHelper.AuthorizeForEmployee(user)` — no ASP.NET `[Authorize]` attributes; authorization is manual in services
- **OTP flow**: 6-digit codes stored in `otp_code` table with 5-minute TTL, delivered via Gmail SMTP (MailKit)

### Facade conventions

- Facades inject concrete service classes (not interfaces), e.g. `HistoryService`, not `IHistoryService`
- Error handling pattern: wrap every action in try/catch; return `BadRequest(ex.Message)` for auth/validation errors and `StatusCode(500, new { message = ex.Message })` for unexpected errors

### Key Utilities

- `Utils/DateTimeHelper.cs` — Vietnam timezone conversion; **always use this for timestamps** (server runs UTC)
- `Utils/EmailService.cs` — Gmail SMTP OTP delivery
- `Utils/OTPGenerator.cs` — 6-digit OTP generation
- `Utils/AccessToken.cs` — JWT extraction and Supabase user validation

### External service

`ChatbotApiService` calls an external AI chatbot via `HttpClient` (registered with `AddHttpClient<ChatbotApiService>()`).

### Configuration

All credentials live in `appsettings.json`:
- `SupabaseUrl`, `SupabaseApiKey`, `SupabaseAdminKey` — Supabase connection
- `ChatbotUrl`, `ChatbotApiKey` — External AI chatbot service

CORS is configured to allow any origin (`AllowAnyOrigin`). HTTPS redirect is disabled in development.
