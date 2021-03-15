# Windows Service Template
[![Continuous Ingegration](https://github.com/gsoulavy/template-windows-service-kestrel/actions/workflows/ci.yml/badge.svg)](https://github.com/gsoulavy/template-windows-service-kestrel/actions/workflows/ci.yml) [![Release to Nuget](https://github.com/gsoulavy/template-windows-service-kestrel/actions/workflows/release.yml/badge.svg)](https://github.com/gsoulavy/template-windows-service-kestrel/actions/workflows/release.yml) ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/GSoulavy.Template.WindowsService.Kestrel)

## Install the template
### From NuGet
```
dotnet new -i <NUGET_PACKAGE_ID>
```
### From file system directory
```
dotnet new -i <FILE_SYSTEM_DIRECTORY>
```
## Uninstall the template
The uninstall command, without any other parameters, will list all installed templates.
```
dotnet new -u
```
That command returns something similar to the following output:
```
Template Instantiation Commands for .NET CLI

Currently installed items:
  Microsoft.DotNet.Common.ItemTemplates
    Templates:
      global.json file (globaljson)
      NuGet Config (nugetconfig)
      Solution File (sln)
      Dotnet local tool manifest file (tool-manifest)
      Web Config (webconfig)
  Microsoft.DotNet.Common.ProjectTemplates.3.0
    Templates:
      Class library (classlib) C#
      Class library (classlib) F#
      Class library (classlib) VB
      Console Application (console) C#
      Console Application (console) F#
      Console Application (console) VB
...
```
The first level of items after `Currently installed items`: are the identifiers used in uninstalling a template. And in the example above, `Microsoft.DotNet.Common.ItemTemplates` and `Microsoft.DotNet.Common.ProjectTemplates.3.0` are listed. If the template was installed by using a file system path, this identifier will the folder path of the .template.config folder.
### Uninstalling a template
Use the dotnet new -u|--uninstall command to uninstall a package.

If the package was installed by either a NuGet feed or by a .nupkg file directly, provide the identifier.
```
dotnet new -u <NUGET_PACKAGE_ID>
```
If the package was installed by specifying a path to the .template.config folder, use that absolute path to uninstall the package. You can see the absolute path of the template in the output provided by the dotnet new -u command.
```
dotnet new -u <ABSOLUTE_FILE_SYSTEM_DIRECTORY>
```

More information on the template subject: `https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates`


## Publish

```
dotnet publish -o ./out
```

## Create a project using a custom template
After a template is installed, use the template by executing the `dotnet new <TEMPLATE>` command as you would with any other pre-installed template. You can also specify options to the `dotnet new` command, including template-specific options you configured in the template settings. Supply the template's short name directly to the command:
```
dotnet new <TEMPLATE> [-n <NAME>]
```
## Install as a Windows Service
```
sc.exe create <name-of-the-service> binpath= <path-to-the-published-app.exe>
```

## Other `sc.exe` commands

```
# start
sc.exe start <name-of-the-service>

# delete
sc.exe delete <name-of-the-service>

```

## Configurations

### EnvironmentVariable
When the service is installed as a windows service, the base path points to the system32
directory, therefore the base path has to be set with an environment variable:

`ServiceBasePath` => `<path-of-the-published-app-installed-as-a-windows-service>`

## `Properites/launchSettings.json`
```
{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "profiles": {
    "<PROJECT-NAME>": {
      "commandName": "Project",
      "dotnetRunMessages": "true", 
      "launchBrowser": true,
      "environmentVariables": {
          "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}

```
