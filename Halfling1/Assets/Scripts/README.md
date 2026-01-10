# Card Game Unity Setup Guide

This is a Unity port of the card game originally written in JavaScript/HTML/CSS.

## Setup Instructions

### 1. Create Card Database Asset
1. In Unity, create an empty GameObject in the scene
2. Add the `CardDatabaseInitializer` component to it
3. Right-click the component and select "Initialize Card Database"
4. This will create a `CardDatabase.asset` file in the Assets folder

### 2. Create UI Prefabs

#### Card Prefab
1. Create a UI Image as the card background
2. Add child UI elements:
   - TextMeshPro for cost (top-left)
   - TextMeshPro for attack (top-right, only for minions)
   - TextMeshPro for name (center)
   - TextMeshPro for description (center-bottom)
   - TextMeshPro for health (bottom-right, only for minions)
   - Image for border (to show playable/attackable states)
3. Add the `CardUI` component to the root GameObject
4. Assign all UI references in the inspector
5. Save as a prefab: `CardPrefab`

#### Minion Prefab
1. Similar to Card Prefab but smaller
2. Add the `MinionUI` component instead
3. Save as a prefab: `MinionPrefab`

#### Damage Popup Prefab
1. Create a Canvas (Screen Space - Overlay)
2. Add a TextMeshPro child
3. Add the `DamagePopup` component
4. Save as a prefab: `DamagePopupPrefab`

### 3. Setup Scene

1. Create a Canvas (Screen Space - Overlay)
2. Create the following UI structure:
   - GameManager GameObject (empty, add `GameManager` component)
   - Opponent Area (Panel)
     - Opponent Portrait (Image, add `HeroPortrait` component, set isPlayer = false)
     - Opponent Stats (Panel)
       - Health Text (TextMeshPro)
       - Mana Text (TextMeshPro)
     - Opponent Board (Panel/Grid Layout Group)
   - Player Area (Panel)
     - Player Board (Panel/Grid Layout Group)
     - Player Portrait (Image, add `HeroPortrait` component, set isPlayer = true)
     - Player Stats (Panel)
       - Health Text (TextMeshPro)
       - Mana Text (TextMeshPro)
   - Hand Area (Panel/Horizontal Layout Group, positioned at bottom)
   - Game Info (Panel, center)
     - Turn Indicator (TextMeshPro)
     - Turn Number (TextMeshPro)
   - End Turn Button (Button, bottom-right)

3. Assign all references in the GameManager component:
   - Card Database (the asset created in step 1)
   - All UI Text references
   - Board transforms
   - Hand area transform
   - Prefabs
   - Hero portraits

### 4. Styling
The game uses a dark fantasy theme with:
- Dark blue/purple background
- Gold borders for cards
- Blue highlights for playable cards
- Red highlights for attackable targets
- Brown/gold hero portraits

You can style the UI elements to match the original CSS design.

## Game Mechanics

- **Turn-based**: Player and AI opponent take turns
- **Mana System**: Starts at 1, increases each turn (max 10)
- **Hand Size**: Maximum 10 cards
- **Board Size**: Maximum 7 minions per side
- **Summoning Sickness**: Minions can't attack the turn they're played
- **Card Types**: Minions (creatures) and Spells (instant effects)
- **Spell Effects**: Damage, Heal, Armor
- **Win Condition**: Reduce opponent's health to 0

## Controls

- Click cards in hand to play them (if you have enough mana)
- Click your minions to select them for attack
- Click opponent minions or hero portrait to attack with selected minion
- Click "End Turn" button to end your turn

