# .NET Domain Driven Deseign Template
This is a template for .NET projects using Domain Driven Deseing.

# Migrations
To create migrations:
```
dotnet ef migrations add InitialCreate --context EFProjectDDDContext --startup-project ..\EFProjectDDD.Presentation\ --output-dir Migrations
```

To update db:
```
dotnet ef database update --context AppDbContext --startup-project ../SS.Enki.Api
```

To revert migration:
```
dotnet ef migrations script <FromMigration> <ToMigration> --context AppDbContext --startup-project ../SS.Enki.Api --output revert.sql
```
