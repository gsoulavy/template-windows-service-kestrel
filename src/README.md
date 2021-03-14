# Windows Service Template

## Publish

```
dotnet publish -o ./out
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

`ServiceBasePath` => <path-of-the-published-app-installed-as-a-windows-service>
