# Card Prefab Creation Guide

## Step-by-Step Instructions

### 1. Create the Root GameObject
- Right-click in Hierarchy → **UI → Image** 
  - Unity automatically creates a GameObject named "Image" under the Canvas
  - This GameObject already has an **Image** component attached
  - **Rename this "Image" GameObject to `CardPrefab`** (click on it in Hierarchy and press F2, or right-click → Rename)
- Set the RectTransform:
  - **Width**: 140
  - **Height**: 200
  - **Anchors**: Middle-Center
  - **Position**: (0, 0, 0)
  
**Note**: This root GameObject (the one originally named "Image") will become your card background AND the root of your prefab. It already has an Image component, which is perfect - we'll use that as the `cardImage` reference later.

### 2. Add the CardUI Component
- Select the `CardPrefab` GameObject
- Click **Add Component** → Search for `CardUI` → Add it
- The CardUI script needs references that we'll assign after creating child elements

### 3. Setup the Background Image
- The root Image IS the `cardImage` reference
- In the Image component:
  - **Source Image**: None (or use a sprite if you have one)
  - **Color**: Dark blue/gray (e.g., R: 44, G: 62, B: 80, A: 255) - matches the original CSS gradient
  - **Image Type**: Simple
  - **Preserve Aspect**: Unchecked

### 4. Create the Border Image
**IMPORTANT**: The BorderImage will be used to show the gold border outline. We'll position it in the hierarchy so it renders behind all text elements but on top of the card background.

- Right-click `CardPrefab` → **UI → Image** → Rename to `BorderImage`
- Set RectTransform:
  - **Anchors**: Stretch-Stretch (hold Alt and click stretch)
  - **Left**: -3 (makes it 3px larger on left)
  - **Right**: -3 (makes it 3px larger on right)
  - **Top**: -3 (makes it 3px larger on top)
  - **Bottom**: -3 (makes it 3px larger on bottom)
  - **This creates a border outline effect** (the image extends 3px beyond the card edges)
- In the Image component:
  - **Source Image**: None (we'll use color only)
  - **Color**: Gold (R: 218, G: 165, B: 32, A: 255) with Alpha around 200-255 for visibility
  - **Image Type**: Simple
  - **Preserve Aspect**: Unchecked
  - **Raycast Target**: Unchecked (so clicks pass through to the card)
  
**Alternative Approach - Use an Outline Effect:**
Instead of a full rectangle, you can create a border effect using:
1. Create a border sprite (hollow rectangle/frame) in an image editor
2. OR use Unity's Outline component on the card (Add Component → UI → Outline)
3. OR make BorderImage semi-transparent and position it correctly

**CRITICAL - BorderImage Position in Hierarchy:**
The BorderImage must be positioned **AFTER the root Image** but **BEFORE all text elements** so:
1. Root Image (card background) renders first (behind everything) ✅
2. BorderImage renders second (on top of background, but behind text) ✅
3. All text elements render last (on top of everything, including border) ✅

**Correct Hierarchy Order:**
```
CardPrefab (root with Image component - card background)
├── BorderImage ← MUST be here (right after root, before ALL text)
├── CostBackground
├── CostText
├── AttackBackground
├── AttackText
├── HealthBackground
├── HealthText
├── NameText
└── DescriptionText
```

**If BorderImage is covering text:**
1. **Drag BorderImage UP** in the hierarchy so it's positioned right after the root CardPrefab
2. Make sure ALL text elements (NameText, DescriptionText, CostText, etc.) are listed BELOW BorderImage
3. In Unity Hierarchy: items listed higher = render behind, items listed lower = render on top
4. BorderImage should be the FIRST child of CardPrefab (right after the root Image component)

### 5. Create Cost Text (Top-Left)
**IMPORTANT**: Both CostBackground and CostText should be **siblings** (both direct children of CardPrefab), NOT parent-child. In Unity, siblings render in hierarchy order: items listed **higher** render **behind**, items listed **lower** render **on top**.

**Step 5a - Create Background FIRST (so it renders behind):**
- Right-click `CardPrefab` → **UI → Image** → Rename to `CostBackground`
- Set RectTransform:
  - **Width**: 30
  - **Height**: 30
  - **Anchors**: Top-Left
  - **Pos X**: 15
  - **Pos Y**: -15
- In Image component:
  - **Source Image**: None (we'll use a circle sprite or color)
  - **Color**: Blue (R: 74, G: 158, B: 255) - mana blue
  - **Image Type**: Simple
  - **Preserve Aspect**: Unchecked
  - **Note**: To make it circular, you can create a white circle sprite in an image editor, or use Unity's built-in circle sprite (see tip below)

**Step 5b - Create Text SECOND (so it renders on top):**
- Right-click `CardPrefab` → **UI → Text - TextMeshPro** → Rename to `CostText`
- Set RectTransform:
  - **Width**: 30
  - **Height**: 30
  - **Anchors**: Top-Left
  - **Pos X**: 15 (same as background - must overlap exactly)
  - **Pos Y**: -15 (same as background)
- In TextMeshProUGUI component:
  - **Text**: "0" (placeholder)
  - **Font Size**: 18
  - **Alignment**: Center (both horizontal and vertical)
  - **Color**: White
  - **Font Style**: Bold

**HOW TO ENSURE CORRECT RENDERING ORDER:**
1. After creating both, check the Hierarchy:
   ```
   CardPrefab
   ├── CostBackground ← Should be ABOVE CostText
   └── CostText       ← Should be BELOW CostBackground
   ```

2. **If CostText is above CostBackground** (wrong order):
   - **Drag CostText down** in the hierarchy so it appears **below** CostBackground
   - OR drag CostBackground up so it appears **above** CostText
   
3. **Visual check**: In the Hierarchy panel:
   - Items listed **higher** = render **behind** (background)
   - Items listed **lower** = render **on top** (text)
   
4. **In Scene view**: The background should render behind, text should appear on top

**Unity Hierarchy Rule**: For siblings under the same parent, Unity renders them from top to bottom. So the first child renders first (behind), and later children render on top of earlier ones.

**Tip for circular background:**
If you want a perfect circle, you can:
1. Create a white circle sprite (128x128) in an image editor, save as PNG
2. Import to Unity: Drag PNG into Assets folder
3. Select the sprite → Inspector → Texture Type: "Sprite (2D and UI)"
4. Apply, then assign it to CostBackground's Image → Source Image field
5. Alternatively, use Unity's built-in circle: In the Image component, click the circle next to "Source Image" → Select "UI Circle" or create a circle sprite using Unity's Sprite Editor

### 6. Create Attack Text (Top-Right, for Minions)
**Same principle as Cost**: Create AttackBackground FIRST, then AttackText SECOND (both as siblings of CardPrefab).

**Step 6a - Create Background FIRST:**
- Right-click `CardPrefab` → **UI → Image** → Rename to `AttackBackground`
- Set RectTransform:
  - **Width**: 30
  - **Height**: 30
  - **Anchors**: Top-Right
  - **Pos X**: -15
  - **Pos Y**: -15
- In Image component:
  - **Source Image**: None (or circle sprite)
  - **Color**: Red (R: 255, G: 68, B: 68)
  - **Image Type**: Simple
  - **Preserve Aspect**: Unchecked

**Step 6b - Create Text SECOND:**
- Right-click `CardPrefab` → **UI → Text - TextMeshPro** → Rename to `AttackText`
- Set RectTransform:
  - **Width**: 30
  - **Height**: 30
  - **Anchors**: Top-Right
  - **Pos X**: -15 (same as background)
  - **Pos Y**: -15 (same as background)
- In TextMeshProUGUI:
  - **Text**: "0"
  - **Font Size**: 18
  - **Alignment**: Center
  - **Color**: White
  - **Font Style**: Bold

**Ensure AttackBackground is ABOVE AttackText in hierarchy** (see Step 5 for details)

### 7. Create Name Text (Center-Top)
- Right-click `CardPrefab` → **UI → Text - TextMeshPro** → Rename to `NameText`
- Set RectTransform:
  - **Width**: 120
  - **Height**: 20
  - **Anchors**: Middle-Top
  - **Pos Y**: -40
- In TextMeshProUGUI:
  - **Text**: "Card Name"
  - **Font Size**: 14
  - **Alignment**: Center-Top
  - **Color**: White
  - **Font Style**: Bold
  - **Auto Size**: Checked (optional)

### 8. Create Description Text (Center)
- Right-click `CardPrefab` → **UI → Text - TextMeshPro** → Rename to `DescriptionText`
- Set RectTransform:
  - **Width**: 120
  - **Height**: 60
  - **Anchors**: Middle-Center
  - **Pos Y**: 0
- In TextMeshProUGUI:
  - **Text**: "Card description text"
  - **Font Size**: 11
  - **Alignment**: Center-Center
  - **Color**: Light Gray (R: 204, G: 204, B: 204)
  - **Wrapping**: Enabled
  - **Auto Size**: Checked (optional)

### 9. Create Health Text (Bottom-Right, for Minions)
**Same principle as Cost and Attack**: Create HealthBackground FIRST, then HealthText SECOND (both as siblings of CardPrefab).

**Step 9a - Create Background FIRST:**
- Right-click `CardPrefab` → **UI → Image** → Rename to `HealthBackground`
- Set RectTransform:
  - **Width**: 30
  - **Height**: 30
  - **Anchors**: Bottom-Right
  - **Pos X**: -15
  - **Pos Y**: 15
- In Image component:
  - **Source Image**: None (or circle sprite)
  - **Color**: Green (R: 68, G: 255, B: 68)
  - **Image Type**: Simple
  - **Preserve Aspect**: Unchecked

**Step 9b - Create Text SECOND:**
- Right-click `CardPrefab` → **UI → Text - TextMeshPro** → Rename to `HealthText`
- Set RectTransform:
  - **Width**: 30
  - **Height**: 30
  - **Anchors**: Bottom-Right
  - **Pos X**: -15 (same as background)
  - **Pos Y**: 15 (same as background)
- In TextMeshProUGUI:
  - **Text**: "0"
  - **Font Size**: 18
  - **Alignment**: Center
  - **Color**: White
  - **Font Style**: Bold

**Ensure HealthBackground is ABOVE HealthText in hierarchy** (see Step 5 for details)

### 10. Wire Up References in CardUI Component
- Select the root `CardPrefab` GameObject
- In the CardUI component inspector, assign all references:
  - **Cost Text**: Drag `CostText` GameObject
  - **Attack Text**: Drag `AttackText` GameObject
  - **Health Text**: Drag `HealthText` GameObject
  - **Name Text**: Drag `NameText` GameObject
  - **Description Text**: Drag `DescriptionText` GameObject
  - **Card Image**: Drag the root Image component (the CardPrefab itself)
  - **Border Image**: Drag `BorderImage` GameObject

### 11. Make it Clickable
- Ensure the root GameObject has:
  - **Image component** (already has this)
  - **CardUI component** (already added)
  - The parent Canvas must have a **GraphicRaycaster** component (Unity adds this automatically)
  - The parent Canvas must have an **EventSystem** in the scene (Unity adds this automatically when creating first UI element)

### 12. Final Hierarchy Should Look Like:
**CRITICAL**: The hierarchy order determines rendering order. Items listed HIGHER render BEHIND, items listed LOWER render ON TOP.

```
Canvas
└── CardPrefab (GameObject - rename from "Image")
    ├── (Image component) ← This is the card background (cardImage reference) - RENDERS FIRST (behind everything)
    ├── (CardUI component) ← Add this component
    │
    ├── BorderImage (GameObject with Image) ← RENDERS SECOND (on top of background, behind text)
    │
    ├── CostBackground (GameObject with Image) ← RENDERS THIRD
    ├── CostText (GameObject with TextMeshProUGUI) ← RENDERS FOURTH (on top of CostBackground)
    │
    ├── AttackBackground (GameObject with Image) ← RENDERS FIFTH
    ├── AttackText (GameObject with TextMeshProUGUI) ← RENDERS SIXTH (on top of AttackBackground)
    │
    ├── HealthBackground (GameObject with Image) ← RENDERS SEVENTH
    ├── HealthText (GameObject with TextMeshProUGUI) ← RENDERS EIGHTH (on top of HealthBackground)
    │
    ├── NameText (GameObject with TextMeshProUGUI) ← RENDERS NINTH (on top of border)
    └── DescriptionText (GameObject with TextMeshProUGUI) ← RENDERS TENTH (on top of border)
```

**Proper Hierarchy Order (from top to bottom):**
1. **CardPrefab root** (with Image component) - card background
2. **BorderImage** - gold border (on top of background, but behind text)
3. **All text backgrounds and texts** - Cost, Attack, Health pairs
4. **NameText and DescriptionText** - on top of everything

**Hierarchy Order Rules:**
- Items listed **HIGHER** in hierarchy = render **BEHIND** (backgrounds, borders)
- Items listed **LOWER** in hierarchy = render **ON TOP** (text)
- For each pair (Cost, Attack, Health): Background must be ABOVE its text
- **BorderImage must be AFTER root Image but BEFORE all text elements** (so text shows on top)

**If BorderImage is covering text**: Move BorderImage UP in the hierarchy (drag it higher in the list) so it's positioned right after the root Image component, but before all text elements.

**Visual Layout (as seen in the scene/game):**
```
┌─────────────────────────────────────┐
│  CardPrefab (140x200)               │
│  ┌───────────────────────────────┐  │
│  │ [BorderImage - Gold border]   │  │
│  │                               │  │
│  │ [CostBg]     [AttackBg]       │  │ ← Top row
│  │   0            0              │  │
│  │                               │  │
│  │        Card Name              │  │ ← Center-top
│  │                               │  │
│  │    Card description           │  │ ← Center
│  │    text goes here             │  │
│  │                               │  │
│  │            [HealthBg]         │  │ ← Bottom-right
│  │              0                │  │
│  └───────────────────────────────┘  │
└─────────────────────────────────────┘
```

### 13. Save as Prefab
- Drag `CardPrefab` from Hierarchy to the `Assets` folder (or create a `Prefabs` folder first)
- Delete the instance from the scene (we'll instantiate it via code)
- Rename if needed to just `CardPrefab` in the Project window

### 14. Optional: Add Visual Polish
- Add a shadow or outline to text elements for better readability
- Consider adding a subtle gradient to the card background
- Add rounded corners by using a rounded rectangle sprite (if you have one)
- Adjust colors to match the original CSS design:
  - Card background: Dark gradient (similar to `#2c3e50` to `#34495e`)
  - Border: Gold `#daa520`
  - Cost background: Blue `#4a9eff`
  - Attack background: Red `#ff4444`
  - Health background: Green `#44ff44`

## Notes
- The BorderImage will change color to show playable (blue) or attackable (red) states
- Attack and Health texts are automatically hidden for spell cards
- All text elements should have TextMeshPro (not the old Text component)
- Make sure the Canvas Render Mode is "Screen Space - Overlay" for proper UI rendering

