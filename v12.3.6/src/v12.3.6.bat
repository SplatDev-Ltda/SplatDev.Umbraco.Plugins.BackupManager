dotnet new install Umbraco.Templates::12.3.6
# Create solution/project
dotnet new sln --name "SplatDev.Umbraco.Plugin.v12.3.6"
dotnet new umbraco --force -n "SplatDev.Umbraco.Web"
dotnet sln add "SplatDev.Umbraco.Web"
dotnet new umbracopackage --force -n "SplatDev.Umbraco.Plugin.Backups"
dotnet sln add "SplatDev.Umbraco.Plugin.Backups"
dotnet run --project "SplatDev.Umbraco.Web"
#Running