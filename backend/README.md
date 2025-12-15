# Backend – Context & Decisions

This backend is an ASP.NET Core Web API built as a **clear, explainable beta**.

---

## Structure

```text
src/Api/
├── Controllers/   // HTTP only
├── Services/      // business logic
├── Dtos/          // request/response shapes
├── Auth/          // placeholder only
├── Migrations/    // EF Core generated
├── AppDbContext.cs
├── Program.cs
```

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
