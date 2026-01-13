using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroPortrait : MonoBehaviour, IPointerClickHandler
{
    public bool isPlayer;
    private Image portraitImage;
    private EventTrigger eventTrigger;
    
    private void Awake()
    {
        portraitImage = GetComponent<Image>();
        // Ensure the Image component exists and can receive raycast events
        if (portraitImage == null)
        {
            portraitImage = gameObject.AddComponent<Image>();
        }
        portraitImage.raycastTarget = true;
        
        // Also add EventTrigger as a fallback
        eventTrigger = GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = gameObject.AddComponent<EventTrigger>();
        }
        
        // Add pointer click event to EventTrigger (only if not already added)
        bool hasClickEntry = false;
        if (eventTrigger.triggers != null)
        {
            foreach (var entry in eventTrigger.triggers)
            {
                if (entry.eventID == EventTriggerType.PointerClick)
                {
                    hasClickEntry = true;
                    break;
                }
            }
        }
        
        if (!hasClickEntry)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
            if (eventTrigger.triggers == null)
            {
                eventTrigger.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();
            }
            eventTrigger.triggers.Add(entry);
        }
    }
    
    private void Start()
    {
        // Double-check raycastTarget is enabled
        if (portraitImage != null)
        {
            portraitImage.raycastTarget = true;
        }
        
        // Verify EventSystem exists
        if (EventSystem.current == null)
        {
            Debug.LogWarning("No EventSystem found in scene! HeroPortrait clicks may not work.");
        }
        
        // Verify Canvas has GraphicRaycaster
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
            if (raycaster == null)
            {
                Debug.LogWarning("Canvas is missing GraphicRaycaster! HeroPortrait clicks may not work.");
            }
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked on {(isPlayer ? "Player" : "Opponent")} portrait");
        
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager.Instance is null!");
            return;
        }
        
        if (!GameManager.Instance.IsPlayerTurn())
        {
            Debug.Log("Not player's turn");
            return;
        }
        
        // If opponent portrait clicked and we have a selected minion
        if (!isPlayer)
        {
            CardData selected = GameManager.Instance.GetSelectedCard();
            if (selected != null && selected.canAttack)
            {
                Debug.Log($"Attacking opponent hero with {selected.name} (Attack: {selected.attack})");
                GameManager.Instance.AttackHero(false);
            }
            else if (selected == null)
            {
                Debug.Log("No minion selected. Select a minion first to attack the opponent hero.");
            }
            else if (!selected.canAttack)
            {
                Debug.Log($"{selected.name} cannot attack (summoning sickness or already attacked).");
            }
        }
    }
    
    // Test method to verify setup
    [ContextMenu("Test Portrait Setup")]
    private void TestPortraitSetup()
    {
        Debug.Log("=== HeroPortrait Setup Test ===");
        Debug.Log($"Image component: {(portraitImage != null ? "Found" : "MISSING")}");
        if (portraitImage != null)
        {
            Debug.Log($"raycastTarget: {portraitImage.raycastTarget}");
        }
        Debug.Log($"EventTrigger component: {(eventTrigger != null ? "Found" : "MISSING")}");
        Debug.Log($"EventSystem: {(EventSystem.current != null ? "Found" : "MISSING")}");
        
        Canvas canvas = GetComponentInParent<Canvas>();
        Debug.Log($"Canvas: {(canvas != null ? "Found" : "MISSING")}");
        if (canvas != null)
        {
            GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
            Debug.Log($"GraphicRaycaster: {(raycaster != null ? "Found" : "MISSING")}");
        }
        
        // Check for blocking parents
        Transform parent = transform.parent;
        int depth = 0;
        while (parent != null && depth < 10)
        {
            Image parentImage = parent.GetComponent<Image>();
            if (parentImage != null && parentImage.raycastTarget)
            {
                Debug.Log($"WARNING: Parent '{parent.name}' has Image with raycastTarget=true (might block clicks)");
            }
            parent = parent.parent;
            depth++;
        }
        Debug.Log("=== End Test ===");
    }
    
    private void Update()
    {
        // Highlight if attackable
        if (portraitImage != null && !isPlayer && GameManager.Instance != null)
        {
            CardData selected = GameManager.Instance.GetSelectedCard();
            if (selected != null && selected.canAttack && GameManager.Instance.IsPlayerTurn())
            {
                portraitImage.color = new Color(1f, 0.5f, 0.5f, 1f); // Slight red tint
            }
            else
            {
                portraitImage.color = Color.white;
            }
        }
    }
}

