# Animal Company VR Mod - Setup Instructions

## Prerequisites

### 1. Install BepInEx in Animal Company
1. Download BepInEx 5.x from [GitHub Releases](https://github.com/BepInEx/BepInEx/releases)
2. Extract BepInEx files to your Animal Company game directory
3. Run the game once to initialize BepInEx folders

### 2. Required VR Setup
1. Install SteamVR or Oculus software
2. Ensure your VR headset is connected and detected
3. Test VR functionality with other VR games

### 3. Development Environment (Optional)
1. Install Visual Studio 2022 or VS Code
2. Install .NET SDK 6.0 or later
3. Install Unity Hub and Unity 2022.3 LTS

## Installation

### Method 1: Pre-built Mod
1. Download `AnimalCompanyVR.dll` from releases
2. Copy to `Animal Company/BepInEx/plugins/`
3. Launch game with VR headset connected

### Method 2: Build from Source
1. Clone this repository
2. Copy Unity DLLs to `lib/` folder:
   - `UnityEngine.dll`
   - `UnityEngine.CoreModule.dll` 
   - `UnityEngine.XRModule.dll`
   - Unity XR Management DLLs
3. Run `build_mod.bat`
4. Copy `build/AnimalCompanyVR.dll` to game plugins folder

## Required Unity DLLs

Copy these files from your Unity installation to the `lib/` folder:

```
Unity Editor/Data/Managed/
├── UnityEngine.dll
├── UnityEngine.CoreModule.dll
├── UnityEngine.XRModule.dll
├── UnityEngine.InputLegacyModule.dll
├── UnityEngine.PhysicsModule.dll
└── UnityEngine.UI.dll

Unity Editor/Data/Resources/PackageManager/ProjectTemplates/
└── [XR Package DLLs]
```

Or from Animal Company game directory:
```
Animal Company_Data/Managed/
├── UnityEngine.dll
├── UnityEngine.CoreModule.dll
└── [Other Unity modules]
```

## Configuration

### VR Settings
The mod includes these configurable options:
- **Enable Teleportation**: Teleport-based movement
- **Enable Smooth Locomotion**: Analog stick movement
- **Locomotion Speed**: Movement speed multiplier
- **Interaction Distance**: Range for animal interactions

### Controls
- **Right Thumbstick**: Movement (if smooth locomotion enabled)
- **Left/Right Trigger**: Interact with objects/animals
- **Grip Buttons**: Additional actions (game-specific)

## Troubleshooting

### VR Not Starting
1. Verify SteamVR or Oculus is running
2. Check BepInEx console for errors
3. Ensure Unity XR packages are compatible

### Performance Issues
1. Lower game graphics settings
2. Close unnecessary applications
3. Check VR headset refresh rate settings

### Animals Not Responding
1. Verify mod patches are applying correctly
2. Check BepInEx logs for patch failures
3. Ensure game version compatibility

## Development Notes

### Modding Animal Company
This mod uses Harmony patching to integrate VR functionality:
- **Camera Patches**: Redirect rendering to VR display
- **Input Patches**: Replace keyboard/mouse with VR controllers
- **UI Patches**: Convert screen UI to world-space VR UI
- **Animal Patches**: Add VR-specific animal interactions

### Customization
To customize the mod:
1. Modify source files in `src/` directory
2. Adjust interaction distances and behaviors
3. Add custom VR mechanics specific to Animal Company
4. Rebuild using `build_mod.bat`

### Contributing
1. Test changes thoroughly in VR
2. Ensure compatibility with different VR headsets
3. Document any new features or configuration options
4. Submit pull requests with detailed descriptions

## Support

For issues and support:
1. Check existing GitHub issues
2. Provide BepInEx console logs
3. Include VR headset model and game version
4. Describe steps to reproduce the problem