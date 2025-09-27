@echo off
echo Building Animal Company VR Mod...

REM Clean previous build
if exist "build\AnimalCompanyVR.dll" del "build\AnimalCompanyVR.dll"

REM Build the project
dotnet build --configuration Release --output build

if %ERRORLEVEL% EQU 0 (
    echo Build completed successfully!
    echo Output: build\AnimalCompanyVR.dll
    echo.
    echo To install:
    echo 1. Copy build\AnimalCompanyVR.dll to your Animal Company game folder
    echo 2. Place it in BepInEx\plugins\ directory
    echo 3. Launch the game with your VR headset connected
) else (
    echo Build failed!
    pause
)

pause