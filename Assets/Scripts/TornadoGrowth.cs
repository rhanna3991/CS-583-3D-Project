using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TornadoGrowth : MonoBehaviour
{
    [Header("XP Milestones")]
    [Tooltip("The total XP required to reach each size level.")]
    public List<float> milestones = new List<float> { 100f, 300f, 600f, 1000f, 1500f };
    
    [Header("Growth Settings")]
    public float startScale = 0.1f; // The size at 0 XP
    public float growthPerLevel = 0.1f; // How much size adds per milestone passed

    [Header("UI Settings")]
    public Slider xpBar;
    public RectTransform xpBarRect;
    public float baseBarWidth = 100f; // Width of the bar for the first milestone (100 XP)
    
    [Header("Game Over Settings")]
    public string mainMenuSceneName = "MainMenu";

    // Read-only property to see total XP in Inspector (for debugging)
    [SerializeField] private float _currentTotalXP = 0f;
    public float CurrentTotalXP => _currentTotalXP;

    void Start()
    {
        // Initialize visuals based on starting XP
        UpdateState();
    }

    public void AddExperience(float amount)
    {
        _currentTotalXP += amount;
        UpdateState();
    }

    public void RemoveExperience(float amount)
    {
        _currentTotalXP -= amount;
        UpdateState();
        
        // Check if game is over (XP is 0 or below)
        if (_currentTotalXP <= 0)
        {
            GameOver();
        }
    }
    
    void GameOver()
    {
        // Load the MainMenu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // Calculates Level, Size, and UI based on Total XP
    private void UpdateState()
    {
        int currentLevel = 0;
        float nextMilestone = milestones[0];

        // 1. Determine Level by checking which milestones we've passed
        for (int i = 0; i < milestones.Count; i++)
        {
            if (_currentTotalXP >= milestones[i])
            {
                // Milestone passed
                currentLevel = i + 1;
                
                // Determine what the next milestone is
                if (i + 1 < milestones.Count)
                {
                    nextMilestone = milestones[i + 1];
                }
                else
                {
                    // Max level reached: Create a "fake" higher milestone
                    nextMilestone = milestones[i] * 1.5f; 
                }
            }
            else
            {
                nextMilestone = milestones[i];
                break; // Stop checking
            }
        }

        // 2. Set Physical Size
        // Formula: Base 0.1 + (0.1 * Level)
        float targetScale = startScale + (currentLevel * growthPerLevel);
        transform.localScale = Vector3.one * targetScale;

        // 3. Calculate level-specific values for UI
        float previousMilestone = currentLevel == 0 ? 0f : milestones[currentLevel - 1];
        float xpThisLevel = _currentTotalXP - previousMilestone;
        float levelRange = nextMilestone - previousMilestone;

        // 4. Update UI Bar Values
        if (xpBar != null)
        {
            // Bar shows XP progress within the current level, not total XP
            xpBar.maxValue = levelRange;
            xpBar.value = xpThisLevel;
        }

        // Update bar width scaling based on the current level's XP range relative to the first level's range
        if (xpBarRect != null)
        {
            float firstRange = milestones[0] - 0f;
            float widthRatio = levelRange / firstRange;
            
            float newWidth = baseBarWidth * widthRatio;
            xpBarRect.sizeDelta = new Vector2(newWidth, xpBarRect.sizeDelta.y);
        }
    }
}
