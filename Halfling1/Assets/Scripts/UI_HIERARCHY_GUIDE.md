# Complete UI Hierarchy Guide for Card Game

This guide shows the complete UI structure for the card game scene, including all components and their setup.

## Complete Hierarchy Structure

```
Canvas (Canvas - Screen Space Overlay)
├── (Canvas component)
├── (Canvas Scaler component)
├── (GraphicRaycaster component)
│
├── GameManager (Empty GameObject)
│   └── (GameManager component) ← Add this script
│
├── OpponentArea (Panel)
│   ├── (Image component - background panel)
│   │
│   ├── OpponentPlayerInfo (Panel)
│   │   ├── (Image component - optional background)
│   │   │
│   │   ├── OpponentPortrait (Image)
│   │   │   ├── (Image component - hero portrait)
│   │   │   └── (HeroPortrait component - isPlayer = false)
│   │   │
│   │   └── OpponentStats (Panel)
│   │       ├── (Image component - optional background)
│   │       │
│   │       ├── OpponentHealthBar (Panel)
│   │       │   ├── (Image component - optional background)
│   │       │   ├── (Horizontal Layout Group component)
│   │       │   │
│   │       │   ├── OpponentHealthLabel (TextMeshProUGUI)
│   │       │   │   └── (TextMeshProUGUI - "Health:")
│   │       │   │
│   │       │   └── OpponentHealthValue (TextMeshProUGUI)
│   │       │       └── (TextMeshProUGUI - "30")
│   │       │
│   │       └── OpponentManaBar (Panel)
│   │           ├── (Image component - optional background)
│   │           ├── (Horizontal Layout Group component)
│   │           │
│   │           ├── OpponentManaLabel (TextMeshProUGUI)
│   │           │   └── (TextMeshProUGUI - "Mana:")
│   │           │
│   │           ├── OpponentManaValue (TextMeshProUGUI)
│   │           │   └── (TextMeshProUGUI - "0")
│   │           │
│   │           ├── OpponentManaSlash (TextMeshProUGUI)
│   │           │   └── (TextMeshProUGUI - "/")
│   │           │
│   │           └── OpponentManaMax (TextMeshProUGUI)
│   │               └── (TextMeshProUGUI - "0")
│   │
│   └── OpponentBoard (Panel or Grid Layout Group)
│       ├── (Image component - board background)
│       ├── (Grid Layout Group component - optional, for auto-layout)
│       └── (Minions will be instantiated here as children)
│
├── GameInfo (Panel)
│   ├── (Image component - optional background)
│   │
│   ├── TurnIndicator (TextMeshProUGUI)
│   │   └── (TextMeshProUGUI - "Your Turn" / "Opponent's Turn")
│   │
│   └── TurnCounter (Panel)
│       ├── (Image component - optional background)
│       ├── (Horizontal Layout Group component)
│       │
│       ├── TurnLabel (TextMeshProUGUI)
│       │   └── (TextMeshProUGUI - "Turn:")
│       │
│       └── TurnNumber (TextMeshProUGUI)
│           └── (TextMeshProUGUI - "1")
│
├── PlayerArea (Panel)
│   ├── (Image component - background panel)
│   │
│   ├── PlayerBoard (Panel or Grid Layout Group)
│   │   ├── (Image component - board background)
│   │   ├── (Grid Layout Group component - optional, for auto-layout)
│   │   └── (Minions will be instantiated here as children)
│   │
│   └── PlayerPlayerInfo (Panel)
│       ├── (Image component - optional background)
│       │
│       ├── PlayerPortrait (Image)
│       │   ├── (Image component - hero portrait)
│       │   └── (HeroPortrait component - isPlayer = true)
│       │
│       └── PlayerStats (Panel)
│           ├── (Image component - optional background)
│           │
│           ├── PlayerHealthBar (Panel)
│           │   ├── (Image component - optional background)
│           │   ├── (Horizontal Layout Group component)
│           │   │
│           │   ├── PlayerHealthLabel (TextMeshProUGUI)
│           │   │   └── (TextMeshProUGUI - "Health:")
│           │   │
│           │   └── PlayerHealthValue (TextMeshProUGUI)
│           │       └── (TextMeshProUGUI - "30")
│           │
│           └── PlayerManaBar (Panel)
│               ├── (Image component - optional background)
│               ├── (Horizontal Layout Group component)
│               │
│               ├── PlayerManaLabel (TextMeshProUGUI)
│               │   └── (TextMeshProUGUI - "Mana:")
│               │
│               ├── PlayerManaValue (TextMeshProUGUI)
│               │   └── (TextMeshProUGUI - "1")
│               │
│               ├── PlayerManaSlash (TextMeshProUGUI)
│               │   └── (TextMeshProUGUI - "/")
│               │
│               └── PlayerManaMax (TextMeshProUGUI)
│                   └── (TextMeshProUGUI - "1")
│
├── HandArea (Panel or Horizontal Layout Group)
│   ├── (Image component - optional background, can be transparent)
│   ├── (Horizontal Layout Group component - for card layout)
│   └── (Cards will be instantiated here as children)
│
└── EndTurnButton (Button)
    ├── (Button component)
    ├── (Image component - button background)
    └── EndTurnButtonText (TextMeshProUGUI)
        └── (TextMeshProUGUI - "End Turn")
```

---

## Step-by-Step Setup Instructions

### 1. Create Canvas

1. Right-click in Hierarchy → **UI → Canvas**
2. Unity automatically creates:
   - Canvas GameObject
   - EventSystem GameObject (for input handling)
3. Canvas Settings:
   - **Render Mode**: Screen Space - Overlay (default)
   - **Pixel Perfect**: Optional (checked for crisp UI)
   - **Sort Order**: 0 (default)
4. Canvas Scaler (automatically added):
   - **UI Scale Mode**: Scale With Screen Size
   - **Reference Resolution**: X: 1920, Y: 1080 (or your target resolution)
   - **Match**: 0.5 (width/height balance)
5. GraphicRaycaster (automatically added):
   - Keep default settings

---

### 2. Create GameManager GameObject

1. Right-click Canvas → **Create Empty** → Rename to `GameManager`
2. Add Component → Search for `GameManager` → Add
3. **Don't assign references yet** - we'll do that after creating all UI elements

---

### 3. Create Opponent Area

#### 3a. OpponentArea Panel
1. Right-click Canvas → **UI → Panel** → Rename to `OpponentArea`
2. RectTransform Settings:
   - **Anchors**: Top-Stretch (hold Alt and click)
   - **Left**: 0
   - **Right**: 0
   - **Top**: 0
   - **Height**: 300 (or adjust based on your design)
3. Image Component (Panel):
   - **Color**: Transparent or dark with alpha (R: 0, G: 0, B: 0, A: 100)
   - **Raycast Target**: Unchecked (optional, for performance)

#### 3b. OpponentPlayerInfo Panel
1. Right-click OpponentArea → **UI → Panel** → Rename to `OpponentPlayerInfo`
2. RectTransform:
   - **Anchors**: Top-Left
   - **Pos X**: 20
   - **Pos Y**: -20
   - **Width**: 400
   - **Height**: 120
3. Image Component: Optional background (can be transparent)

#### 3c. OpponentPortrait Image
1. Right-click OpponentPlayerInfo → **UI → Image** → Rename to `OpponentPortrait`
2. RectTransform:
   - **Anchors**: Left-Center
   - **Pos X**: 0
   - **Pos Y**: 0
   - **Width**: 120
   - **Height**: 120
3. Image Component:
   - **Source Image**: None (or use a sprite/emoji texture)
   - **Color**: Brown/Gold (R: 139, G: 69, B: 19, A: 255) - matches original design
   - **Image Type**: Simple
4. Add Component → Search for `HeroPortrait` → Add
   - **Is Player**: Unchecked (false)

#### 3d. OpponentStats Panel
1. Right-click OpponentPlayerInfo → **UI → Panel** → Rename to `OpponentStats`
2. RectTransform:
   - **Anchors**: Stretch-Stretch
   - **Left**: 140 (to the right of portrait)
   - **Right**: 0
   - **Top**: 0
   - **Bottom**: 0
3. Image Component: Optional background

#### 3e. OpponentHealthBar Panel
1. Right-click OpponentStats → **UI → Panel** → Rename to `OpponentHealthBar`
2. RectTransform:
   - **Anchors**: Top-Stretch
   - **Left**: 0
   - **Right**: 0
   - **Top**: 0
   - **Height**: 40
3. Add Component → **Layout → Horizontal Layout Group**
   - **Child Alignment**: Middle Left
   - **Spacing**: 10
   - **Child Force Expand**: Width: Unchecked, Height: Unchecked

#### 3f. OpponentHealthLabel Text
1. Right-click OpponentHealthBar → **UI → Text - TextMeshPro** → Rename to `OpponentHealthLabel`
2. RectTransform:
   - **Width**: 70
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "Health:"
   - **Font Size**: 18
   - **Color**: White
   - **Alignment**: Left-Center

#### 3g. OpponentHealthValue Text
1. Right-click OpponentHealthBar → **UI → Text - TextMeshPro** → Rename to `OpponentHealthValue`
2. RectTransform:
   - **Width**: 50
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "30"
   - **Font Size**: 24
   - **Color**: Red (R: 255, G: 68, B: 68)
   - **Alignment**: Left-Center
   - **Font Style**: Bold

#### 3h. OpponentManaBar Panel
1. Right-click OpponentStats → **UI → Panel** → Rename to `OpponentManaBar`
2. RectTransform:
   - **Anchors**: Bottom-Stretch
   - **Left**: 0
   - **Right**: 0
   - **Bottom**: 0
   - **Height**: 40
3. Add Component → **Layout → Horizontal Layout Group**
   - **Child Alignment**: Middle Left
   - **Spacing**: 5
   - **Child Force Expand**: Width: Unchecked, Height: Unchecked

#### 3i. OpponentManaLabel Text
1. Right-click OpponentManaBar → **UI → Text - TextMeshPro** → Rename to `OpponentManaLabel`
2. RectTransform:
   - **Width**: 70
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "Mana:"
   - **Font Size**: 18
   - **Color**: White
   - **Alignment**: Left-Center

#### 3j. OpponentManaValue Text
1. Right-click OpponentManaBar → **UI → Text - TextMeshPro** → Rename to `OpponentManaValue`
2. RectTransform:
   - **Width**: 30
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "0"
   - **Font Size**: 24
   - **Color**: Blue (R: 74, G: 158, B: 255)
   - **Alignment**: Center-Center
   - **Font Style**: Bold

#### 3k. OpponentManaSlash Text
1. Right-click OpponentManaBar → **UI → Text - TextMeshPro** → Rename to `OpponentManaSlash`
2. RectTransform:
   - **Width**: 20
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "/"
   - **Font Size**: 18
   - **Color**: Gray (R: 170, G: 170, B: 170)
   - **Alignment**: Center-Center

#### 3l. OpponentManaMax Text
1. Right-click OpponentManaBar → **UI → Text - TextMeshPro** → Rename to `OpponentManaMax`
2. RectTransform:
   - **Width**: 30
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "0"
   - **Font Size**: 18
   - **Color**: Gray (R: 136, G: 136, B: 136)
   - **Alignment**: Center-Center

#### 3m. OpponentBoard Panel
1. Right-click OpponentArea → **UI → Panel** → Rename to `OpponentBoard`
2. RectTransform:
   - **Anchors**: Stretch-Stretch
   - **Left**: 20
   - **Right**: 20
   - **Top**: 130
   - **Bottom**: 20
3. Image Component:
   - **Color**: Dark with alpha (R: 0, G: 0, B: 0, A: 50)
   - **Image Type**: Simple
4. Add Component → **Layout → Grid Layout Group** (Optional, for auto-layout)
   - **Cell Size**: X: 120, Y: 170 (minion size)
   - **Spacing**: X: 10, Y: 10
   - **Start Corner**: Upper Left
   - **Start Axis**: Horizontal
   - **Child Alignment**: Middle Center
   - **Constraint**: Flexible

---

### 4. Create Game Info

#### 4a. GameInfo Panel
1. Right-click Canvas → **UI → Panel** → Rename to `GameInfo`
2. RectTransform:
   - **Anchors**: Middle-Center
   - **Pos X**: 0
   - **Pos Y**: 0
   - **Width**: 300
   - **Height**: 100
3. Image Component:
   - **Color**: Transparent (or semi-transparent dark)
   - **Raycast Target**: Unchecked

#### 4b. TurnIndicator Text
1. Right-click GameInfo → **UI → Text - TextMeshPro** → Rename to `TurnIndicator`
2. RectTransform:
   - **Anchors**: Middle-Center
   - **Pos X**: 0
   - **Pos Y**: 20
   - **Width**: 300
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "Your Turn"
   - **Font Size**: 32
   - **Color**: White
   - **Alignment**: Center-Center
   - **Font Style**: Bold
   - **Text Shadow**: Optional (add Outline or Shadow component for glow effect)

#### 4c. TurnCounter Panel
1. Right-click GameInfo → **UI → Panel** → Rename to `TurnCounter`
2. RectTransform:
   - **Anchors**: Middle-Center
   - **Pos X**: 0
   - **Pos Y**: -30
   - **Width**: 200
   - **Height**: 30
3. Add Component → **Layout → Horizontal Layout Group**
   - **Child Alignment**: Middle Center
   - **Spacing**: 5

#### 4d. TurnLabel Text
1. Right-click TurnCounter → **UI → Text - TextMeshPro** → Rename to `TurnLabel`
2. RectTransform:
   - **Width**: 80
   - **Height**: 30
3. TextMeshProUGUI:
   - **Text**: "Turn:"
   - **Font Size**: 18
   - **Color**: Gray (R: 170, G: 170, B: 170)
   - **Alignment**: Right-Center

#### 4e. TurnNumber Text
1. Right-click TurnCounter → **UI → Text - TextMeshPro** → Rename to `TurnNumber`
2. RectTransform:
   - **Width**: 50
   - **Height**: 30
3. TextMeshProUGUI:
   - **Text**: "1"
   - **Font Size**: 18
   - **Color**: White
   - **Alignment**: Left-Center
   - **Font Style**: Bold

---

### 5. Create Player Area

#### 5a. PlayerArea Panel
1. Right-click Canvas → **UI → Panel** → Rename to `PlayerArea`
2. RectTransform:
   - **Anchors**: Bottom-Stretch (hold Alt and click)
   - **Left**: 0
   - **Right**: 0
   - **Bottom**: 0
   - **Height**: 300
3. Image Component:
   - **Color**: Transparent or dark with alpha

#### 5b. PlayerBoard Panel
1. Right-click PlayerArea → **UI → Panel** → Rename to `PlayerBoard`
2. RectTransform:
   - **Anchors**: Top-Stretch
   - **Left**: 20
   - **Right**: 20
   - **Top**: 0
   - **Height**: 170
3. Image Component:
   - **Color**: Dark with alpha (R: 0, G: 0, B: 0, A: 50)
4. Add Component → **Layout → Grid Layout Group** (Optional)
   - Same settings as OpponentBoard

#### 5c. PlayerPlayerInfo Panel
1. Right-click PlayerArea → **UI → Panel** → Rename to `PlayerPlayerInfo`
2. RectTransform:
   - **Anchors**: Bottom-Stretch
   - **Left**: 20
   - **Right**: 20
   - **Top**: 180
   - **Bottom**: 20
3. Image Component: Optional background

#### 5d. PlayerPortrait Image
1. Right-click PlayerPlayerInfo → **UI → Image** → Rename to `PlayerPortrait`
2. RectTransform:
   - **Anchors**: Right-Center
   - **Pos X**: 0
   - **Pos Y**: 0
   - **Width**: 120
   - **Height**: 120
3. Image Component:
   - **Source Image**: None (or use a sprite)
   - **Color**: Brown/Gold (R: 139, G: 69, B: 19, A: 255)
4. Add Component → Search for `HeroPortrait` → Add
   - **Is Player**: Checked (true)

#### 5e. PlayerStats Panel
1. Right-click PlayerPlayerInfo → **UI → Panel** → Rename to `PlayerStats`
2. RectTransform:
   - **Anchors**: Stretch-Stretch
   - **Left**: 0
   - **Right**: 140
   - **Top**: 0
   - **Bottom**: 0
3. Image Component: Optional background

#### 5f. PlayerHealthBar Panel
1. Right-click PlayerStats → **UI → Panel** → Rename to `PlayerHealthBar`
2. RectTransform:
   - **Anchors**: Top-Stretch
   - **Left**: 0
   - **Right**: 0
   - **Top**: 0
   - **Height**: 40
3. Add Component → **Layout → Horizontal Layout Group**
   - **Child Alignment**: Middle Left
   - **Spacing**: 10
   - **Child Force Expand**: Width: Unchecked, Height: Unchecked

#### 5g. PlayerHealthLabel Text
1. Right-click PlayerHealthBar → **UI → Text - TextMeshPro** → Rename to `PlayerHealthLabel`
2. RectTransform:
   - **Width**: 70
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "Health:"
   - **Font Size**: 18
   - **Color**: White
   - **Alignment**: Left-Center

#### 5h. PlayerHealthValue Text
1. Right-click PlayerHealthBar → **UI → Text - TextMeshPro** → Rename to `PlayerHealthValue`
2. RectTransform:
   - **Width**: 50
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "30"
   - **Font Size**: 24
   - **Color**: Red (R: 255, G: 68, B: 68)
   - **Alignment**: Left-Center
   - **Font Style**: Bold

#### 5i. PlayerManaBar Panel
1. Right-click PlayerStats → **UI → Panel** → Rename to `PlayerManaBar`
2. RectTransform:
   - **Anchors**: Bottom-Stretch
   - **Left**: 0
   - **Right**: 0
   - **Bottom**: 0
   - **Height**: 40
3. Add Component → **Layout → Horizontal Layout Group**
   - **Child Alignment**: Middle Left
   - **Spacing**: 5
   - **Child Force Expand**: Width: Unchecked, Height: Unchecked

#### 5j. PlayerManaLabel Text
1. Right-click PlayerManaBar → **UI → Text - TextMeshPro** → Rename to `PlayerManaLabel`
2. RectTransform:
   - **Width**: 70
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "Mana:"
   - **Font Size**: 18
   - **Color**: White
   - **Alignment**: Left-Center

#### 5k. PlayerManaValue Text
1. Right-click PlayerManaBar → **UI → Text - TextMeshPro** → Rename to `PlayerManaValue`
2. RectTransform:
   - **Width**: 30
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "1"
   - **Font Size**: 24
   - **Color**: Blue (R: 74, G: 158, B: 255)
   - **Alignment**: Center-Center
   - **Font Style**: Bold

#### 5l. PlayerManaSlash Text
1. Right-click PlayerManaBar → **UI → Text - TextMeshPro** → Rename to `PlayerManaSlash`
2. RectTransform:
   - **Width**: 20
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "/"
   - **Font Size**: 18
   - **Color**: Gray (R: 170, G: 170, B: 170)
   - **Alignment**: Center-Center

#### 5m. PlayerManaMax Text
1. Right-click PlayerManaBar → **UI → Text - TextMeshPro** → Rename to `PlayerManaMax`
2. RectTransform:
   - **Width**: 30
   - **Height**: 40
3. TextMeshProUGUI:
   - **Text**: "1"
   - **Font Size**: 18
   - **Color**: Gray (R: 136, G: 136, B: 136)
   - **Alignment**: Center-Center

---

### 6. Create Hand Area

#### 6a. HandArea Panel
1. Right-click Canvas → **UI → Panel** → Rename to `HandArea`
2. RectTransform:
   - **Anchors**: Bottom-Stretch
   - **Left**: 0
   - **Right**: 0
   - **Bottom**: 0
   - **Height**: 250 (or adjust based on card height)
3. Image Component:
   - **Color**: Transparent (A: 0) - no background needed
   - **Raycast Target**: Unchecked
4. Add Component → **Layout → Horizontal Layout Group**
   - **Child Alignment**: Middle Center
   - **Spacing**: 5
   - **Child Force Expand**: Width: Unchecked, Height: Unchecked
   - **Child Control Size**: Width: Unchecked, Height: Unchecked

**Note**: Cards will be instantiated here as children by the GameManager script.

---

### 7. Create End Turn Button

#### 7a. EndTurnButton Button
1. Right-click Canvas → **UI → Button** → Rename to `EndTurnButton`
2. RectTransform:
   - **Anchors**: Bottom-Right
   - **Pos X**: -20
   - **Pos Y**: 20
   - **Width**: 150
   - **Height**: 50
3. Image Component (Button background):
   - **Source Image**: None (or use a sprite)
   - **Color**: Brown/Gold gradient (R: 139, G: 69, B: 19, A: 255)
   - **Image Type**: Simple
4. Button Component:
   - **Interactable**: Checked
   - **Transition**: Color Tint (default)
   - **Normal Color**: Brown (R: 139, G: 69, B: 19, A: 255)
   - **Highlighted Color**: Lighter brown
   - **Pressed Color**: Darker brown
   - **Selected Color**: Same as normal
   - **Disabled Color**: Gray (R: 128, G: 128, B: 128, A: 128)

#### 7b. EndTurnButtonText Text
1. Select EndTurnButton (it already has a Text child)
2. Rename the Text child to `EndTurnButtonText`
3. TextMeshProUGUI (or Text component):
   - **Text**: "End Turn"
   - **Font Size**: 18
   - **Color**: White
   - **Alignment**: Center-Center
   - **Font Style**: Bold

**Note**: If Unity created a Text component instead of TextMeshProUGUI, you can:
- Delete the Text component
- Add TextMeshProUGUI component instead
- Or keep Text component (it will work, but TextMeshPro is preferred)

---

## 8. Wire Up GameManager References

Now that all UI elements are created, assign them to the GameManager component:

1. Select the **GameManager** GameObject
2. In the GameManager component Inspector, assign all references:

### Card Database:
- **Card Database**: Drag your CardDatabase asset from Project window

### UI References:
- **Player Health Text**: Drag `PlayerHealthValue` GameObject
- **Player Mana Text**: Drag `PlayerManaValue` GameObject
- **Player Mana Max Text**: Drag `PlayerManaMax` GameObject
- **Opponent Health Text**: Drag `OpponentHealthValue` GameObject
- **Opponent Mana Text**: Drag `OpponentManaValue` GameObject
- **Opponent Mana Max Text**: Drag `OpponentManaMax` GameObject
- **Turn Indicator Text**: Drag `TurnIndicator` GameObject
- **Turn Number Text**: Drag `TurnNumber` GameObject
- **End Turn Button**: Drag `EndTurnButton` GameObject

### Board References:
- **Player Board**: Drag `PlayerBoard` GameObject (the Transform)
- **Opponent Board**: Drag `OpponentBoard` GameObject (the Transform)
- **Player Hand Area**: Drag `HandArea` GameObject (the Transform)

### Prefabs:
- **Card Prefab**: Drag your CardPrefab from Project window
- **Minion Prefab**: Drag your MinionPrefab from Project window
- **Damage Popup Prefab**: Drag your DamagePopupPrefab from Project window

### Hero Portraits:
- **Player Portrait**: Drag `PlayerPortrait` GameObject (the Image component)
- **Opponent Portrait**: Drag `OpponentPortrait` GameObject (the Image component)

---

## Layout Tips

### Positioning:
- Use **Anchors** to make UI elements responsive to different screen sizes
- **Stretch-Stretch** anchors make elements fill their parent
- **Top-Left**, **Bottom-Right**, etc. for fixed positions

### Spacing:
- Use **Layout Groups** (Horizontal, Vertical, Grid) for automatic spacing
- Or manually position elements using RectTransform

### Styling:
- Match the original CSS design:
  - Dark blue/purple gradient background
  - Gold borders and accents
  - Brown/gold hero portraits
  - Blue for mana, red for health

### Testing:
- Test at different screen resolutions
- Adjust Canvas Scaler settings if needed
- Use Safe Area if targeting mobile devices

---

## Final Checklist

- [ ] Canvas created with proper settings
- [ ] GameManager GameObject with GameManager component
- [ ] OpponentArea with portrait, stats, and board
- [ ] PlayerArea with board, portrait, and stats
- [ ] GameInfo with turn indicator and counter
- [ ] HandArea for player cards
- [ ] EndTurnButton
- [ ] All GameManager references assigned
- [ ] HeroPortrait components added to portraits
- [ ] All text elements use TextMeshProUGUI
- [ ] Layout Groups added where needed
- [ ] Colors match the original design

Your UI structure is now complete and ready to use!

