@ECHO OFF
SET outputFolder=..\.packed
SET version=6.0.1
SET nugetSource=http://nuget.home.rbrink.nl/nuget

ECHO.
ECHO Packing all projects for version %version%...
ECHO.

dotnet pack ..\NetActive.CleanArchitecture.Autofac\NetActive.CleanArchitecture.Autofac.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Domain\NetActive.CleanArchitecture.Domain.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Domain.FluentValidation\NetActive.CleanArchitecture.Domain.FluentValidation.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Application\NetActive.CleanArchitecture.Application.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Application.EntityFrameworkCore\NetActive.CleanArchitecture.Application.EntityFrameworkCore.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Persistence\NetActive.CleanArchitecture.Persistence.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Persistence.EntityFrameworkCore\NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.csproj --output %outputFolder% --version-suffix %version%

ECHO.
ECHO All packages created, pushing to %nugetSource%...
ECHO.

dotnet nuget push %outputFolder%\*.nupkg --source %nugetSource%
del %outputFolder%\*.nupkg

ECHO.
ECHO Done.
ECHO.