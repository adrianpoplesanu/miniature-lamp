using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Elements")]
    public TextMeshProUGUI costText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image cardImage;
    public Outline borderOutline;
    [Header("Background Images (Optional - for hiding on spell cards)")]
    public GameObject attackBackground;
    public GameObject healthBackground;
    //public Image borderImage;
    
    private CardData cardData;
    private bool isPlayer;
    private int index;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Canvas cardCanvas;

    private void Awake() {
        if (attackBackground == null && attackText != null)
        {
            attackBackground = transform.Find("AttackBackground").gameObject;
        }

        if (healthBackground == null && healthText != null)
        {
            healthBackground = transform.Find("HealthBackground").gameObject;
        }
    }
    
    private void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }
    
    public void SetupCard(CardData card, bool isPlayerCard, int cardIndex)
    {
        cardData = card;
        isPlayer = isPlayerCard;
        index = cardIndex;
        
        UpdateCardDisplay();
    }
    
    private void UpdateCardDisplay()
    {
        if (cardData == null) return;
        
        if (costText != null) costText.text = cardData.cost.ToString();
        if (nameText != null) nameText.text = cardData.name;
        if (descriptionText != null) descriptionText.text = cardData.description;
        
        if (cardData.type == CardType.Minion)
        {
            if (attackText != null)
            {
                attackText.text = cardData.attack.ToString();
                attackText.gameObject.SetActive(true);
            }
            if (attackBackground != null)
            {
                attackBackground.gameObject.SetActive(true);
            }
            if (healthText != null)
            {
                healthText.text = cardData.health.ToString();
                healthText.gameObject.SetActive(true);
            }
            if (healthBackground != null)
            {
                healthBackground.gameObject.SetActive(true);
            }
        }
        else
        {
            // Hide attack and health elements for spell cards
            if (attackText != null) attackText.gameObject.SetActive(false);
            if (attackBackground != null) attackBackground.gameObject.SetActive(false);
            if (healthText != null) healthText.gameObject.SetActive(false);
            if (healthBackground != null) healthBackground.gameObject.SetActive(false);
        }
        
        // Update playable state
        UpdatePlayableState();
    }
    
    /*private void UpdatePlayableState()
    {
        if (borderImage != null && isPlayer && GameManager.Instance != null)
        {
            bool canPlay = cardData.cost <= GameManager.Instance.GetPlayerMana();
            bool hasSelection = GameManager.Instance.GetSelectedCard() != null;
            
            if (canPlay && !hasSelection)
            {
                borderImage.color = new Color(0.29f, 0.62f, 1f, 1f); // Blue for playable
            }
            else
            {
                borderImage.color = new Color(0.85f, 0.65f, 0.13f, 1f); // Gold default
            }
        }
    }*/

    private void UpdatePlayableState()
    {
        if (borderOutline != null && isPlayer && GameManager.Instance != null)
        {
            bool canPlay = cardData.cost <= GameManager.Instance.GetPlayerMana();
            bool hasSelection = GameManager.Instance.GetSelectedCard() != null;
            
            if (canPlay && !hasSelection)
            {
                borderOutline.effectColor = new Color(0.29f, 0.62f, 1f, 1f); // Blue for playable
            }
            else
            {
                borderOutline.effectColor = new Color(0.85f, 0.65f, 0.13f, 1f); // Gold default
            }
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isPlayer) return;
        if (GameManager.Instance == null) return;
        if (!GameManager.Instance.IsPlayerTurn()) return;
        
        // Check if we have a selected minion
        CardData selectedCard = GameManager.Instance.GetSelectedCard();
        if (selectedCard != null) return; // Can't play cards when minion is selected
        
        if (cardData.cost <= GameManager.Instance.GetPlayerMana())
        {
            GameManager.Instance.PlayCard(cardData);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isPlayer)
        {
            // Ensure Canvas is set up
            if (cardCanvas == null)
            {
                Canvas parentCanvas = GetComponentInParent<Canvas>();
                if (parentCanvas != null)
                {
                    cardCanvas = gameObject.AddComponent<Canvas>();
                    cardCanvas.overrideSorting = true;
                    cardCanvas.renderMode = parentCanvas.renderMode;
                    cardCanvas.pixelPerfect = parentCanvas.pixelPerfect;
                    if (GetComponent<GraphicRaycaster>() == null)
                    {
                        gameObject.AddComponent<GraphicRaycaster>();
                    }
                }
            }
            
            transform.localScale = originalScale * 1.1f;
            if (cardCanvas != null) cardCanvas.sortingOrder = 100; // Render on top of other cards
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        
        // Remove Canvas component to prevent event blocking
        if (cardCanvas != null)
        {
            GraphicRaycaster raycaster = GetComponent<GraphicRaycaster>();
            if (raycaster != null)
            {
                Destroy(raycaster);
            }
            Destroy(cardCanvas);
            cardCanvas = null;
        }
    }
    
    public CardData GetCardData()
    {
        return cardData;
    }
}

