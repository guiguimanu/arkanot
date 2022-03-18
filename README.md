
![arkanot_title](img/arkanot_title.png)

# Overview
A minimalist clone of the beloved game Arkanoid targeted for android. Developed for a coding challenge.

- Like the original the objective of the game is to clear all the bricks
- You control a paddle and you lose a heart when the ball falls to the void
- Some bricks take more than one hit to destroy (based on color)
- Power ups are available
- Level progression is sequential (ie. next level gets unlocked if you beat the current one)

![arkanot_title](img/arkanot_showcase.gif)

# Sections
- [Overview](#overview)
- [Sections](#sections)
- [Technical Overview](#technical-overview)
  - [Game Logic](#game-logic)
  - [Home](#home)
  - [Level Selection](#level-selection)
  - [Data](#data)
  - [Input](#input)
- [Future Improvements / TODO](#future-improvements--todo)
- [Dependencies and Attributions](#dependencies-and-attributions)


# Technical Overview

## Game Logic

TODO: 

## Home

TODO

## Level Selection

TODO

## Data

Due to the scope of the project, a simple **PlayerPrefs** system was implemented. A static class **LevelProgressData** is responsible for handling the data.

## Input

The input in *Arkanot* was designed exclusively for mobile. The Input classes are fairly simple as the game only needs to detect the following inputs:

- UI Tap
  - Handled by Unity's internal *Event System*
- Convert Touch coordinates to world input for paddle movement
- Swipe up gesture for start game

# Future Improvements / TODO

- [ ] Add Swipe Animation Instead Of Text Hint
- [ ] Implement some tests
- [ ] Add a back to selection screen button in-game
- [ ] Add a current power up display
- [ ] A level building feature would be really cool
# Dependencies and Attributions

- Arkanot uses the [Dotween](http://dotween.demigiant.com/) library to handle all in game tweens.
  - [Dotween](http://dotween.demigiant.com/) is a fast, efficient, fully type-safe object-oriented animation engine for Unity.
- A modified spritesheet of [simple icon pastel tone](https://assetstore.unity.com/packages/2d/gui/icons/simple-icon-pastel-tone-107568) is used for the icons in Arkanot
- Using default fonts is boring right? [Shellahera Script Font](https://www.fontspace.com/shellahera-script-font-f23405) is used for all text
  - hindsight 20/20 its rather hard to read!
