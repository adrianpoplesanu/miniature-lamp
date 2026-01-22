using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float lifetime = 1f;
    public float floatSpeed = 50f;
    
    private RectTransform rectTransform;
    private Vector2 startPosition;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            startPosition = rectTransform.anchoredPosition;
        }
        
        // Ensure it's visible on top
        transform.SetAsLastSibling();
    }
    
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    
    private void Update()
    {
        if (rectTransform != null)
        {
            // Move up in UI space (anchoredPosition uses local canvas coordinates)
            rectTransform.anchoredPosition += Vector2.up * floatSpeed * Time.deltaTime;
        }
        else
        {
            // Fallback for non-UI objects
            transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        }
    }
    
    public void SetDamage(int damage)
    {
        if (damageText != null)
        {
            damageText.text = "-" + damage.ToString();
            damageText.color = Color.red;
        }
    }
}

