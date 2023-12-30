## Create new Migration

```powershell
dotnet ef migrations add <MigrationName> 
--project ".\Blogger.Infrastructure.Database.csproj" 
--startup-project "..\Blogger.Presentation.WebAPI\Blogger.Presentation.WebAPI.csproj"
```
## Update Database with latest migration 

```powershell
dotnet ef database update  
--project ".\Blogger.Infrastructure.Database.csproj" 
--startup-project "..\Blogger.Presentation.WebAPI\Blogger.Presentation.WebAPI.csproj"
```
