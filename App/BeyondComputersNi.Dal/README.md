When managing migrations, append `-StartupProject BeyondComputersNi.Api` so the DbContext can have dependencies injected from the API project.
Also, set the Default Project in Package Manager Console to `BeyondComputersNi.Dal`
- `Add-Migration MigrationName -StartupProject BeyondComputersNi.Api`
- `Remove-Migration -StartupProject BeyondComputersNi.Api`
- `Update-Database -StartupProject BeyondComputersNi.Api`