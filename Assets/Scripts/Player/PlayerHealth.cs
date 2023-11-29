using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] public int m_maxHealth = 5;
    public int playerHealth;

    public Image[] uiHearts;
    public Sprite heart_image;

    private void Start()
    {
        playerHealth = m_maxHealth;
    }

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        if (playerHealth <= 0) 
        {
            Debug.Log("died");
            //tragic death here
        }
    }

    public void GetHealth(int amount)
    {
        if (playerHealth + amount <= m_maxHealth)
        {
            playerHealth += amount;
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < uiHearts.Length; i++)
        {
            if (i < playerHealth)
            {
                uiHearts[i].enabled = true;
            }
            else
            {
                uiHearts[i].enabled = false;
            }
        }
        //Debug.Log(playerHealth);
    }
}
