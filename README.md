### Cardgame demo assignment project

Cardgame project is written using .net core SDK 3.1. It consists of two parts : library (located at cardgame/src/lib) and executable (located at cardgame/src/game). There are also tests (at cardgame/tests) which can be run as follows :

On linux:

`cd tests && dotnet test`

On windows:

```
  cd tests
  dotnet test
```

In order to compile projects, do as follows :

executable:

`dotnet build /src/game/crazycards.csproj`

library:

`dotnet build /src/lib/game.lib.csproj`

### Project dependencies

- autofac 5.2.0
- nlog 4.7.2
- nlog.config 4.7.2
- YamlDotNet 8.1.2
- xunit 2.4.1
- xunit.runner.visualstudio 2.4.2

Those are nuget packages and .net core will automatically take care of them upon build. You can always manually restore them with

`dotnet restore`

from corresponding .csproj directory.
