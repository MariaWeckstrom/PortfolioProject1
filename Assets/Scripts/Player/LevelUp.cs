using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class LevelUp : MonoBehaviour
{
    //UnityEvent onLevelUp;

    public TextMeshProUGUI levelText;
    private int currentLevel = 1;
    private int maxLevel = 10;
    AlienManager alienManagerScript;
    AlienFire alienFireScript;
    public GameObject alienManager;
    PlayerHealth playerHealthScript;
    //FireCannon fireCannonScript;
    public GameObject player;
    AudioSource music;

    private void Start()
    {
        levelText.text = $"Level {currentLevel}";
        alienManager = GameObject.FindWithTag("AlienManager");
        alienManagerScript = alienManager.GetComponent<AlienManager>();
        alienFireScript = alienManager.GetComponent<AlienFire>();
        player = GameObject.FindWithTag("Player");
        playerHealthScript = player.GetComponent<PlayerHealth>();
        //fireCannonScript = player.GetComponent<FireCannon>();
        music = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();
        music.pitch = 0.75f;
    }

    private void FixedUpdate()
    {
        if (alienManager.transform.childCount == 0)
        {
            foreach (GameObject projectile in GameObject.FindGameObjectsWithTag("Projectile"))
            {
                Destroy(projectile);
            }
            if (currentLevel+1 > maxLevel)
            {
                StartCoroutine(VictoryScreen());
            }
            else
            {
                currentLevel += 1;
                levelText.text = $"Level {currentLevel}";
                playerHealthScript.GetHealth(1);
                alienFireScript.OnLevelUp(currentLevel, maxLevel);
                //fireCannonScript.SetCanFire(false);
                alienManagerScript.SpawnAliens();
                music.pitch += 0.025f;
            }
            //onLevelUp.Invoke();
        }
    }
    private IEnumerator VictoryScreen()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }

}
