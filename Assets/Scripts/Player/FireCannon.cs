using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    [SerializeField] float m_fireRate = 0.25f;
    [SerializeField] Transform m_muzzleTransform;
    [SerializeField] GameObject m_projectile;

    void Update()
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
                canFire = false;
                yield return new WaitForSecondsRealtime(m_fireRate);
                canFire = true;
            }
        }
    }
}
