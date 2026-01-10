using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroPortrait : MonoBehaviour, IPointerClickHandler
{
    public bool isPlayer;
    private Image portraitImage;
    
    private void Start()
    {
        portraitImage = GetComponent<Image>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance == null) return;
        if (!GameManager.Instance.IsPlayerTurn()) return;
        
        // If opponent portrait clicked and we have a selected minion
        if (!isPlayer)
        {
            CardData selected = GameManager.Instance.GetSelectedCard();
            if (selected != null && selected.canAttack)
            {
                GameManager.Instance.AttackHero(false);
            }
        }
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

