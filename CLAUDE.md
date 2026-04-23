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

- Swagger UI available at `http://localhost:5123` (HTTP) or `https://localhost:7178` (HTTPS)
- No test project exists in this solution

## Architecture

The project uses a **4-layer clean architecture** with a Facade pattern for HTTP entry points:

```
API/Facades/         → HTTP endpoints (instead of Controllers)
Application/Services/ → Business logic
Domain/Entities/     → Database models
Infrastructure/Repository/ → Data access via Supabase Postgrest
Utils/               → Shared utilities (JWT, Email, OTP, DateTime)
```

### Adding a new feature

1. Entity in `Domain/Entities/` — use `[Table]`, `[Column]`, `[PrimaryKey]` attributes, extend `BaseModel`
2. Repository interface + class in `Infrastructure/Repository/` — extend `Repository<T>`; register in `Infrastructure/DependencyInjection.cs`
3. Service in `Application/Services/` — register as `Scoped` in `Application/DependencyInjection.cs`
4. Request/Response DTOs in `API/Contracts/Requests/` and `API/Contracts/Responses/`
5. Mapper extension method in `API/Contracts/Mappers/`
6. Facade class in `API/Facades/` — use `[ApiController]`, `[Route]` attributes

### Database & ORM

- **Database**: Supabase PostgreSQL (cloud-hosted)
- **ORM**: Supabase C# Postgrest client — entities must extend `BaseModel`
- **Client**: Singleton `Supabase.Client` injected via DI; repositories use `Client.Postgrest.Table<T>()`
- Common query pattern:
  ```csharp
  await Table.Filter("column", Constants.Operator.Equals, value).Get();
  await Table.Insert(entity);
  await Table.Filter("id", Constants.Operator.Equals, id).Update(entity);
  await Table.Filter("id", Constants.Operator.Equals, id).Delete();
  ```

### Authentication & Authorization

- **Auth provider**: Supabase Auth (email/password)
- **Tokens**: JWT Bearer tokens; extract with `Utils/AccessToken.GetAccessToken(context)`, validate with `AccessToken.GetUser(...)`
- **Roles**: Checked via `Utils/AuthorizeHelper.AuthorizeForEmployee(user)` — no ASP.NET `[Authorize]` attributes; authorization is manual in services
- **OTP flow**: 6-digit codes stored in `otp_code` table with 5-minute TTL, delivered via Gmail SMTP (MailKit)

### Key Utilities

- `Utils/DateTimeHelper.cs` — Vietnam timezone conversion (important: server runs in UTC, use this for all timestamps)
- `Utils/EmailService.cs` — Gmail SMTP OTP delivery
- `Utils/OTPGenerator.cs` — 6-digit OTP generation
- `Utils/AccessToken.cs` — JWT extraction and Supabase user validation

### Configuration

All credentials live in `appsettings.json`:
- `SupabaseUrl`, `SupabaseApiKey`, `SupabaseAdminKey` — Supabase connection
- `ChatbotUrl` — External AI chatbot service URL

CORS is configured to allow any origin (`AllowAnyOrigin`).
