# Bloodlust

<p align="center">
  <strong>A 2D action platformer prototype built with Unity and C#</strong>
</p>

<p align="center">
  A portfolio project focused on gameplay programming, combat systems, enemy management, animation, audio, and game state systems.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Unity-6000.4.8f1-black?logo=unity" alt="Unity Version">
  <img src="https://img.shields.io/badge/Language-C%23-blue?logo=csharp" alt="C#">
  <img src="https://img.shields.io/badge/Status-Playable-brightgreen" alt="Project Status">
</p>

---

## About

**Bloodlust** is a 2D action platformer prototype developed in Unity as a game development portfolio project.

The project was created to put Unity and C# concepts into practice by building interconnected gameplay systems rather than relying entirely on pre-built mechanics.

The main focus was on developing and integrating systems such as player movement, combat, health management, enemy behavior, spawning, animation, audio, score, and game states.

The project also served as practical experience in debugging gameplay issues and organizing functionality into separate scripts and reusable Unity components.

---

## Gameplay

The player controls a vampire character through combat encounters against different types of enemies.

The core gameplay loop is built around movement, jumping, combat, and surviving enemy encounters while managing the player's health.

The project currently includes a playable and exported build.

---

## Features

* 2D player movement and jumping
* Ground detection
* Basic melee combat
* Special attack
* Player health and damage system
* Enemy health and damage system
* Enemy behavior
* Enemy prefab spawning
* Enemy respawn system
* Special enemy encounters
* Player and enemy animations
* Sound effects and background audio
* Score and reward system
* Main menu
* Game over state

---

## Technical Implementation

### Player Controller

The player controller manages the character's core movement and interaction with the game environment.

Implemented functionality includes:

* Horizontal movement
* Jumping
* Ground detection
* Player facing direction
* Rigidbody2D-based movement
* Collision handling
* Animator integration
* Sound effects

The movement system uses Unity's 2D physics and collision system to determine interactions with the environment.

### Combat System

Combat functionality is handled separately from the player's movement logic through a dedicated combat script.

The system handles:

* Basic attacks
* Special attacks
* Attack animations
* Damage interaction with enemies

Separating combat from movement keeps the responsibilities of the player systems more clearly defined.

### Health & Damage System

Both the player and enemies use dedicated health and damage logic.

The enemy system manages:

* Maximum health
* Current health
* Damage reception
* Death state
* Death animation
* Object removal after death

The player has a separate health system responsible for receiving damage and managing the player's health state.

### Enemy System

Enemies are implemented as reusable Unity prefabs.

The enemy-related systems cover:

* Enemy behavior
* Health management
* Damage handling
* Death states
* Animation states
* Enemy spawning
* Enemy respawning
* Special enemy encounters

The use of prefabs allows enemy instances to be created and configured without duplicating the underlying game object structure.

### Enemy Respawn & Special Enemy

The enemy respawn system controls the creation of new enemy instances during gameplay.

A dedicated system also manages special enemy encounters, including the conditions for activating and maintaining a special enemy instance.

This creates a more dynamic combat flow and allows different enemy types to be introduced during gameplay.

### Animation System

Unity's Animator system is used to connect gameplay states with character and enemy animations.

The project includes animations for actions such as:

* Idle
* Movement
* Jumping
* Attacking
* Special attacks
* Death

Gameplay scripts communicate with the Animator to change animation states based on player and enemy behavior.

### Audio System

Audio is integrated into gameplay through sound effects and background audio.

A dedicated `SoundManager` is used to centralize audio-related functionality while gameplay scripts can trigger sounds associated with specific actions and events.

### Score System

The score system provides a **reward mechanism** for defeating enemies.

Each enemy can have a configurable score value. When an enemy is defeated, the score system receives the corresponding reward and updates the player's score.

This creates a direct connection between combat performance and gameplay progression.

The score is managed through a dedicated `ScoreCounter`, keeping reward handling separate from the individual enemy's core health and behavior logic.

### Game States

The project includes dedicated game flow elements such as:

* Main menu
* Gameplay state
* Game over state
* Score display

A `GameManager` is used to coordinate game-level functionality and transitions between different states.

---

## Controls

| Input      | Action         |
| ---------- | -------------- |
| Arrow Keys | Move           |
| Space      | Jump           |
| Ctrl       | Attack         |
| Shift      | Special Attack |

---

## Project Structure

The project is organized into separate areas based on gameplay responsibility.

```text
Assets/
├── Animation/
├── Audio/
├── Scenes/
├── Scripts/
│   ├── Enemies/
│   ├── Player/
│   └── GameManager/
├── Sprites/
└── ...
```

### Main Scripts

```text
Scripts/
├── Player/
│   ├── PlayerController.cs
│   ├── PlayerCombat.cs
│   ├── PlayerHealth.cs
│   └── GroundCheck.cs
│
├── Enemies/
│   ├── Enemy.cs
│   ├── EnemyController.cs
│   ├── EnemyRespawn.cs
│   └── EnemySpecialAttackSc.cs
│
└── GameManager/
    ├── GameManager.cs
    ├── ScoreCounter.cs
    └── SoundManager.cs
```

This structure separates player, enemy, and game-management responsibilities while keeping the project accessible for further development.

---

## Screenshots

Screenshots from the playable build will be presented here.

<!-- Screenshots to be added -->

---

## Technologies

* **Unity 6000.4.8f1**
* **C#**
* **Unity 2D Physics**
* **Unity Animator**
* **Unity Prefabs**
* **Unity Audio System**
* **Git**
* **GitHub**

---

## Assets & Credits

Bloodlust uses a combination of free third-party assets and AI-generated visual assets.

Third-party assets are used according to their respective licenses and attribution requirements.

AI-generated assets were used during the visual development and prototyping process.

---

## Development Highlights

Through the development of Bloodlust, I gained practical experience in:

* Developing gameplay systems with C#
* Working with Unity components
* Managing Rigidbody2D physics
* Implementing collision detection
* Creating reusable prefabs
* Connecting gameplay logic to Animator states
* Building health and damage systems
* Implementing enemy spawning and respawning
* Creating reward and score systems
* Managing audio through scripts
* Debugging gameplay behavior
* Structuring a Unity project for continued development

---

## Future Improvements

Planned or potential improvements include:

* Additional levels
* More enemy types
* Expanded combat mechanics
* Additional player abilities
* More advanced enemy AI
* Additional animations
* Expanded progression systems
* Gameplay balancing
* Additional visual and audio polish

---

## Project Status

**Playable Prototype**

Bloodlust is currently a playable and exported Unity prototype.

The project is being maintained as part of my game development portfolio and may continue to evolve as I develop new Unity and C# skills.

---

## Author

**Jovaine Cari**

Unity Developer / Game Developer

[GitHub](https://github.com/JovaineCari)
