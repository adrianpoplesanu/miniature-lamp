using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MinionUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Elements")]
    public TextMeshProUGUI costText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI nameText;
    public Image borderImage;
    
    private CardData cardData;
    private bool isPlayer;
    private int index;
    private Vector3 originalScale;
    
    private void Start()
    {
        originalScale = transform.localScale;
    }
    
    public void SetupMinion(CardData minion, bool isPlayerMinion, int minionIndex)
    {
        cardData = minion;
        isPlayer = isPlayerMinion;
        index = minionIndex;
        
        UpdateMinionDisplay();
    }
    
    private void UpdateMinionDisplay()
    {
        if (cardData == null) return;
        
        if (costText != null) costText.text = cardData.cost.ToString();
        if (attackText != null) attackText.text = cardData.attack.ToString();
        if (healthText != null) healthText.text = cardData.health.ToString();
        if (nameText != null) nameText.text = cardData.name;
        
        // Update attackable state
        UpdateAttackableState();
    }
    
    private void UpdateAttackableState()
    {
        if (borderImage == null || GameManager.Instance == null) return;
        
        if (isPlayer)
        {
            // Player minion - highlight if can attack
            if (cardData.canAttack && GameManager.Instance.IsPlayerTurn())
            {
                CardData selected = GameManager.Instance.GetSelectedCard();
                if (selected == null || selected.instanceId == cardData.instanceId)
                {
                    borderImage.color = new Color(1f, 0.27f, 0.27f, 1f); // Red for can attack
                }
                else
                {
                    borderImage.color = new Color(0.85f, 0.65f, 0.13f, 1f); // Gold default
                }
            }
            else
            {
                borderImage.color = new Color(0.85f, 0.65f, 0.13f, 1f); // Gold default
            }
        }
        else
        {
            // Opponent minion - highlight if can be attacked
            CardData selected = GameManager.Instance.GetSelectedCard();
            if (selected != null && selected.canAttack && GameManager.Instance.IsPlayerTurn())
            {
                borderImage.color = new Color(1f, 0.27f, 0.27f, 1f); // Red for attackable
            }
            else
            {
                borderImage.color = new Color(0.85f, 0.65f, 0.13f, 1f); // Gold default
            }
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance == null) return;
        
        if (isPlayer && GameManager.Instance.IsPlayerTurn())
        {
            // Player minion clicked
            if (cardData.canAttack)
            {
                GameManager.Instance.SelectMinion(cardData);
            }
        }
        else if (!isPlayer && GameManager.Instance.IsPlayerTurn())
        {
            // Opponent minion clicked - attack it
            CardData selected = GameManager.Instance.GetSelectedCard();
            if (selected != null && selected.canAttack)
            {
                GameManager.Instance.AttackMinion(selected, cardData);
            }
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * 1.1f;
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

