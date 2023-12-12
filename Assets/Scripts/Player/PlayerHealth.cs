using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] public int m_maxHealth = 5;
    public int playerHealth;

    public Image[] uiHearts;
    //public Sprite heartImage;
    public Sprite[] heartSprites;

    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    private bool canTakeDamage = true;
    float damageTimer = 0f;
    float damageCooldown = 1f;

    public GameObject explosion;

    private void Start()
    {
        playerHealth = m_maxHealth;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        audioSource = this.GetComponent<AudioSource>();
    }

    public void TakeDamage(int amount)
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            //spriteRenderer.color = new Color32(80, 150, 60, 255);
            spriteRenderer.color = new Color32(255, 124, 136, 255);
            //spriteRenderer.color = Color.red;
            playerHealth -= amount;
            //GetComponent<AudioSource>().clip = hurtSound;
            audioSource.pitch = Random.Range(0.9f, 1f);
            audioSource.volume = 0.8f;
            if (playerHealth <= 0)
            {
                PlayerDeath();
            }
            else
            {
                audioSource.Play();
            }
        }
    }

    public void PlayerDeath()
    {
        playerHealth = 0;
        GameObject explosionInst = Instantiate(explosion);
        explosionInst.transform.position = gameObject.transform.position;
        GetComponent<FireCannon>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        spriteRenderer.color = Color.clear;
        Debug.Log("died");
        StartCoroutine(DeathScreen());

    }

    private IEnumerator DeathScreen()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(3);
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
        if (!canTakeDamage && playerHealth > 0)
        {
            damageTimer += Time.fixedDeltaTime;
        }
        if (!canTakeDamage && damageTimer >= damageCooldown/2) 
        {
            spriteRenderer.color = Color.white;
        }
        if (damageTimer >= damageCooldown)
        {
            canTakeDamage = true;
            damageTimer = 0f;  
        }

        for (int i = 0; i < uiHearts.Length; i++)
        {
            if (i < playerHealth)
            {
                //uiHearts[i].enabled = true;
                uiHearts[i].sprite = heartSprites[0];
            }
            else
            {
                uiHearts[i].sprite = heartSprites[1];
            }
        }
        //Debug.Log(playerHealth);
    }
}
