# Short Clips Web

## Installation
Install node.js in version v20.17.0.
Once installed, open cmd and type: 
```bash
node version
```

Install Angular CLI (v18.2.3) by running the command below:
```bash
npm install -g @angular/cli@18.2.3
ng version
```

Install Microsoft SSMS.
Install Visual Studio 2022.
Install Visual Studio Code.

## Run

### API
1. Open Visual Studio.
1. Open solution.
1. Open `appSettings.json`.
1. In appSettings, add project root path to `"ProjectRootPath"`. Template: `C:\\Users\\<user>\\Documents\\<folder>\\`.
1. Remain in appSettings, add Sql Server Connection to `"SqlServerConnection"`. Template: `Server=<servername>;Database=shortclipsdb;Trusted_Connection=True;TrustServerCertificate=True;`.
1. Restore Nuget Packages.
2. Build solution.
1. Run migrations by typing `update-database` in Package Manager Console.
1. Run SQL scripts to populate database. Scripts can be found at `\ShortClips\ShortClipsWeb\utilities\sql` folder.
1. Run API project.

### Angular
1. Open Visual Studio Code.
1. Open a new terminal and type `npm install`.
1. In Explorer, open `src/environments/environment.development`. Replace the API URL if server has been changed.
