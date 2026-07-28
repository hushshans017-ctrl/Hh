using UnityEngine;
using System.Collections.Generic;

public class CharacterSkinSystem : MonoBehaviour
{
    [System.Serializable]
    public class CharacterSkin
    {
        public string skinName;
        public string rarity; // Common, Rare, Epic, Legendary
        public Sprite skinSprite;
        public Color primaryColor;
        public Color secondaryColor;
        public int cost; // In-game currency
        public bool isUnlocked;
        public string description;
        public Sprite thumbnailSprite;
    }

    public static CharacterSkinSystem Instance { get; private set; }

    [SerializeField] private List<CharacterSkin> availableSkins = new List<CharacterSkin>();
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    
    private int currentSkinIndex = 0;
    private CharacterSkin currentSkin;
    private int playerCurrency = 5000; // Starting currency

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitializeSkins();
        SelectSkin(0);
    }

    private void InitializeSkins()
    {
        // Default Commando Skin
        CharacterSkin commando = new CharacterSkin
        {
            skinName = "Commando",
            rarity = "Common",
            primaryColor = new Color(0.2f, 0.8f, 0.2f),
            secondaryColor = new Color(0.1f, 0.5f, 0.1f),
            cost = 0,
            isUnlocked = true,
            description = "A basic soldier ready for combat"
        };
        availableSkins.Add(commando);

        // Knight Skin
        CharacterSkin knight = new CharacterSkin
        {
            skinName = "Knight",
            rarity = "Rare",
            primaryColor = new Color(0.7f, 0.7f, 0.7f),
            secondaryColor = new Color(0.4f, 0.4f, 0.4f),
            cost = 1200,
            isUnlocked = false,
            description = "Medieval warrior in full armor"
        };
        availableSkins.Add(knight);

        // Ninja Skin
        CharacterSkin ninja = new CharacterSkin
        {
            skinName = "Ninja",
            rarity = "Epic",
            primaryColor = new Color(0.1f, 0.1f, 0.2f),
            secondaryColor = new Color(0.3f, 0.0f, 0.3f),
            cost = 2000,
            isUnlocked = false,
            description = "Stealthy shinobi assassin"
        };
        availableSkins.Add(ninja);

        // Cyborg Skin
        CharacterSkin cyborg = new CharacterSkin
        {
            skinName = "Cyborg",
            rarity = "Epic",
            primaryColor = new Color(0.8f, 0.2f, 0.2f),
            secondaryColor = new Color(0.3f, 0.3f, 0.3f),
            cost = 2000,
            isUnlocked = false,
            description = "High-tech android soldier"
        };
        availableSkins.Add(cyborg);

        // Space Explorer Skin
        CharacterSkin explorer = new CharacterSkin
        {
            skinName = "Space Explorer",
            rarity = "Epic",
            primaryColor = new Color(0.0f, 0.7f, 1.0f),
            secondaryColor = new Color(0.2f, 0.9f, 1.0f),
            cost = 2000,
            isUnlocked = false,
            description = "Intergalactic adventurer"
        };
        availableSkins.Add(explorer);

        // Demon Skin
        CharacterSkin demon = new CharacterSkin
        {
            skinName = "Demon",
            rarity = "Legendary",
            primaryColor = new Color(1.0f, 0.2f, 0.0f),
            secondaryColor = new Color(0.5f, 0.0f, 0.0f),
            cost = 3500,
            isUnlocked = false,
            description = "Dark supernatural entity with immense power"
        };
        availableSkins.Add(demon);

        // Pharaoh Skin
        CharacterSkin pharaoh = new CharacterSkin
        {
            skinName = "Pharaoh",
            rarity = "Legendary",
            primaryColor = new Color(1.0f, 0.8f, 0.0f),
            secondaryColor = new Color(0.8f, 0.6f, 0.0f),
            cost = 3500,
            isUnlocked = false,
            description = "Ancient Egyptian king with golden regalia"
        };
        availableSkins.Add(pharaoh);

        // Ice Queen Skin
        CharacterSkin iceQueen = new CharacterSkin
        {
            skinName = "Ice Queen",
            rarity = "Legendary",
            primaryColor = new Color(0.6f, 0.9f, 1.0f),
            secondaryColor = new Color(0.3f, 0.6f, 0.9f),
            cost = 3500,
            isUnlocked = false,
            description = "Frozen sovereign of winter"
        };
        availableSkins.Add(iceQueen);

        // Shadow Assassin Skin
        CharacterSkin shadowAssassin = new CharacterSkin
        {
            skinName = "Shadow Assassin",
            rarity = "Legendary",
            primaryColor = new Color(0.2f, 0.2f, 0.3f),
            secondaryColor = new Color(0.8f, 0.0f, 0.8f),
            cost = 3500,
            isUnlocked = false,
            description = "Master of darkness and deception"
        };
        availableSkins.Add(shadowAssassin);

        // Mythic Dragon Slayer Skin
        CharacterSkin dragonSlayer = new CharacterSkin
        {
            skinName = "Dragon Slayer",
            rarity = "Legendary",
            primaryColor = new Color(0.9f, 0.1f, 0.1f),
            secondaryColor = new Color(1.0f, 0.6f, 0.0f),
            cost = 4000,
            isUnlocked = false,
            description = "Legendary warrior who hunts mythical beasts"
        };
        availableSkins.Add(dragonSlayer);
    }

    public void SelectSkin(int index)
    {
        if (index >= 0 && index < availableSkins.Count)
        {
            currentSkinIndex = index;
            currentSkin = availableSkins[index];

            if (playerSpriteRenderer != null && currentSkin.skinSprite != null)
            {
                playerSpriteRenderer.sprite = currentSkin.skinSprite;
            }

            // Apply color tint to player
            if (playerSpriteRenderer != null)
            {
                playerSpriteRenderer.color = currentSkin.primaryColor;
            }

            Debug.Log("Selected Skin: " + currentSkin.skinName + " (" + currentSkin.rarity + ")");
        }
    }

    public bool UnlockSkin(int index)
    {
        if (index >= 0 && index < availableSkins.Count)
        {
            CharacterSkin skin = availableSkins[index];
            
            if (skin.isUnlocked)
            {
                Debug.Log("Skin already unlocked!");
                return false;
            }

            if (playerCurrency >= skin.cost)
            {
                playerCurrency -= skin.cost;
                skin.isUnlocked = true;
                Debug.Log("Unlocked: " + skin.skinName + "! Remaining currency: " + playerCurrency);
                return true;
            }
            else
            {
                Debug.Log("Not enough currency! Need: " + skin.cost + ", Have: " + playerCurrency);
                return false;
            }
        }
        return false;
    }

    public void NextSkin()
    {
        int nextIndex = (currentSkinIndex + 1) % availableSkins.Count;
        SelectSkin(nextIndex);
    }

    public void PreviousSkin()
    {
        currentSkinIndex--;
        if (currentSkinIndex < 0)
            currentSkinIndex = availableSkins.Count - 1;
        SelectSkin(currentSkinIndex);
    }

    public CharacterSkin GetCurrentSkin()
    {
        return currentSkin;
    }

    public CharacterSkin GetSkin(int index)
    {
        if (index >= 0 && index < availableSkins.Count)
            return availableSkins[index];
        return null;
    }

    public List<CharacterSkin> GetAllSkins()
    {
        return availableSkins;
    }

    public int GetPlayerCurrency()
    {
        return playerCurrency;
    }

    public void AddCurrency(int amount)
    {
        playerCurrency += amount;
    }

    public int GetUnlockedSkinCount()
    {
        int count = 0;
        foreach (CharacterSkin skin in availableSkins)
        {
            if (skin.isUnlocked)
                count++;
        }
        return count;
    }
}
