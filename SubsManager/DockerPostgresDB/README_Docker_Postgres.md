# Docker Postgres for SubsManager

## 1) Files
- `.env` — credentials & ports
- `docker-compose.yml` — Postgres service (+ optional pgAdmin)
- `init.sql` — executed at first start (uuid extension enabled)

## 2) Start
```bash
docker compose up -d
docker compose ps
```

## 3) Connection string (appsettings.Development.json)
```json
{
  "Database": { "Provider": "postgres" },
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=subs_mgr;Username=subs_user;Password=subs_pw"
  }
}
```

## 4) Test with psql
```bash
docker exec -it subs-postgres psql -U subs_user -d subs_mgr -c "\l"
```

## 5) Stop & clean
```bash
docker compose down
# to wipe data:
docker volume rm docker-postgres-subs_subs_pg_data
```

## 6) Using pgAdmin (optional)
- Uncomment the `pgadmin` service in `docker-compose.yml` and run `docker compose up -d`.
- Open http://localhost:5050 — login with `PGADMIN_DEFAULT_EMAIL` / `PGADMIN_DEFAULT_PASSWORD`
- Add a new server pointing to `db` host, port `5432`, user `${POSTGRES_USER}`, password `${POSTGRES_PASSWORD}`.

## 7) EF Core migrations (example)
From `src/SubsManager.Api/`:
```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add Initial --project ../SubsManager.Infrastructure --startup-project .
dotnet ef database update --project ../SubsManager.Infrastructure --startup-project .
```
