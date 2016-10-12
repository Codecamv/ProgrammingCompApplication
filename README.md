# ProgrammingCompApplication

## Nuget:
- https://forums.asp.net/t/2089929.aspx?Using+Signalr+3+in+a+ASP+NET+Core+MVC6+project+
- The version of signalR I am using is not available on nuget anymore.
- To make this application compile you have to:
  - Copy folders inside "/MissingNugetPackages" from source
  - Paste into your global nuget folder "..\Users\\[YourUserFolder]\\.dnx\packages
  - Right click on "References" in the project and select "Restore packages"
