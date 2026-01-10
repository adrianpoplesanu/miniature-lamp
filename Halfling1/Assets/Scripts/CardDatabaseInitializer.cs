using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CardDatabaseInitializer : MonoBehaviour
{
    [ContextMenu("Initialize Card Database")]
    public void InitializeCardDatabase()
    {
        CardDatabase database = ScriptableObject.CreateInstance<CardDatabase>();
        
        database.cards = new CardData[]
        {
            new CardData { id = 1, name = "Fireball", cost = 4, attack = 0, health = 0, description = "Deal 6 damage", type = CardType.Spell, effect = SpellEffect.Damage, value = 6 },
            new CardData { id = 2, name = "Lightning Bolt", cost = 1, attack = 0, health = 0, description = "Deal 3 damage", type = CardType.Spell, effect = SpellEffect.Damage, value = 3 },
            new CardData { id = 3, name = "Heal", cost = 2, attack = 0, health = 0, description = "Restore 5 health", type = CardType.Spell, effect = SpellEffect.Heal, value = 5 },
            new CardData { id = 4, name = "Chicken", cost = 1, attack = 1, health = 1, description = "A weak minion", type = CardType.Minion },
            new CardData { id = 5, name = "Orc Warrior", cost = 3, attack = 3, health = 2, description = "A fierce fighter", type = CardType.Minion },
            new CardData { id = 6, name = "Dragon", cost = 8, attack = 8, health = 8, description = "A powerful beast", type = CardType.Minion },
            new CardData { id = 7, name = "Knight", cost = 4, attack = 3, health = 5, description = "A sturdy defender", type = CardType.Minion },
            new CardData { id = 8, name = "Goblin", cost = 2, attack = 2, health = 1, description = "Quick but fragile", type = CardType.Minion },
            new CardData { id = 9, name = "Shield", cost = 1, attack = 0, health = 0, description = "Gain 5 armor", type = CardType.Spell, effect = SpellEffect.Armor, value = 5 },
            new CardData { id = 10, name = "Wolf", cost = 2, attack = 2, health = 2, description = "A loyal companion", type = CardType.Minion }
        };
        
#if UNITY_EDITOR
        string path = "Assets/CardDatabase.asset";
        AssetDatabase.CreateAsset(database, path);
        AssetDatabase.SaveAssets();
        Debug.Log("Card Database created at " + path);
#endif
    }
}

