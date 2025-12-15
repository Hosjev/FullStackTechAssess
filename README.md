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
- Database: PostgreSQL (Docker, seeded)
- Frontend: Angular + RxJS
- Tests: scaffolded only (backend + frontend)

---

## Key Architectural Decisions

- **Thin controllers**
  - Controllers handle HTTP concerns only
- **Service layer present**
  - Business logic lives in Services
- **Direct DbContext usage**
  - No repository abstraction
- **EF Core migrations**
  - Applied at app startup via `db.Database.Migrate()`
- **Database seeding**
  - Performed by Postgres container on first startup (local dev)
- **Auth**
  - Scaffolded only (no implementation in beta)
- **Validation**
  - Data Annotations for now
  - Explicitly allowed to evolve later
- **Testing**
  - Projects scaffolded, implementations deferred

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
- No optimization beyond clarity
