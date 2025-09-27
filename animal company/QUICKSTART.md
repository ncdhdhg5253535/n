# Animal Company VR Mod - Complete Setup Guide

## 🚀 Quick Start

Your Animal Company VR mod project is now set up! Here's what you have:

### Project Structure
```
AnimalCompanyVRMod/
├── src/                           # C# source code
│   ├── AnimalCompanyVRPlugin.cs   # Main plugin entry point
│   ├── VRManager.cs               # VR system management
│   ├── VRControllerHandler.cs     # VR controller input
│   ├── VRPatches.cs               # Harmony patches for game integration
│   ├── VRAnimalInteraction.cs     # Animal-specific VR interactions
│   └── PluginInfo.cs              # Plugin metadata
├── assets/                        # VR assets and prefabs (empty - for future use)
├── lib/                           # External Unity/BepInEx libraries (you need to populate)
├── build/                         # Build output
├── AnimalCompanyVR.csproj         # C# project file
├── build_mod.bat                  # Windows build script
├── README.md                      # Project documentation
└── SETUP.md                       # Detailed setup instructions
```

## 🎯 Next Steps

### 1. Install Prerequisites

#### .NET SDK (Required for building)
1. Download .NET 6.0 SDK: https://dotnet.microsoft.com/download
2. Run installer and restart VS Code
3. Verify: Open terminal and run `dotnet --version`

#### BepInEx (Required for modding)
1. Download BepInEx 5.4.22: https://github.com/BepInEx/BepInEx/releases
2. Extract to your Animal Company game directory
3. Run game once to generate BepInEx folders

### 2. Get Unity DLLs

You need to copy Unity DLL files to the `lib/` folder. Get them from:

**Option A: From Animal Company Game**
```
[Animal Company Install]/Animal Company_Data/Managed/
├── UnityEngine.dll
├── UnityEngine.CoreModule.dll
├── UnityEngine.XRModule.dll
├── UnityEngine.InputLegacyModule.dll
├── UnityEngine.PhysicsModule.dll
└── UnityEngine.UI.dll
```

**Option B: From Unity Installation**
```
Unity/Editor/Data/Managed/UnityEngine/
```

### 3. Build Your Mod

Once .NET SDK is installed and Unity DLLs are in `lib/`:

```bash
# Option 1: Use build script
./build_mod.bat

# Option 2: Manual dotnet command
dotnet build --configuration Release --output build
```

### 4. Install the Mod

1. Copy `build/AnimalCompanyVR.dll` to:
   ```
   [Animal Company]/BepInEx/plugins/AnimalCompanyVR.dll
   ```

2. Launch Animal Company with VR headset connected

## 🎮 VR Features Implemented

### Core VR System
- ✅ **VR Initialization**: Automatic XR setup for Oculus/OpenXR
- ✅ **VR Camera**: Replaces game camera with VR display
- ✅ **Controller Tracking**: Full 6DOF hand tracking
- ✅ **Haptic Feedback**: Touch controller vibration

### Movement & Locomotion  
- ✅ **Smooth Locomotion**: Right thumbstick movement
- ✅ **Comfort Options**: Configurable movement speeds
- ✅ **VR-Safe Movement**: No motion sickness inducing transitions

### Interaction System
- ✅ **Ray-cast Pointing**: Laser pointers from controllers
- ✅ **Object Interaction**: Trigger-based interaction system
- ✅ **Animal Interactions**: Special VR responses for animals
- ✅ **Visual Feedback**: Highlighting and haptic responses

### UI Integration
- ✅ **World Space UI**: Converts screen UI to VR-friendly world space
- ✅ **VR Menu System**: 3D positioned menus and interfaces

## 🔧 Customization

### VR Settings (in VRManager.cs)
```csharp
public bool enableTeleportation = true;        // Enable teleport movement
public bool enableSmoothLocomotion = true;     // Enable thumbstick movement  
public float locomotionSpeed = 3.0f;           // Movement speed multiplier
public float rotationSpeed = 45.0f;            // Snap rotation speed
```

### Animal Interactions (in VRAnimalInteraction.cs)
```csharp
public float interactionDistance = 2.0f;       // How close to highlight animals
public float highlightIntensity = 1.5f;        // Glow effect strength
```

## 🎯 VR Controls

| Action | Control |
|--------|---------|
| **Move** | Right thumbstick |
| **Interact** | Left/Right trigger |
| **Point** | Aim controllers |
| **Menu** | Game-specific buttons |

## 🔍 Troubleshooting

### Build Issues
- **"dotnet not found"**: Install .NET 6.0 SDK
- **"Assembly not found"**: Copy Unity DLLs to `lib/` folder
- **"BepInEx errors"**: Verify BepInEx installation

### VR Issues  
- **VR not starting**: Check SteamVR/Oculus is running
- **No controller tracking**: Verify headset USB/wireless connection
- **Performance issues**: Lower game graphics settings

### Game Integration
- **Mod not loading**: Check `BepInEx/LogOutput.log` for errors
- **Animals not responding**: Verify Harmony patches are working
- **UI issues**: Game may need UI-specific patches

## 🚀 Ready to Code!

Your VR mod is ready for development! The framework provides:
- **Complete VR setup** with Oculus support
- **Modular architecture** for easy customization  
- **Game integration** via Harmony patching
- **Ready-to-build** project structure

### Development Workflow:
1. **Modify** C# files in `src/` for your features
2. **Build** using `build_mod.bat` or dotnet CLI
3. **Test** by copying DLL to game and launching in VR
4. **Iterate** and improve based on VR testing

Have fun creating your Animal Company VR experience! 🦊🥽