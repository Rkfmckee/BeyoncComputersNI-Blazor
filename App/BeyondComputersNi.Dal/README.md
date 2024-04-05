When managing migrations, append `-StartupProject BeyondComputersNi.Api` so the DbContext can have dependencies injected from the API project.
- `Add-Migration MigrationName -StartupProject BeyondComputersNi.Api`
- `Remove-Migration -StartupProject BeyondComputersNi.Api`
- `UpdateDatabase -StartupProject BeyondComputersNi.Api`