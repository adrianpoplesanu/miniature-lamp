using System;
using UnityEngine;

[Serializable]
public enum CardType
{
    Minion,
    Spell
}

[Serializable]
public enum SpellEffect
{
    Damage,
    Heal,
    Armor
}

[Serializable]
public class CardData
{
    public int id;
    public string name;
    public int cost;
    public int attack;
    public int health;
    public string description;
    public CardType type;
    public SpellEffect effect;
    public int value; // For spell effects
    
    public float instanceId; // Unique ID for this card instance
    
    public bool canAttack; // For minions
    
    public CardData Clone()
    {
        return new CardData
        {
            id = this.id,
            name = this.name,
            cost = this.cost,
            attack = this.attack,
            health = this.health,
            description = this.description,
            type = this.type,
            effect = this.effect,
            value = this.value,
            instanceId = UnityEngine.Random.Range(0f, 1000000f) + Time.time,
            canAttack = this.canAttack
        };
    }
}

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Card Game/Card Database")]
public class CardDatabase : ScriptableObject
{
    public CardData[] cards;
    
    public CardData GetRandomCard()
    {
        if (cards == null || cards.Length == 0) return null;
        return cards[UnityEngine.Random.Range(0, cards.Length)];
    }
}

