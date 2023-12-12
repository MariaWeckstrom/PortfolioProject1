# Space War

## Overview
This game was made as a school project. The goal was to independently create a game that takes inspiration from a classic game. The inspiration for this game was Space Invaders. The game was made using Unity.

## Screenshots
![image](https://github.com/MariaWeckstrom/PortfolioProject1/assets/111729331/689a6b14-9f7a-4216-b414-60c5526824ec)
![image](https://github.com/MariaWeckstrom/PortfolioProject1/assets/111729331/3f45cfe3-642c-42db-9496-bb8e2780cdc7)

## Demo video
[Demo video on YouTube](https://youtu.be/gB-jNkPDpsw)

## Itch.io
[Play the game at Itch.io](https://mariaweckstrom.itch.io/space-war)

## How to play
- Use WASD or arrow keys to move left and right
- Use spacebar to shoot
- Complete 10 levels to win


## Code sample
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    [SerializeField] float m_fireRate = 0.5f;
    [SerializeField] Transform m_muzzleTransform;
    [SerializeField] GameObject m_projectile;
    AudioSource audioSource;

    void Start()
    {
        audioSource = this.transform.GetChild(0).GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire"))
        {
            StartCoroutine(Shoot());
        }
    }

    bool canFire = true;
    private IEnumerator Shoot()
    {
        if (canFire)
        {
            Vector3 spawnPosition = transform.position;
            if (m_muzzleTransform)
            {
                spawnPosition = m_muzzleTransform.position;
            }
            if (m_projectile)
            {
                GameObject projectileInst = Instantiate(m_projectile);
                projectileInst.transform.position = spawnPosition;
                audioSource.pitch = Random.Range(1f, 1.2f);
                audioSource.volume = Random.Range(0.1f, 0.2f);
                audioSource.Play();
                canFire = false;
                yield return new WaitForSecondsRealtime(m_fireRate);
                canFire = true;
            }
        }
    }
}
```
