using UnityEngine;
using System.Collections.Generic;

public class MiniGameManager : MonoBehaviour
{
    [System.Serializable]
    public class MiniGame
    {
        public string gameName;
        public string description;
        public int vbucksReward;
        public int xpReward;
        public float playtimeDuration;
        public string updateVersion;
        public bool isActive;
        public int maxPlaysPerDay;
        public int difficulty; // 1-5
    }

    public static MiniGameManager Instance { get; private set; }

    private List<MiniGame> availableMiniGames = new List<MiniGame>();
    private Dictionary<string, int> dailyPlayCounts = new Dictionary<string, int>();
    private int totalVBucksEarned = 0;

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
        InitializeMiniGames();
    }

    private void InitializeMiniGames()
    {
        // ============ UPDATE 1.0 - LAUNCH MINI GAMES ============

        MiniGame clickerGame = new MiniGame
        {
            gameName = "Clicker Frenzy",
            description = "Tap as fast as you can in 30 seconds",
            vbucksReward = 50,
            xpReward = 100,
            playtimeDuration = 30f,
            updateVersion = "1.0",
            isActive = true,
            maxPlaysPerDay = 3,
            difficulty = 1
        };
        availableMiniGames.Add(clickerGame);

        MiniGame memoryGame = new MiniGame
        {
            gameName = "Memory Match",
            description = "Match pairs of cards before time runs out",
            vbucksReward = 75,
            xpReward = 150,
            playtimeDuration = 45f,
            updateVersion = "1.0",
            isActive = true,
            maxPlaysPerDay = 2,
            difficulty = 2
        };
        availableMiniGames.Add(memoryGame);

        // ============ UPDATE 1.1 - NEW MINI GAMES ============

        MiniGame dodgeGame = new MiniGame
        {
            gameName = "Dodge Master",
            description = "Avoid falling obstacles for 40 seconds",
            vbucksReward = 100,
            xpReward = 200,
            playtimeDuration = 40f,
            updateVersion = "1.1",
            isActive = true,
            maxPlaysPerDay = 3,
            difficulty = 3
        };
        availableMiniGames.Add(dodgeGame);

        MiniGame catchGame = new MiniGame
        {
            gameName = "Catch The Coins",
            description = "Collect 50 coins in 60 seconds",
            vbucksReward = 80,
            xpReward = 160,
            playtimeDuration = 60f,
            updateVersion = "1.1",
            isActive = true,
            maxPlaysPerDay = 2,
            difficulty = 2
        };
        availableMiniGames.Add(catchGame);

        // ============ UPDATE 1.2 - RHYTHM & QUICK REFLEXES ============

        MiniGame rhythmGame = new MiniGame
        {
            gameName = "Beat Clapper",
            description = "Tap in rhythm with the music (8 beats)",
            vbucksReward = 120,
            xpReward = 250,
            playtimeDuration = 20f,
            updateVersion = "1.2",
            isActive = true,
            maxPlaysPerDay = 3,
            difficulty = 3
        };
        availableMiniGames.Add(rhythmGame);

        MiniGame reactionGame = new MiniGame
        {
            gameName = "Reaction Time",
            description = "Click as fast as possible when the light turns green",
            vbucksReward = 60,
            xpReward = 120,
            playtimeDuration = 15f,
            updateVersion = "1.2",
            isActive = true,
            maxPlaysPerDay = 5,
            difficulty = 1
        };
        availableMiniGames.Add(reactionGame);

        // ============ UPDATE 1.3 - PUZZLE & STRATEGY ============

        MiniGame puzzleGame = new MiniGame
        {
            gameName = "Puzzle Rush",
            description = "Complete 5 puzzles as fast as you can",
            vbucksReward = 110,
            xpReward = 220,
            playtimeDuration = 90f,
            updateVersion = "1.3",
            isActive = true,
            maxPlaysPerDay = 2,
            difficulty = 3
        };
        availableMiniGames.Add(puzzleGame);

        MiniGame colorGame = new MiniGame
        {
            gameName = "Color Match",
            description = "Match colors faster and faster",
            vbucksReward = 85,
            xpReward = 170,
            playtimeDuration = 45f,
            updateVersion = "1.3",
            isActive = true,
            maxPlaysPerDay = 4,
            difficulty = 2
        };
        availableMiniGames.Add(colorGame);

        // ============ UPDATE 1.4 - COMPETITIVE CHALLENGES ============

        MiniGame shootingGame = new MiniGame
        {
            gameName = "Target Practice",
            description = "Hit 20 targets with perfect accuracy",
            vbucksReward = 150,
            xpReward = 300,
            playtimeDuration = 60f,
            updateVersion = "1.4",
            isActive = true,
            maxPlaysPerDay = 2,
            difficulty = 4
        };
        availableMiniGames.Add(shootingGame);

        MiniGame survivalGame = new MiniGame
        {
            gameName = "Survival Gauntlet",
            description = "Survive waves of enemies for 120 seconds",
            vbucksReward = 200,
            xpReward = 400,
            playtimeDuration = 120f,
            updateVersion = "1.4",
            isActive = true,
            maxPlaysPerDay = 1,
            difficulty = 5
        };
        availableMiniGames.Add(survivalGame);

        // ============ UPDATE 1.5 - BONUS MINI GAMES ============

        MiniGame wordGame = new MiniGame
        {
            gameName = "Word Blast",
            description = "Type words as fast as possible",
            vbucksReward = 90,
            xpReward = 180,
            playtimeDuration = 45f,
            updateVersion = "1.5",
            isActive = true,
            maxPlaysPerDay = 3,
            difficulty = 2
        };
        availableMiniGames.Add(wordGame);

        MiniGame balloonGame = new MiniGame
        {
            gameName = "Balloon Popper",
            description = "Pop as many balloons as you can",
            vbucksReward = 70,
            xpReward = 140,
            playtimeDuration = 30f,
            updateVersion = "1.5",
            isActive = true,
            maxPlaysPerDay = 4,
            difficulty = 1
        };
        availableMiniGames.Add(balloonGame);
    }

    public void PlayMiniGame(int gameIndex)
    {
        if (gameIndex >= 0 && gameIndex < availableMiniGames.Count)
        {
            MiniGame game = availableMiniGames[gameIndex];

            // Check if player has plays remaining
            if (!dailyPlayCounts.ContainsKey(game.gameName))
            {
                dailyPlayCounts[game.gameName] = 0;
            }

            if (dailyPlayCounts[game.gameName] >= game.maxPlaysPerDay)
            {
                Debug.Log("Daily limit reached for " + game.gameName);
                return;
            }

            Debug.Log("Starting: " + game.gameName);
            dailyPlayCounts[game.gameName]++;

            // Start mini game
            StartCoroutine(RunMiniGame(game));
        }
    }

    private System.Collections.IEnumerator RunMiniGame(MiniGame game)
    {
        Debug.Log("Playing: " + game.gameName + " for " + game.playtimeDuration + " seconds");
        
        // Simulate mini game duration
        yield return new WaitForSeconds(game.playtimeDuration);

        // Award V-Bucks on completion
        AwardVBucks(game.vbucksReward, game.gameName);
        AwardXP(game.xpReward, game.gameName);
        
        Debug.Log("Completed: " + game.gameName + " - Earned " + game.vbucksReward + " V-Bucks!");
    }

    private void AwardVBucks(int amount, string gameName)
    {
        totalVBucksEarned += amount;
        CharacterSkinSystem.Instance.AddCurrency(amount);
        Debug.Log("Awarded " + amount + " V-Bucks from " + gameName);
    }

    private void AwardXP(int amount, string gameName)
    {
        // Award XP to player progression system
        Debug.Log("Awarded " + amount + " XP from " + gameName);
    }

    public List<MiniGame> GetAvailableMiniGames()
    {
        return availableMiniGames;
    }

    public List<MiniGame> GetMiniGamesByVersion(string version)
    {
        List<MiniGame> versionGames = new List<MiniGame>();
        foreach (MiniGame game in availableMiniGames)
        {
            if (game.updateVersion == version && game.isActive)
            {
                versionGames.Add(game);
            }
        }
        return versionGames;
    }

    public int GetDailyPlaysRemaining(int gameIndex)
    {
        if (gameIndex >= 0 && gameIndex < availableMiniGames.Count)
        {
            MiniGame game = availableMiniGames[gameIndex];
            if (!dailyPlayCounts.ContainsKey(game.gameName))
                return game.maxPlaysPerDay;
            
            return game.maxPlaysPerDay - dailyPlayCounts[game.gameName];
        }
        return 0;
    }

    public int GetTotalVBucksEarned()
    {
        return totalVBucksEarned;
    }

    public void ResetDailyLimits()
    {
        dailyPlayCounts.Clear();
        Debug.Log("Daily mini game limits reset!");
    }

    public MiniGame GetMiniGame(int index)
    {
        if (index >= 0 && index < availableMiniGames.Count)
            return availableMiniGames[index];
        return null;
    }
}
