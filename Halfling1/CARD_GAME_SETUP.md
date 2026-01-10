# Card Game Unity Implementation

This Unity project contains a complete port of the card game from JavaScript/HTML/CSS to Unity C#.

## Files Created

### Core Scripts
- **CardData.cs** - Data structures for cards (CardData, CardType, SpellEffect, CardDatabase ScriptableObject)
- **GameManager.cs** - Main game controller managing turn flow, game state, card playing, combat, and AI opponent
- **CardUI.cs** - UI controller for cards in hand
- **MinionUI.cs** - UI controller for minions on the board
- **DamagePopup.cs** - Visual effect for showing damage numbers
- **HeroPortrait.cs** - Click handler for hero portraits (for direct hero attacks)
- **CardDatabaseInitializer.cs** - Editor utility to create the card database asset

### Documentation
- **Assets/Scripts/README.md** - Detailed setup instructions

## Game Features Implemented

✅ Turn-based gameplay (Player vs AI)
✅ Mana system (starts at 1, increases each turn, max 10)
✅ Hand management (max 10 cards)
✅ Board management (max 7 minions per side)
✅ Card types: Minions and Spells
✅ Spell effects: Damage, Heal, Armor
✅ Minion combat with attack/health
✅ Summoning sickness (minions can't attack the turn they're played)
✅ AI opponent with card playing and attacking logic
✅ Visual feedback (playable cards, attackable targets)
✅ Damage popups
✅ Win/lose conditions

## Next Steps to Complete Setup

1. **Create Card Database Asset**
   - Add `CardDatabaseInitializer` to a GameObject
   - Right-click component → "Initialize Card Database"
   - This creates `Assets/CardDatabase.asset`

2. **Create UI Prefabs**
   - Create Card Prefab with CardUI component
   - Create Minion Prefab with MinionUI component  
   - Create Damage Popup Prefab with DamagePopup component

3. **Setup Scene**
   - Create Canvas with all UI elements
   - Add GameManager component
   - Wire up all references in inspector
   - Add HeroPortrait components to hero images

4. **Styling**
   - Apply dark fantasy theme matching original design
   - Use gold borders, blue highlights for playable, red for attackable

## Game Mechanics

The game follows the same rules as the original:
- Players start with 30 health
- Each turn, mana increases (capped at 10)
- Cards cost mana to play
- Minions have attack and health
- Spells have instant effects
- Minions can attack other minions or the opponent hero
- First to reduce opponent to 0 health wins

## Controls

- **Click card in hand** → Play it (if affordable)
- **Click your minion** → Select for attack
- **Click opponent minion/hero** → Attack with selected minion
- **Click End Turn button** → End your turn

All scripts are ready to use. You just need to create the UI prefabs and wire up the scene references!

