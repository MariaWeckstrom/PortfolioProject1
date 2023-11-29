using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    private int currentLevel = 1;
    AlienManager alienManagerScript;
    public GameObject alienManager;
    PlayerHealth playerHealthScript;
    public GameObject player;

    private void Start()
    {
        levelText.text = $"Level {currentLevel}";
        alienManager = GameObject.FindWithTag("AlienManager");
        alienManagerScript = alienManager.GetComponent<AlienManager>();
        player = GameObject.FindWithTag("Player");
        playerHealthScript = player.GetComponent<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        if (alienManager.transform.childCount == 0)
        {
            currentLevel += 1;
            levelText.text = $"Level {currentLevel}";
            playerHealthScript.GetHealth(1);
            alienManagerScript.SpawnAliens();
        }
    }

}
