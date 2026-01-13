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
    //public Image borderImage;
    
    private CardData cardData;
    private bool isPlayer;
    private int index;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    
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
            if (healthText != null)
            {
                healthText.text = cardData.health.ToString();
                healthText.gameObject.SetActive(true);
            }
        }
        else
        {
            if (attackText != null) attackText.gameObject.SetActive(false);
            if (healthText != null) healthText.gameObject.SetActive(false);
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
            transform.localScale = originalScale * 1.1f;
            // Removed SetAsLastSibling() to prevent cards from shuffling in hand
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
    
    public CardData GetCardData()
    {
        return cardData;
    }
}

