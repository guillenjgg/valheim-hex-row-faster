# HexRowFaster

Row faster going forward and backwards!

Tired of rowing your ship at snail speed when the wind dies?  
HexRowFaster boosts the ship's paddle force, making rowing, reversing, and initial movement feel much better.

## Features

- Faster rowing speed
- Faster reverse speed
- Stronger initial ship acceleration
- Faster straight-line paddling
- Configurable force multiplier presets
- Adjust speed on the fly via BepInEx Configuration Manager

## Multiplayer

HexRowFaster is designed so that only the local player controlling the ship has their rowing force modified.

Example:

- If you have the mod installed and control the ship, the mod takes effect.
- If another player without the mod controls the ship, vanilla rowing behavior is used.
- If another player with the mod controls the ship, their config settings are applied while they control the ship.

I have tested this mod primarily in singleplayer. Multiplayer compatibility should be considered experimental.

## Compatibility Disclaimer

Any mod that modifies ship movement, ship physics, rowing force, Rigidbody behavior, water physics, or vanilla ship values may conflict with HexRowFaster.

Likewise, HexRowFaster may affect or alter the behavior of other ship-related mods.

Use at your own risk when combining multiple ship or physics mods.

## What This Mod Actually Affects

HexRowFaster temporarily modifies the ship's internal `m_backwardForce` value while the local player is controlling the ship.

This value contributes to various ship propulsion force calculations including:

- Initial push from a stop
- Forward rowing (`Speed.Slow`)
- Reverse rowing (`Speed.Back`)
- Straight-line paddling acceleration

This mod does **NOT** directly increase:

- Maximum sailing speed
- Wind speed
- Sail speed
- Turning speed

Vanilla steering penalties still apply while rowing.  
Turning hard left or right reduces rowing propulsion force, so ships will still slow down while maneuvering.

## Configuration

Configuration file:

```txt
BepInEx/config/com.hex.rowfaster.cfg
```

Available presets:

```txt
Vanilla = 1x
LittleFaster = 5x
Cruising = 10x
Speeding = 20x
Insane = 40x
```

Example config:

```ini
[General]

## Enables or disables HexRowFaster while controlling a ship.
# Setting type: Boolean
# Default value: true
Enable = true

## How fast do you want to go?
# Setting type: ForceMultiplier
# Default value: Cruising
Force Multiplier = Cruising
```

## Technical Notes

HexRowFaster temporarily modifies the ship's `m_backwardForce` value during `Ship.CustomFixedUpdate()` and restores the original value immediately afterward.

This mod does not directly set ship velocity.  
Instead, it increases force values already used by vanilla ship physics calculations.

## Dependencies

- BepInExPack Valheim
- HarmonyX (included with BepInEx)

## Installation

1. Install BepInEx for Valheim
2. Extract `HexRowFaster.dll` into:

```txt
BepInEx/plugins/HexRowFaster/
```

3. Launch the game

## Source

Developed by Hex Viking.