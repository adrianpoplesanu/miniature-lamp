using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.IO;

public class CardGameUIGenerator : EditorWindow
{
    [MenuItem("Card Game/Generate UI Hierarchy")]
    public static void GenerateUI()
    {
        // Check if Canvas already exists
        Canvas existingCanvas = FindObjectOfType<Canvas>();
        if (existingCanvas != null)
        {
            if (!EditorUtility.DisplayDialog("Canvas Exists", 
                "A Canvas already exists in the scene. Do you want to delete it and create a new one?", 
                "Yes", "No"))
            {
                return;
            }
            DestroyImmediate(existingCanvas.gameObject);
        }

        // Create Canvas
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = true;
        
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;
        
        canvasObj.AddComponent<GraphicRaycaster>();

        // Create GameManager
        GameObject gameManager = new GameObject("GameManager");
        gameManager.transform.SetParent(canvasObj.transform);
        gameManager.AddComponent<GameManager>();

        // Create Opponent Area
        GameObject opponentArea = CreatePanel("OpponentArea", canvasObj.transform);
        SetRectTransform(opponentArea, new Vector2(0, 1), new Vector2(1, 1), new Vector4(0, 0, 0, -300));
        SetImageColor(opponentArea, new Color(0, 0, 0, 0.4f));

        // Opponent Player Info
        GameObject opponentPlayerInfo = CreatePanel("OpponentPlayerInfo", opponentArea.transform);
        SetRectTransform(opponentPlayerInfo, new Vector2(0, 1), new Vector2(0, 1), new Vector4(20, -20, -380, -100));

        // Opponent Portrait
        GameObject opponentPortrait = CreateImage("OpponentPortrait", opponentPlayerInfo.transform);
        SetRectTransform(opponentPortrait, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(0, 0, 120, 120));
        SetImageColor(opponentPortrait, new Color(0.545f, 0.271f, 0.075f, 1f)); // Brown/Gold
        HeroPortrait opponentHeroPortrait = opponentPortrait.AddComponent<HeroPortrait>();
        opponentHeroPortrait.isPlayer = false;

        // Opponent Stats
        GameObject opponentStats = CreatePanel("OpponentStats", opponentPlayerInfo.transform);
        SetRectTransform(opponentStats, new Vector2(0, 0), new Vector2(1, 1), new Vector4(140, 0, 0, 0));

        // Opponent Health Bar
        GameObject opponentHealthBar = CreatePanel("OpponentHealthBar", opponentStats.transform);
        SetRectTransform(opponentHealthBar, new Vector2(0, 1), new Vector2(1, 1), new Vector4(0, 0, 0, -40));
        AddHorizontalLayoutGroup(opponentHealthBar, 10);

        GameObject opponentHealthLabel = CreateText("OpponentHealthLabel", opponentHealthBar.transform, "Health:", 18, Color.white);
        SetRectTransform(opponentHealthLabel, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(0, 0, 70, 40));
        SetTextAlignment(opponentHealthLabel, TextAlignmentOptions.Left, TextAlignmentOptions.Midline);

        GameObject opponentHealthValue = CreateText("OpponentHealthValue", opponentHealthBar.transform, "30", 24, new Color(1f, 0.27f, 0.27f));
        SetRectTransform(opponentHealthValue, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(80, 0, 50, 40));
        SetTextAlignment(opponentHealthValue, TextAlignmentOptions.Left, TextAlignmentOptions.Midline);
        SetTextStyle(opponentHealthValue, FontStyles.Bold);

        // Opponent Mana Bar
        GameObject opponentManaBar = CreatePanel("OpponentManaBar", opponentStats.transform);
        SetRectTransform(opponentManaBar, new Vector2(0, 0), new Vector2(1, 1), new Vector4(0, 40, 0, 0));
        AddHorizontalLayoutGroup(opponentManaBar, 5);

        GameObject opponentManaLabel = CreateText("OpponentManaLabel", opponentManaBar.transform, "Mana:", 18, Color.white);
        SetRectTransform(opponentManaLabel, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(0, 0, 70, 40));
        SetTextAlignment(opponentManaLabel, TextAlignmentOptions.Left, TextAlignmentOptions.Midline);

        GameObject opponentManaValue = CreateText("OpponentManaValue", opponentManaBar.transform, "0", 24, new Color(0.29f, 0.62f, 1f));
        SetRectTransform(opponentManaValue, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(75, 0, 30, 40));
        SetTextAlignment(opponentManaValue, TextAlignmentOptions.Center, TextAlignmentOptions.Midline);
        SetTextStyle(opponentManaValue, FontStyles.Bold);

        GameObject opponentManaSlash = CreateText("OpponentManaSlash", opponentManaBar.transform, "/", 18, new Color(0.67f, 0.67f, 0.67f));
        SetRectTransform(opponentManaSlash, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(110, 0, 20, 40));
        SetTextAlignment(opponentManaSlash, TextAlignmentOptions.Center, TextAlignmentOptions.Midline);

        GameObject opponentManaMax = CreateText("OpponentManaMax", opponentManaBar.transform, "0", 18, new Color(0.53f, 0.53f, 0.53f));
        SetRectTransform(opponentManaMax, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(135, 0, 30, 40));
        SetTextAlignment(opponentManaMax, TextAlignmentOptions.Center, TextAlignmentOptions.Midline);

        // Opponent Board
        GameObject opponentBoard = CreatePanel("OpponentBoard", opponentArea.transform);
        SetRectTransform(opponentBoard, new Vector2(0, 0), new Vector2(1, 1), new Vector4(20, 20, -20, -130));
        SetImageColor(opponentBoard, new Color(0, 0, 0, 0.2f));
        AddGridLayoutGroup(opponentBoard, new Vector2(120, 170), new Vector2(10, 10));

        // Game Info
        GameObject gameInfo = CreatePanel("GameInfo", canvasObj.transform);
        SetRectTransform(gameInfo, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector4(-150, -50, 150, 50));
        SetImageColor(gameInfo, new Color(0, 0, 0, 0));

        GameObject turnIndicator = CreateText("TurnIndicator", gameInfo.transform, "Your Turn", 32, Color.white);
        SetRectTransform(turnIndicator, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector4(-150, 10, 150, 50));
        SetTextAlignment(turnIndicator, TextAlignmentOptions.Center, TextAlignmentOptions.Midline);
        SetTextStyle(turnIndicator, FontStyles.Bold);

        GameObject turnCounter = CreatePanel("TurnCounter", gameInfo.transform);
        SetRectTransform(turnCounter, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector4(-100, -40, 100, -10));
        AddHorizontalLayoutGroup(turnCounter, 5);

        GameObject turnLabel = CreateText("TurnLabel", turnCounter.transform, "Turn:", 18, new Color(0.67f, 0.67f, 0.67f));
        SetRectTransform(turnLabel, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(0, 0, 80, 30));
        SetTextAlignment(turnLabel, TextAlignmentOptions.Right, TextAlignmentOptions.Midline);

        GameObject turnNumber = CreateText("TurnNumber", turnCounter.transform, "1", 18, Color.white);
        SetRectTransform(turnNumber, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(85, 0, 50, 30));
        SetTextAlignment(turnNumber, TextAlignmentOptions.Left, TextAlignmentOptions.Midline);
        SetTextStyle(turnNumber, FontStyles.Bold);

        // Player Area
        GameObject playerArea = CreatePanel("PlayerArea", canvasObj.transform);
        SetRectTransform(playerArea, new Vector2(0, 0), new Vector2(1, 0), new Vector4(0, 300, 0, 0));
        SetImageColor(playerArea, new Color(0, 0, 0, 0.4f));

        // Player Board
        GameObject playerBoard = CreatePanel("PlayerBoard", playerArea.transform);
        SetRectTransform(playerBoard, new Vector2(0, 1), new Vector2(1, 1), new Vector4(20, -170, -20, 0));
        SetImageColor(playerBoard, new Color(0, 0, 0, 0.2f));
        AddGridLayoutGroup(playerBoard, new Vector2(120, 170), new Vector2(10, 10));

        // Player Player Info
        GameObject playerPlayerInfo = CreatePanel("PlayerPlayerInfo", playerArea.transform);
        SetRectTransform(playerPlayerInfo, new Vector2(0, 0), new Vector2(1, 0), new Vector4(20, 0, -20, -300));

        // Player Portrait
        GameObject playerPortrait = CreateImage("PlayerPortrait", playerPlayerInfo.transform);
        SetRectTransform(playerPortrait, new Vector2(1, 0.5f), new Vector2(1, 0.5f), new Vector4(-120, 0, 0, 120));
        SetImageColor(playerPortrait, new Color(0.545f, 0.271f, 0.075f, 1f)); // Brown/Gold
        HeroPortrait playerHeroPortrait = playerPortrait.AddComponent<HeroPortrait>();
        playerHeroPortrait.isPlayer = true;

        // Player Stats
        GameObject playerStats = CreatePanel("PlayerStats", playerPlayerInfo.transform);
        SetRectTransform(playerStats, new Vector2(0, 0), new Vector2(1, 1), new Vector4(0, 0, -140, 0));

        // Player Health Bar
        GameObject playerHealthBar = CreatePanel("PlayerHealthBar", playerStats.transform);
        SetRectTransform(playerHealthBar, new Vector2(0, 1), new Vector2(1, 1), new Vector4(0, 0, 0, -40));
        AddHorizontalLayoutGroup(playerHealthBar, 10);

        GameObject playerHealthLabel = CreateText("PlayerHealthLabel", playerHealthBar.transform, "Health:", 18, Color.white);
        SetRectTransform(playerHealthLabel, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(0, 0, 70, 40));
        SetTextAlignment(playerHealthLabel, TextAlignmentOptions.Left, TextAlignmentOptions.Midline);

        GameObject playerHealthValue = CreateText("PlayerHealthValue", playerHealthBar.transform, "30", 24, new Color(1f, 0.27f, 0.27f));
        SetRectTransform(playerHealthValue, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(80, 0, 50, 40));
        SetTextAlignment(playerHealthValue, TextAlignmentOptions.Left, TextAlignmentOptions.Midline);
        SetTextStyle(playerHealthValue, FontStyles.Bold);

        // Player Mana Bar
        GameObject playerManaBar = CreatePanel("PlayerManaBar", playerStats.transform);
        SetRectTransform(playerManaBar, new Vector2(0, 0), new Vector2(1, 1), new Vector4(0, 40, 0, 0));
        AddHorizontalLayoutGroup(playerManaBar, 5);

        GameObject playerManaLabel = CreateText("PlayerManaLabel", playerManaBar.transform, "Mana:", 18, Color.white);
        SetRectTransform(playerManaLabel, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(0, 0, 70, 40));
        SetTextAlignment(playerManaLabel, TextAlignmentOptions.Left, TextAlignmentOptions.Midline);

        GameObject playerManaValue = CreateText("PlayerManaValue", playerManaBar.transform, "1", 24, new Color(0.29f, 0.62f, 1f));
        SetRectTransform(playerManaValue, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(75, 0, 30, 40));
        SetTextAlignment(playerManaValue, TextAlignmentOptions.Center, TextAlignmentOptions.Midline);
        SetTextStyle(playerManaValue, FontStyles.Bold);

        GameObject playerManaSlash = CreateText("PlayerManaSlash", playerManaBar.transform, "/", 18, new Color(0.67f, 0.67f, 0.67f));
        SetRectTransform(playerManaSlash, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(110, 0, 20, 40));
        SetTextAlignment(playerManaSlash, TextAlignmentOptions.Center, TextAlignmentOptions.Midline);

        GameObject playerManaMax = CreateText("PlayerManaMax", playerManaBar.transform, "1", 18, new Color(0.53f, 0.53f, 0.53f));
        SetRectTransform(playerManaMax, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector4(135, 0, 30, 40));
        SetTextAlignment(playerManaMax, TextAlignmentOptions.Center, TextAlignmentOptions.Midline);

        // Hand Area
        GameObject handArea = CreatePanel("HandArea", canvasObj.transform);
        SetRectTransform(handArea, new Vector2(0, 0), new Vector2(1, 0), new Vector4(0, 250, 0, 0));
        SetImageColor(handArea, new Color(0, 0, 0, 0));
        Image handAreaImage = handArea.GetComponent<Image>();
        if (handAreaImage != null) handAreaImage.raycastTarget = false;
        AddHorizontalLayoutGroup(handArea, 5);

        // End Turn Button
        GameObject endTurnButton = new GameObject("EndTurnButton");
        endTurnButton.transform.SetParent(canvasObj.transform);
        SetRectTransform(endTurnButton, new Vector2(1, 0), new Vector2(1, 0), new Vector4(-170, 70, -20, 20));
        
        Image buttonImage = endTurnButton.AddComponent<Image>();
        buttonImage.color = new Color(0.545f, 0.271f, 0.075f, 1f);
        
        Button button = endTurnButton.AddComponent<Button>();
        ColorBlock colors = button.colors;
        colors.normalColor = new Color(0.545f, 0.271f, 0.075f, 1f);
        colors.highlightedColor = new Color(0.627f, 0.322f, 0.176f, 1f);
        colors.pressedColor = new Color(0.392f, 0.196f, 0.118f, 1f);
        colors.selectedColor = new Color(0.545f, 0.271f, 0.075f, 1f);
        colors.disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        button.colors = colors;

        GameObject buttonText = CreateText("EndTurnButtonText", endTurnButton.transform, "End Turn", 18, Color.white);
        SetRectTransform(buttonText, new Vector2(0, 0), new Vector2(1, 1), new Vector4(0, 0, 0, 0));
        SetTextAlignment(buttonText, TextAlignmentOptions.Center, TextAlignmentOptions.Midline);
        SetTextStyle(buttonText, FontStyles.Bold);

        // Auto-assign GameManager references
        GameManager gm = gameManager.GetComponent<GameManager>();
        if (gm != null)
        {
            var playerHealthText = typeof(GameManager).GetField("playerHealthText", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var playerManaText = typeof(GameManager).GetField("playerManaText", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var playerManaMaxText = typeof(GameManager).GetField("playerManaMaxText", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var opponentHealthText = typeof(GameManager).GetField("opponentHealthText", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var opponentManaText = typeof(GameManager).GetField("opponentManaText", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var opponentManaMaxText = typeof(GameManager).GetField("opponentManaMaxText", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var turnIndicatorText = typeof(GameManager).GetField("turnIndicatorText", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var turnNumberText = typeof(GameManager).GetField("turnNumberText", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var endTurnButtonField = typeof(GameManager).GetField("endTurnButton", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var playerBoardField = typeof(GameManager).GetField("playerBoard", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var opponentBoardField = typeof(GameManager).GetField("opponentBoard", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var playerHandAreaField = typeof(GameManager).GetField("playerHandArea", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var playerPortraitField = typeof(GameManager).GetField("playerPortrait", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var opponentPortraitField = typeof(GameManager).GetField("opponentPortrait", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            if (playerHealthText != null) playerHealthText.SetValue(gm, playerHealthValue.GetComponent<TextMeshProUGUI>());
            if (playerManaText != null) playerManaText.SetValue(gm, playerManaValue.GetComponent<TextMeshProUGUI>());
            if (playerManaMaxText != null) playerManaMaxText.SetValue(gm, playerManaMax.GetComponent<TextMeshProUGUI>());
            if (opponentHealthText != null) opponentHealthText.SetValue(gm, opponentHealthValue.GetComponent<TextMeshProUGUI>());
            if (opponentManaText != null) opponentManaText.SetValue(gm, opponentManaValue.GetComponent<TextMeshProUGUI>());
            if (opponentManaMaxText != null) opponentManaMaxText.SetValue(gm, opponentManaMax.GetComponent<TextMeshProUGUI>());
            if (turnIndicatorText != null) turnIndicatorText.SetValue(gm, turnIndicator.GetComponent<TextMeshProUGUI>());
            if (turnNumberText != null) turnNumberText.SetValue(gm, turnNumber.GetComponent<TextMeshProUGUI>());
            if (endTurnButtonField != null) endTurnButtonField.SetValue(gm, button);
            if (playerBoardField != null) playerBoardField.SetValue(gm, playerBoard.transform);
            if (opponentBoardField != null) opponentBoardField.SetValue(gm, opponentBoard.transform);
            if (playerHandAreaField != null) playerHandAreaField.SetValue(gm, handArea.transform);
            if (playerPortraitField != null) playerPortraitField.SetValue(gm, playerPortrait.GetComponent<Image>());
            if (opponentPortraitField != null) opponentPortraitField.SetValue(gm, opponentPortrait.GetComponent<Image>());
        }

        // Save as prefab
        string prefabPath = "Assets/Prefabs/CardGameUI.prefab";
        string prefabDirectory = Path.GetDirectoryName(prefabPath);
        if (!Directory.Exists(prefabDirectory))
        {
            Directory.CreateDirectory(prefabDirectory);
        }

        PrefabUtility.SaveAsPrefabAsset(canvasObj, prefabPath);
        Debug.Log($"UI Hierarchy created and saved as prefab at: {prefabPath}");
        
        Selection.activeGameObject = canvasObj;
        EditorUtility.FocusProjectWindow();
    }

    private static GameObject CreatePanel(string name, Transform parent)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent);
        panel.AddComponent<Image>();
        return panel;
    }

    private static GameObject CreateImage(string name, Transform parent)
    {
        GameObject image = new GameObject(name);
        image.transform.SetParent(parent);
        image.AddComponent<Image>();
        return image;
    }

    private static GameObject CreateText(string name, Transform parent, string text, int fontSize, Color color)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent);
        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.color = color;
        return textObj;
    }

    private static void SetRectTransform(GameObject obj, Vector2 anchorMin, Vector2 anchorMax, Vector4 offset)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();
        if (rt == null) rt = obj.AddComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = new Vector2(offset.x, offset.y);
        rt.offsetMax = new Vector2(offset.z, offset.w);
    }

    private static void SetImageColor(GameObject obj, Color color)
    {
        Image img = obj.GetComponent<Image>();
        if (img != null) img.color = color;
    }

    private static void SetTextAlignment(GameObject obj, TextAlignmentOptions horizontal, TextAlignmentOptions vertical)
    {
        TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();
        if (tmp != null) tmp.alignment = horizontal | vertical;
    }

    private static void SetTextStyle(GameObject obj, FontStyles style)
    {
        TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();
        if (tmp != null) tmp.fontStyle = style;
    }

    private static void AddHorizontalLayoutGroup(GameObject obj, float spacing)
    {
        HorizontalLayoutGroup hlg = obj.AddComponent<HorizontalLayoutGroup>();
        hlg.childAlignment = TextAnchor.MiddleLeft;
        hlg.spacing = spacing;
        hlg.childForceExpandWidth = false;
        hlg.childForceExpandHeight = false;
    }

    private static void AddGridLayoutGroup(GameObject obj, Vector2 cellSize, Vector2 spacing)
    {
        GridLayoutGroup glg = obj.AddComponent<GridLayoutGroup>();
        glg.cellSize = cellSize;
        glg.spacing = spacing;
        glg.startCorner = GridLayoutGroup.Corner.UpperLeft;
        glg.startAxis = GridLayoutGroup.Axis.Horizontal;
        glg.childAlignment = TextAnchor.MiddleCenter;
        glg.constraint = GridLayoutGroup.Constraint.Flexible;
    }
}

