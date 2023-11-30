using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    private int currentLevel = 1;

    private void Start()
    {
        levelText.text = $"Level {currentLevel}";
        GameManager.Instance.onLevelUp.AddListener(OnLevelUp);
    }

    void OnLevelUp(int level, int _max)
    {
        currentLevel = level;
        levelText.text = $"Level {currentLevel}";
    }

}
