# Overview

ASP.NET Core Web API using EF Core and PostgreSQL.

This backend implements Products and Categories with soft deletes, derived
summary data, database-enforced correctness, and a clear growth path.  
It is intentionally scoped as a **beta**: complete, explainable, and easy to extend.

## Project Structure

```text
├── backend.sln
├── docker-compose.yml
├── README.md
├── docker/
├── src/
│   └── Api/
│       ├── Auth/
│       ├── Controllers/
│       ├── Dtos/
│       ├── Entities/
│       ├── Middleware/
│       ├── Migrations/
│       ├── Services/
│       ├── AppDbContext.cs
│       ├── Program.cs
│       └── Api.csproj
└── tests/
    └── Api.Tests/
        └── Api.Tests.csproj
```

### Rules

- Controllers are thin.
- All querying, aggregation, and calculations live in Services.
- Entities represent stored domain facts.
- DTOs represent API contracts only.

## Domain Model Overview

### Products

Products represent sellable inventory.

Stored (authoritative):
- Name
- Price
- StockQuantity
- IsActive
- timestamps

Behavior:
- Soft-deleted via IsActive = false
- Inventory state is represented directly by StockQuantity

### Categories

Higher construct where we categorize products.

Stored (authoritative):
- Name
- IsActive
- timestamps

Behavior:
- Soft-deleted via IsActive = false

## API Endpoints

### Products

- **GET** `/api/products`  
  Returns all active products with embedded category `{ id, name }`.

- **GET** `/api/products/{id}`  
  Returns a single active product.  
  `404` if not found or inactive.

- **POST** `/api/products`  
  Creates a new active product.  
  Duplicate (unique constraint) → `409 Conflict`.

- **PUT** `/api/products/{id}`  
  Updates an active product.  
  `404` if not found or inactive.

- **DELETE** `/api/products/{id}`  
  Soft delete (`IsActive = false`).  
  `404` if not found or inactive.

### Categories

- **GET** `/api/categories`  
  Returns all active categories.

- **POST** `/api/categories`  
  Creates a new active category.  
  Duplicate (unique constraint) → `409 Conflict`.

- **DELETE** `/api/categories/{id}`  
  Soft delete (`IsActive = false`).  
  `404` if not found or inactive.

- **GET** `/api/categories/{id}/summary`  
  Returns a derived category summary.

## Validation and Error Handling

- Request validation uses data annotations on DTOs.
- Invalid input returns `400 Bad Request`.
- Uniqueness is enforced at the database level via unique indices.
- A global exception-handling middleware translates database uniqueness
  violations into `409 Conflict`.
- Controllers do not perform error mapping.

## Database and Migrations

- Schema, indices and constraints are owned by EF Core migrations.
- The database is PostgreSQL.
- Migrations must be applied before seeding or running the API.

### Indexing

Indices match actual access patterns:
- products (is_active, id)
- products (category_id, is_active)
- partial: products (category_id) WHERE is_active = true
- categories (is_active)

Uniqueness:
- categories (name) unique
- products (category_id, name) unique

## Local Development

Ensure the required .NET SDK is installed with all dependencies.

Start dependencies:

```bash
docker compose up -d
dotnet build
dotnet ef database update
dotnet run --project src/Api
```

## Testing
Mostly deferred but scaffolded.

## Optional: LLM / Claude Integration (Future)

This project may optionally integrate an LLM (e.g., Claude) via event-based hooks to:

- review architectural changes
- detect drift between README intent and implementation
- summarize changes across iterations

This is **explicitly deferred** for the beta to avoid:
- premature coupling
- design bias from tooling
- unnecessary cognitive overhead

If added later, the LLM would act as a **reviewer and historian**, not a decision-maker.

## Deferred

- Authentication / authorization
- CI/CD
- Production infrastructure
- Advanced inventory modeling

These are intentionally out of scope for the beta.