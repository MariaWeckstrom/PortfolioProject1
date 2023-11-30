using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnLevelUpEvent : UnityEvent<int, int> { }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] int m_maxDifficultyLevel = 10;
    private int currentLevel = 1;
    AlienManager alienManagerScript;
    public GameObject alienManager;
    PlayerHealth playerHealthScript;
    public GameObject player;
    public OnLevelUpEvent onLevelUp = new OnLevelUpEvent();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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
            playerHealthScript.GetHealth(1);
            alienManagerScript.SpawnAliens();
            onLevelUp.Invoke(currentLevel, m_maxDifficultyLevel);
        }
    }
}
