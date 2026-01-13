using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Card Database")]
    public CardDatabase cardDatabase;
    
    [Header("UI References")]
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerManaText;
    public TextMeshProUGUI playerManaMaxText;
    public TextMeshProUGUI opponentHealthText;
    public TextMeshProUGUI opponentManaText;
    public TextMeshProUGUI opponentManaMaxText;
    public TextMeshProUGUI turnIndicatorText;
    public TextMeshProUGUI turnNumberText;
    public Button endTurnButton;
    
    [Header("Board References")]
    public Transform playerBoard;
    public Transform opponentBoard;
    public Transform playerHandArea;
    
    [Header("Prefabs")]
    public GameObject cardPrefab;
    public GameObject minionPrefab;
    public GameObject damagePopupPrefab;
    
    [Header("Hero Portraits")]
    public Image playerPortrait;
    public Image opponentPortrait;
    
    // Game State
    private int turn = 1;
    private bool playerTurn = true;
    private int playerHealth = 30;
    private int playerMana = 1;
    private int playerManaMax = 1;
    private int opponentHealth = 30;
    private int opponentMana = 0;
    private int opponentManaMax = 0;
    
    private List<CardData> playerHand = new List<CardData>();
    private List<CardData> opponentHand = new List<CardData>();
    private List<CardData> playerBoardMinions = new List<CardData>();
    private List<CardData> opponentBoardMinions = new List<CardData>();
    
    private CardData selectedCard = null;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        InitializeGame();
    }
    
    private void InitializeGame()
    {
        // Draw initial hands
        for (int i = 0; i < 3; i++)
        {
            DrawCard(true);
            DrawCard(false);
        }
        
        UpdateUI();
        SetupEventListeners();
    }
    
    private void SetupEventListeners()
    {
        if (endTurnButton != null)
        {
            endTurnButton.onClick.AddListener(EndTurn);
        }
    }
    
    public void DrawCard(bool isPlayer)
    {
        List<CardData> hand = isPlayer ? playerHand : opponentHand;
        if (hand.Count >= 10) return; // Max hand size
        
        CardData randomCard = cardDatabase.GetRandomCard();
        if (randomCard != null)
        {
            CardData newCard = randomCard.Clone();
            hand.Add(newCard);
            
            if (isPlayer)
            {
                RenderHand();
            }
        }
    }
    
    public void RenderHand()
    {
        // Clear existing cards
        foreach (Transform child in playerHandArea)
        {
            Destroy(child.gameObject);
        }
        
        // Create card UI elements
        for (int i = 0; i < playerHand.Count; i++)
        {
            CardData card = playerHand[i];
            GameObject cardObj = Instantiate(cardPrefab, playerHandArea);
            CardUI cardUI = cardObj.GetComponent<CardUI>();
            if (cardUI != null)
            {
                cardUI.SetupCard(card, true, i);
            }
        }
    }
    
    public void RenderBoard()
    {
        // Clear existing minions
        foreach (Transform child in this.playerBoard)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in this.opponentBoard)
        {
            Destroy(child.gameObject);
        }
        
        // Create player minions
        for (int i = 0; i < playerBoardMinions.Count; i++)
        {
            CardData minion = playerBoardMinions[i];
            GameObject minionObj = Instantiate(minionPrefab, this.playerBoard);
            MinionUI minionUI = minionObj.GetComponent<MinionUI>();
            if (minionUI != null)
            {
                minionUI.SetupMinion(minion, true, i);
            }
        }
        
        // Create opponent minions
        for (int i = 0; i < opponentBoardMinions.Count; i++)
        {
            CardData minion = opponentBoardMinions[i];
            GameObject minionObj = Instantiate(minionPrefab, this.opponentBoard);
            MinionUI minionUI = minionObj.GetComponent<MinionUI>();
            if (minionUI != null)
            {
                minionUI.SetupMinion(minion, false, i);
            }
        }
    }
    
    public void PlayCard(CardData card)
    {
        if (card.cost > playerMana) return;
        if (!playerTurn) return;
        
        if (card.type == CardType.Spell)
        {
            PlaySpell(card);
        }
        else if (card.type == CardType.Minion)
        {
            if (playerBoardMinions.Count < 7)
            {
                PlayMinion(card);
            }
        }
        
        selectedCard = null;
        UpdateUI();
    }
    
    private void PlayMinion(CardData card)
    {
        CardData minion = card.Clone();
        minion.canAttack = false; // Summoning sickness
        
        // Log card play
        Debug.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Player played card: {card.name} (Minion)");
        
        playerBoardMinions.Add(minion);
        playerMana -= card.cost;
        playerHand.Remove(card);
        
        selectedCard = null;
        UpdateUI();
    }
    
    private void PlaySpell(CardData card)
    {
        // Log card play
        Debug.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Player played card: {card.name} (Spell - {card.effect})");
        
        if (card.effect == SpellEffect.Damage)
        {
            if (opponentBoardMinions.Count > 0)
            {
                CardData target = opponentBoardMinions[UnityEngine.Random.Range(0, opponentBoardMinions.Count)];
                DealDamage(target, card.value);
            }
            else
            {
                DealDamageToHero(false, card.value);
            }
        }
        else if (card.effect == SpellEffect.Heal)
        {
            playerHealth = Mathf.Min(30, playerHealth + card.value);
        }
        else if (card.effect == SpellEffect.Armor)
        {
            playerHealth = Mathf.Min(30, playerHealth + card.value);
        }
        
        playerMana -= card.cost;
        playerHand.Remove(card);
        UpdateUI();
    }
    
    public void SelectMinion(CardData minion)
    {
        if (!playerTurn) return;
        if (!minion.canAttack) return;
        
        if (selectedCard != null && selectedCard.instanceId == minion.instanceId)
        {
            selectedCard = null;
        }
        else
        {
            selectedCard = minion;
        }
        
        HighlightTargets();
        UpdateUI();
    }
    
    public void AttackMinion(CardData attacker, CardData defender)
    {
        DealDamage(defender, attacker.attack);
        DealDamage(attacker, defender.attack);
        
        attacker.canAttack = false;
        selectedCard = null;
        
        // Remove dead minions
        playerBoardMinions.RemoveAll(m => m.health <= 0);
        opponentBoardMinions.RemoveAll(m => m.health <= 0);
        
        UpdateUI();
    }
    
    public void AttackHero(bool isPlayer)
    {
        if (selectedCard == null || !selectedCard.canAttack) return;
        
        if (isPlayer)
        {
            DealDamageToHero(true, selectedCard.attack);
        }
        else
        {
            DealDamageToHero(false, selectedCard.attack);
        }
        
        selectedCard.canAttack = false;
        selectedCard = null;
        UpdateUI();
    }
    
    private void DealDamage(CardData target, int damage)
    {
        target.health -= damage;
        
        // Show damage popup
        ShowDamagePopup(target, damage);
        
        if (target.health <= 0)
        {
            StartCoroutine(RemoveDeadMinion(target));
        }
    }
    
    private void DealDamageToHero(bool isPlayer, int damage)
    {
        if (isPlayer)
        {
            playerHealth = Mathf.Max(0, playerHealth - damage);
        }
        else
        {
            opponentHealth = Mathf.Max(0, opponentHealth - damage);
        }
        
        Image heroPortrait = isPlayer ? playerPortrait : opponentPortrait;
        ShowDamagePopupOnTransform(heroPortrait.transform, damage);
        
        CheckGameOver();
    }
    
    private void ShowDamagePopup(CardData target, int damage)
    {
        // Find the minion object
        MinionUI[] minions = FindObjectsOfType<MinionUI>();
        foreach (MinionUI minion in minions)
        {
            if (minion.GetCardData() != null && minion.GetCardData().instanceId == target.instanceId)
            {
                ShowDamagePopupOnTransform(minion.transform, damage);
                return;
            }
        }
    }
    
    private void ShowDamagePopupOnTransform(Transform target, int damage)
    {
        if (damagePopupPrefab != null)
        {
            GameObject popup = Instantiate(damagePopupPrefab, target.position, Quaternion.identity);
            DamagePopup damagePopup = popup.GetComponent<DamagePopup>();
            if (damagePopup != null)
            {
                damagePopup.SetDamage(damage);
            }
        }
    }
    
    private IEnumerator RemoveDeadMinion(CardData minion)
    {
        yield return new WaitForSeconds(0.1f);
        
        playerBoardMinions.RemoveAll(m => m.instanceId == minion.instanceId);
        opponentBoardMinions.RemoveAll(m => m.instanceId == minion.instanceId);
        
        UpdateUI();
    }
    
    private void HighlightTargets()
    {
        UpdateUI();
    }
    
    public void EndTurn()
    {
        if (!playerTurn) return;
        
        playerTurn = false;
        
        // Reset minion attack status
        foreach (CardData minion in playerBoardMinions)
        {
            minion.canAttack = true;
        }
        
        StartCoroutine(OpponentTurn());
    }
    
    private IEnumerator OpponentTurn()
    {
        yield return new WaitForSeconds(1f);
        
        turn++;
        opponentManaMax = Mathf.Min(10, Mathf.FloorToInt(turn / 2f));
        opponentMana = opponentManaMax;
        
        DrawCard(false);
        
        List<float> minionsAtStartOfTurn = new List<float>();
        foreach (CardData minion in opponentBoardMinions)
        {
            minionsAtStartOfTurn.Add(minion.instanceId);
        }
        
        UpdateUI();
        
        yield return new WaitForSeconds(0.5f);
        
        OpponentPlayCards(minionsAtStartOfTurn);
        
        yield return new WaitForSeconds(1.5f);
        
        OpponentAttack(minionsAtStartOfTurn);
        
        yield return new WaitForSeconds(1f);
        
        EndOpponentTurn();
    }
    
    private void OpponentPlayCards(List<float> minionsAtStartOfTurn)
    {
        List<CardData> playableCards = new List<CardData>();
        foreach (CardData card in opponentHand)
        {
            if (card.cost <= opponentMana)
            {
                playableCards.Add(card);
            }
        }
        
        playableCards.Sort((a, b) => b.cost.CompareTo(a.cost));
        
        foreach (CardData card in playableCards)
        {
            if (opponentMana >= card.cost)
            {
                if (card.type == CardType.Minion && opponentBoardMinions.Count < 7)
                {
                    // Log card play
                    Debug.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Opponent played card: {card.name} (Minion)");
                    
                    CardData minion = card.Clone();
                    minion.canAttack = false;
                    opponentBoardMinions.Add(minion);
                    opponentMana -= card.cost;
                    opponentHand.Remove(card);
                }
                else if (card.type == CardType.Spell)
                {
                    // Log card play
                    Debug.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Opponent played card: {card.name} (Spell - {card.effect})");
                    
                    if (card.effect == SpellEffect.Damage)
                    {
                        if (playerBoardMinions.Count > 0)
                        {
                            CardData target = playerBoardMinions[UnityEngine.Random.Range(0, playerBoardMinions.Count)];
                            DealDamage(target, card.value);
                        }
                        else
                        {
                            DealDamageToHero(true, card.value);
                        }
                    }
                    else if (card.effect == SpellEffect.Heal)
                    {
                        opponentHealth = Mathf.Min(30, opponentHealth + card.value);
                    }
                    opponentMana -= card.cost;
                    opponentHand.Remove(card);
                }
            }
        }
        
        // Log opponent's current hand
        string handContents = "Opponent Hand: ";
        if (opponentHand.Count == 0)
        {
            handContents += "Empty";
        }
        else
        {
            for (int i = 0; i < opponentHand.Count; i++)
            {
                handContents += opponentHand[i].name;
                if (i < opponentHand.Count - 1)
                {
                    handContents += ", ";
                }
            }
        }
        Debug.Log($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {handContents}");
        
        UpdateUI();
    }
    
    private void OpponentAttack(List<float> minionsAtStartOfTurn)
    {
        // Reset attack status for minions that were on board at start of turn
        foreach (CardData minion in opponentBoardMinions)
        {
            if (minionsAtStartOfTurn.Contains(minion.instanceId))
            {
                minion.canAttack = true;
            }
        }
        
        List<CardData> attackers = new List<CardData>();
        foreach (CardData minion in opponentBoardMinions)
        {
            if (minion.canAttack)
            {
                attackers.Add(minion);
            }
        }
        
        foreach (CardData attacker in attackers)
        {
            if (playerBoardMinions.Count > 0)
            {
                CardData target = playerBoardMinions[UnityEngine.Random.Range(0, playerBoardMinions.Count)];
                AttackMinion(attacker, target);
            }
            else
            {
                DealDamageToHero(true, attacker.attack);
                attacker.canAttack = false;
            }
        }
        
        UpdateUI();
    }
    
    private void EndOpponentTurn()
    {
        playerTurn = true;
        turn++;
        playerManaMax = Mathf.Min(10, Mathf.FloorToInt((turn + 1) / 2f));
        playerMana = playerManaMax;
        
        DrawCard(true);
        
        // Reset minion attack status
        foreach (CardData minion in playerBoardMinions)
        {
            minion.canAttack = true;
        }
        
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (playerHealthText != null) playerHealthText.text = playerHealth.ToString();
        if (playerManaText != null) playerManaText.text = playerMana.ToString();
        if (playerManaMaxText != null) playerManaMaxText.text = playerManaMax.ToString();
        
        if (opponentHealthText != null) opponentHealthText.text = opponentHealth.ToString();
        if (opponentManaText != null) opponentManaText.text = opponentMana.ToString();
        if (opponentManaMaxText != null) opponentManaMaxText.text = opponentManaMax.ToString();
        
        if (turnNumberText != null) turnNumberText.text = turn.ToString();
        if (turnIndicatorText != null) turnIndicatorText.text = playerTurn ? "Your Turn" : "Opponent's Turn";
        
        if (endTurnButton != null) endTurnButton.interactable = playerTurn;
        
        RenderHand();
        RenderBoard();
    }
    
    private void CheckGameOver()
    {
        if (playerHealth <= 0)
        {
            StartCoroutine(GameOver(false));
        }
        else if (opponentHealth <= 0)
        {
            StartCoroutine(GameOver(true));
        }
    }
    
    private IEnumerator GameOver(bool playerWon)
    {
        yield return new WaitForSeconds(0.5f);
        
        string message = playerWon ? "You Win!" : "You Lose!";
        Debug.Log(message);
        // You can add a UI panel here for game over
        
        // Restart game
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    
    // Getters for UI scripts
    public bool IsPlayerTurn() => playerTurn;
    public int GetPlayerMana() => playerMana;
    public CardData GetSelectedCard() => selectedCard;
    public List<CardData> GetPlayerBoard() => playerBoardMinions;
    public List<CardData> GetOpponentBoard() => opponentBoardMinions;
}

