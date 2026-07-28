using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RagebaitPopupSystem : MonoBehaviour
{
    public static RagebaitPopupSystem Instance { get; private set; }

    [SerializeField] private Canvas canvas;
    [SerializeField] private Font popupFont;
    [SerializeField] private float popupDuration = 3f;
    [SerializeField] private float popupSize = 80f;

    private List<string> ragebaitWords = new List<string>
    {
        "GIT GUD!",
        "L + RATIO",
        "NOOB!",
        "SKILL ISSUE!",
        "GET REKT!",
        "YOU DIED!",
        "DEFEATED!",
        "OUTPLAYED!",
        "WASHED UP!",
        "BAD LUCK!",
        "UNLUCKY!",
        "RIP",
        "GAME OVER",
        "FAIL!",
        "REKT!",
        "OWNED!",
        "TOO EASY!",
        "WHAT A PLAY!",
        "DESTROYED!",
        "DEMOLISHED!",
        "ANNIHILATED!",
        "OBLITERATED!",
        "CRUSHED!",
        "SMASHED!",
        "BLOWN AWAY!",
        "DELETED!",
        "ELIMINATED!",
        "VANQUISHED!",
        "SLAUGHTERED!",
        "BUTCHERED!",
        // MEGA RAGE INDUCER - Special intense messages
        "YOU'RE TRASH!",
        "TERRIBLE!",
        "PATHETIC!",
        "EMBARRASSING!",
        "SO BAD!",
        "UNBELIEVABLE!",
        "REALLY?!",
        "THAT WAS AWFUL!",
        "YIKES!",
        "BRUTAL!",
        "HUMILIATED!",
        "ABSOLUTE UNIT OF FAILURE!",
        "YOUR FRIENDS ARE LAUGHING!",
        "REPLAY THIS CLIP!",
        "SCREENSHOT THIS!",
        "SPEEDRUN: DIED IN SECONDS!",
        "WORLD RECORD DEATH!",
        "QUIT WHILE YOU'RE BEHIND!",
        "MAYBE GAMING ISN'T FOR YOU!",
        "EVEN BOTS PLAY BETTER!"
    };

    private List<string> megaRageWords = new List<string>
    {
        "YOU'RE TRASH!",
        "TERRIBLE!",
        "PATHETIC!",
        "EMBARRASSING!",
        "SO BAD!",
        "UNBELIEVABLE!",
        "REALLY?!",
        "THAT WAS AWFUL!",
        "YIKES!",
        "BRUTAL!",
        "HUMILIATED!",
        "ABSOLUTE UNIT OF FAILURE!",
        "YOUR FRIENDS ARE LAUGHING!",
        "REPLAY THIS CLIP!",
        "SCREENSHOT THIS!",
        "SPEEDRUN: DIED IN SECONDS!",
        "WORLD RECORD DEATH!",
        "QUIT WHILE YOU'RE BEHIND!",
        "MAYBE GAMING ISN'T FOR YOU!",
        "EVEN BOTS PLAY BETTER!"
    };

    private int deathCount = 0;
    private float lastDeathTime = 0f;

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

    public void ShowRagebaitPopup(Vector3 position)
    {
        deathCount++;
        lastDeathTime = Time.time;
        
        // Determine if should use mega rage words
        bool useMegaRage = ShouldUseMegaRage();
        
        List<string> wordList = useMegaRage ? megaRageWords : ragebaitWords;
        string randomWord = wordList[Random.Range(0, wordList.Count)];
        
        StartCoroutine(DisplayPopup(randomWord, position, useMegaRage));
    }

    private bool ShouldUseMegaRage()
    {
        // Use mega rage if:
        // 1. Died 3+ times in quick succession (within 2 minutes)
        // 2. Died 5+ times in a game session
        // 3. Random chance increases with death count
        
        if (deathCount >= 5)
            return true;
            
        float timeSinceLastDeath = Time.time - lastDeathTime;
        if (deathCount >= 3 && timeSinceLastDeath < 120f) // Within 2 minutes
            return true;
            
        if (deathCount >= 2 && Random.value < 0.3f)
            return true;
            
        return false;
    }

    private IEnumerator DisplayPopup(string text, Vector3 worldPosition, bool isMegaRage = false)
    {
        // Create a temporary GameObject for the text
        GameObject popupObj = new GameObject("RagebaitPopup");
        popupObj.transform.SetParent(canvas.transform, false);

        // Add Text component
        Text textComponent = popupObj.AddComponent<Text>();
        textComponent.text = text;
        textComponent.font = popupFont;
        textComponent.fontSize = (int)(popupSize * (isMegaRage ? 1.3f : 1f));
        textComponent.fontStyle = isMegaRage ? FontStyle.Bold : FontStyle.Normal;
        textComponent.alignment = TextAnchor.MiddleCenter;

        // Color selection - intense colors for mega rage
        Color selectedColor;
        if (isMegaRage)
        {
            // Mega rage uses hot, angry colors
            Color[] megaRageColors = new Color[]
            {
                Color.red,
                new Color(1f, 0f, 0f),      // Pure red
                new Color(1f, 0.3f, 0f),    // Red-orange
                new Color(1f, 0f, 0.5f),    // Red-pink
                Color.black
            };
            selectedColor = megaRageColors[Random.Range(0, megaRageColors.Length)];
        }
        else
        {
            Color[] colors = new Color[]
            {
                Color.red,
                Color.yellow,
                new Color(1f, 0.5f, 0f), // Orange
                new Color(1f, 0f, 1f),   // Magenta
                new Color(0f, 1f, 1f),   // Cyan
                Color.white
            };
            selectedColor = colors[Random.Range(0, colors.Length)];
        }
        
        textComponent.color = selectedColor;

        // Add outline for better visibility
        Outline outline = popupObj.AddComponent<Outline>();
        outline.effectColor = isMegaRage ? Color.black : new Color(0, 0, 0, 0.7f);
        outline.effectDistance = new Vector2(isMegaRage ? 3 : 2, isMegaRage ? 3 : 2);

        // Add shadow effect for mega rage
        if (isMegaRage)
        {
            Shadow shadow = popupObj.AddComponent<Shadow>();
            shadow.effectColor = Color.black;
            shadow.effectDistance = new Vector2(2, -2);
        }

        // Position the popup
        RectTransform rectTransform = popupObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(Random.Range(-200f, 200f), Random.Range(-100f, 100f));
        rectTransform.sizeDelta = new Vector2(500f, 250f);

        // Extended duration for mega rage
        float duration = isMegaRage ? popupDuration * 1.2f : popupDuration;

        // Animation: Scale and fade out
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;

            // Aggressive scale animation for mega rage
            float scale;
            if (isMegaRage)
            {
                // Bounce effect for mega rage
                scale = 1f + (Mathf.Sin(progress * Mathf.PI * 2) * 0.2f) + (progress * 0.3f);
            }
            else
            {
                scale = 1f + (progress * 0.5f);
            }
            
            rectTransform.localScale = new Vector3(scale, scale, scale);

            // Fade out
            Color newColor = textComponent.color;
            newColor.a = 1f - progress;
            textComponent.color = newColor;

            // Move upward more aggressively for mega rage
            float moveSpeed = isMegaRage ? 80f : 50f;
            Vector2 currentPos = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPos.x, currentPos.y + (moveSpeed * Time.deltaTime));

            // Screen shake effect for mega rage
            if (isMegaRage && progress < 0.5f)
            {
                float shake = Mathf.Sin(elapsed * 20f) * 5f;
                rectTransform.anchoredPosition = new Vector2(
                    rectTransform.anchoredPosition.x + shake,
                    rectTransform.anchoredPosition.y
                );
            }

            yield return null;
        }

        Destroy(popupObj);
    }

    public void ShowDeathMessage()
    {
        ShowRagebaitPopup(Vector3.zero);
    }

    public void ResetDeathCount()
    {
        deathCount = 0;
    }

    public List<string> GetAllRagebaitWords()
    {
        return ragebaitWords;
    }

    public void AddCustomRagebaitWord(string word)
    {
        if (!ragebaitWords.Contains(word))
        {
            ragebaitWords.Add(word);
        }
    }

    public int GetDeathCount()
    {
        return deathCount;
    }

    public bool IsMegaRageMode()
    {
        return ShouldUseMegaRage();
    }
}
