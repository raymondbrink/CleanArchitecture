@ECHO OFF
SET outputFolder=..\.packed
SET version=6.3.0
SET nugetSource=https://api.nuget.org/v3/index.json

ECHO.
ECHO Packing all projects for version %version%...
ECHO.

dotnet pack ..\NetActive.CleanArchitecture.Application\NetActive.CleanArchitecture.Application.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Application.EntityFrameworkCore\NetActive.CleanArchitecture.Application.EntityFrameworkCore.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Application.Persistence.Interfaces\NetActive.CleanArchitecture.Application.Persistence.Interfaces.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Domain\NetActive.CleanArchitecture.Domain.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Domain.FluentValidation\NetActive.CleanArchitecture.Domain.FluentValidation.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Persistence\NetActive.CleanArchitecture.Persistence.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\NetActive.CleanArchitecture.Persistence.EntityFrameworkCore\NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.csproj --output %outputFolder% --version-suffix %version%
dotnet pack ..\..\template\NetActive.CleanArchitecture.Template.csproj --output %outputFolder% -p:PackageVersion=%version%

ECHO.
ECHO All packages created, pushing to %nugetSource%...
ECHO.

dotnet nuget push %outputFolder%\*.nupkg --source %nugetSource%

ECHO.
ECHO Cleaning up...
ECHO.

del /Q %outputFolder%\*.*

ECHO.
ECHO Done.
ECHO.