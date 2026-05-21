# HexRowFaster

Row faster going forward and backwards!

Tired of rowing your ship at snail speed when the wind dies?  
HexRowFaster boosts the ship's paddle force, making rowing, reversing, and initial movement feel much better.

## Compatibility Disclaimer

Any mod that modifies ship movement, ship physics, rowing force, Rigidbody behavior, water physics, or vanilla ship values may conflict with HexRowFaster.

Likewise, HexRowFaster may affect or alter the behavior of other ship-related mods.

Use at your own risk when combining multiple ship or physics mods.

## Features

- Faster rowing speed
- Faster reverse speed
- Stronger initial ship acceleration
- Faster straight-line paddling
- Configurable force multiplier presets

## Multiplayer

Have not tested in multiplayer. Back up your world and use at your own risks.

## What This Mod Actually Affects
HexRowFaster modifies the ship's internal `m_backwardForce` value.

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

## Enables or disables HexRowFaster for newly spawned ships.
# Setting type: Boolean
# Default value: true
Enable = true

## How fast do you want to go?
# Setting type: ForceMultiplier
# Default value: Cruising
Force Multiplier = Cruising
```

## Technical Notes

I modify the `m_backwardForce` value during `Ship.Awake()`.

This mod does not directly set ship velocity.
Instead, it increases force values used in vanilla ship physics calculations.

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