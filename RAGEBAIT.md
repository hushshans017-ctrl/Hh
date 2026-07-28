# Ragebait Popup System

## Feature Overview

The Ragebait Popup System displays hilarious, provocative messages when players die, creating that entertaining "rage bait" moment that encourages players to retry.

## Death Messages

When you die, a random message appears on screen with these options:

### 💥 Combat Messages
- **GIT GUD!**
- **SKILL ISSUE!**
- **NOOB!**
- **GET REKT!**
- **OUTPLAYED!**
- **WASHED UP!**

### 😱 Destruction Messages
- **DESTROYED!**
- **DEMOLISHED!**
- **ANNIHILATED!**
- **OBLITERATED!**
- **SLAUGHTERED!**
- **BUTCHERED!**

### ☠️ Game Over Messages
- **YOU DIED!**
- **GAME OVER**
- **RIP**
- **DEFEATED!**
- **ELIMINATED!**
- **VANQUISHED!**

### 🎮 Meme Messages
- **L + RATIO**
- **TOO EASY!**
- **OWNED!**
- **WHAT A PLAY!**
- **DELETED!**
- **BLOWN AWAY!**

And more! 30+ different ragebait messages to keep players entertained.

## Visual Features

### Dynamic Styling
- ✨ **Random Colors** - Red, Yellow, Orange, Magenta, Cyan, White
- 📈 **Scale Animation** - Text grows as it appears
- ⬆️ **Rising Effect** - Text floats upward
- 👻 **Fade Out** - Smooth transparency fade
- 🖤 **Black Outline** - Makes text visible on any background

### Duration
- Messages appear for 3 seconds
- Auto-dismisses to show Game Over screen
- Multiple messages can stack for brutal deaths

## How It Works

1. Enemy reaches bottom of screen
2. Player dies
3. Ragebait message pops up
4. Game shows Game Over screen
5. Player can retry immediately

## Customization

You can add your own ragebait messages:

```csharp
RagebaitPopupSystem.Instance.AddCustomRagebaitWord("YOUR MESSAGE HERE");
```

## Psychology

This system leverages the "rage bait" gaming trend where funny, taunting messages encourage players to:
- ✅ Immediately retry
- ✅ Improve their skills
- ✅ Share clips with friends
- ✅ Stay engaged longer
- ✅ Build community around fails

## Future Enhancements

- 🎬 Screen shake effect
- 🔊 Audio cue for message
- 🎨 GIF/animation support
- ⭐ Rarity-based messages (legendary deaths get special messages)
- 🏆 Achievement triggers ("Die 100 times")
- 📊 Statistics tracking

