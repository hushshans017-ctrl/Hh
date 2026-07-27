# Cross-Platform Shooting Game

A mobile shooting game built with Unity, supporting both Android and iOS platforms.

## Features
- 🎮 Intuitive touch-based controls
- 🎯 Enemy spawning and destruction system
- 💥 Particle effects and animations
- 📱 Responsive UI for mobile devices
- 🎵 Sound effects and background music
- 📊 Score tracking and leaderboard system

## Project Structure

```
Assets/
├── Scripts/
│   ├── Game/
│   │   ├── GameManager.cs
│   │   ├── Enemy.cs
│   │   ├── Player.cs
│   │   └── Weapon.cs
│   ├── UI/
│   │   ├── GameUIManager.cs
│   │   ├── ScoreDisplay.cs
│   │   └── PauseMenuManager.cs
│   └── Managers/
│       ├── InputManager.cs
│       └── AudioManager.cs
├── Prefabs/
│   ├── Enemy.prefab
│   ├── Bullet.prefab
│   └── UI Elements/
├── Scenes/
│   ├── MainMenu.unity
│   ├── GamePlay.unity
│   └── GameOver.unity
├── Audio/
│   ├── Music/
│   └── SFX/
└── Sprites/
    ├── Player/
    ├── Enemies/
    └── UI/
```

## Setup Instructions

1. Install Unity (2021.3 LTS or newer)
2. Clone this repository
3. Open the project in Unity
4. Import the assets from the Assets folder
5. Build for Android/iOS using Build Settings

## Controls

- **Mobile**: Tap to shoot, drag to move player
- **Platform Specific**: Auto-detects input method based on device

## Build Settings

### Android
- Target SDK: Android 12+
- Minimum SDK: Android 8.0
- Orientation: Portrait

### iOS
- Target OS: iOS 14.0+
- Device Family: Universal (iPhone & iPad)
- Orientation: Portrait

## License
MIT License

## Author
hushshans017-ctrl
