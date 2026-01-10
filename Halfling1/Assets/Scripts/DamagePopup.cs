using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float lifetime = 1f;
    public float floatSpeed = 50f;
    
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    
    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }
    
    public void SetDamage(int damage)
    {
        if (damageText != null)
        {
            damageText.text = "-" + damage.ToString();
        }
    }
}

