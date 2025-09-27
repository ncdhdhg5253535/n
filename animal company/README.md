# Animal Company VR Mod

A VR modification for Animal Company game targeting Oculus devices.

## Overview

This mod adds VR support to Animal Company using BepInEx framework and Unity XR Toolkit, specifically optimized for Oculus headsets.

## Features

- Full VR immersion with hand tracking
- Oculus Touch controller support
- Teleportation and smooth locomotion
- VR-optimized UI interactions
- Animal interaction in 3D space

## Requirements

- Animal Company game installed
- Oculus VR headset (Quest 2, Quest Pro, Rift S, etc.)
- BepInEx modding framework
- Unity XR Plugin Management

## Installation

1. Install BepInEx in your Animal Company game directory
2. Copy mod files to `BepInEx/plugins/` folder
3. Launch game with VR headset connected
4. Enable VR mode in mod settings

## Development Setup

### Prerequisites

- Unity 2022.3 LTS or later
- Visual Studio 2022 with C# support
- Oculus Integration SDK
- BepInEx development environment

### Building the Mod

1. Open project in Unity
2. Install required packages via Package Manager
3. Build mod DLL using included build script
4. Deploy to game directory for testing

## Project Structure

```
AnimalCompanyVRMod/
├── src/                    # Source code
├── assets/                 # VR assets and prefabs
├── lib/                    # External libraries
├── build/                  # Build output
├── docs/                   # Documentation
└── tools/                  # Development tools
```

## Contributing

1. Fork the repository
2. Create feature branch
3. Implement VR improvements
4. Test with Oculus hardware
5. Submit pull request

## License

MIT License - See LICENSE file for details

## Support

For issues and support, please check the documentation or create an issue in the repository.