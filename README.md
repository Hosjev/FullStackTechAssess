# Context Anchor

This repository is a **deliberately scoped beta** intended to demonstrate:
- backend architecture clarity
- data modeling and aggregation
- pragmatic use of EF Core
- frontend consumption via Angular

The goal is **correctness and reasoning**, not overengineering.

---

## High-level Stack

- Backend: ASP.NET Core Web API
- ORM: EF Core (direct DbContext usage)
- Database: PostgreSQL (Docker)
- DB Seeding: Gated DbSeeder
- Frontend: Angular + RxJS
- Tests: scaffolded only (backend + frontend)

---

## Key Architectural Decisions

- **Thin controllers**
  - Controllers handle HTTP concerns only
  - Routing, status codes, and delegation
  - No business logic
- **Service layer present**
  - Business logic lives in Services
  - Aggregation and derived queries centralized
  - Keeps controllers simple and testable
- **Single Responsibility and Dependency Inversion**
  - Each service has a single, focused responsibility
  - Controllers depend on interfaces, not implementations
  - Enables substitution and future growth
- **Direct DbContext vs Repository pattern**
  - No repository abstraction
  - DbContext used directly in services
  - Reduces complexity and avoids duplicating EF Core abstraction
- **EF Core migrations**
  - Schema owned by migrations
  - Aligns with modern EF Core practices
- **Index Strategy**
  - Shared in backend README.md
- **Complex Endpoint**
  - Categories challenged looked more interesting in terms of query optimization
- **Validation**
  - Data Annotations used for request validation
  - Simple and native to EF Core
  - Explicitly designed to evolve later
- **More Time Allowance**
  - Deeper analysis of the domain and API optimization
  - Better database strategy (reads/writes separated)
  - API Pagination
  - Tracing wrapper for workflows

---

## Data Volume

- 5 categories
- 25 products per category
- ~125 products total

This volume is chosen to:
- make aggregation non-trivial
- surface N+1 risks
- justify query decisions

---

## Project Structure

- `backend/` – ASP.NET Core API
- `frontend/` – Angular app
- `docker-compose.yml` – local DB only

Each subproject has its own README to preserve decisions.

---

## Non-Goals (Explicit)

- No CI/CD
- No production infrastructure
- No premature architectural patterns (Clean/Hex/Onion)
- No auth implementation
