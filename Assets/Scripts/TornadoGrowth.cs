using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles tornado growth visuals based on XP gain.
/// </summary>
public class TornadoGrowth : MonoBehaviour
{
    [Header("XP Settings")]
    [Tooltip("XP needed to reach the next level.")]
    public float xpToNextLevel = 100f;
    [Tooltip("How much larger the tornado gets per level.")]
    public float scaleGrowth = 0.25f;
    [Tooltip("How much wider the XP bar gets per level.")]
    public float widthGrowth = 20f;

    [Header("UI References")]
    [Tooltip("Slider representing current XP progress.")]
    public Slider xpBar;
    [Tooltip("RectTransform of the slider for widening on level-up.")]
    public RectTransform xpBarRect;

    private float currentXP = 0f;

    void Start()
    {
        UpdateUI();
    }

    public void AddExperience(float amount)
    {
        currentXP += amount;

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        currentXP = 0f;
        xpToNextLevel *= 1.5f;

        transform.localScale += Vector3.one * scaleGrowth;

        if (xpBarRect != null)
        {
            xpBarRect.sizeDelta += new Vector2(widthGrowth, 0f);
        }
    }

    private void UpdateUI()
    {
        if (xpBar != null)
        {
            xpBar.maxValue = xpToNextLevel;
            xpBar.value = currentXP;
        }
    }
}

