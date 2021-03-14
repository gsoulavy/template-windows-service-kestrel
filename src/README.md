# Windows Service Template

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
    "<PROJECT.NAME>": {
      "commandName": "Project",
      "environmentVariables": {
          "DOTNET_ENVIRONMENT": "Development"
      }
    }
  }
}

```
