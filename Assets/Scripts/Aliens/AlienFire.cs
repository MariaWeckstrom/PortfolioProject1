using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFire : MonoBehaviour
{
    [SerializeField] float m_maxFireRate = 2f;
    [SerializeField] float m_minFireRate = 5f;
    [SerializeField] Transform m_muzzleTransform;
    [SerializeField] GameObject m_projectile;
    [SerializeField] AnimationCurve m_fireRateMultiplierCurve;
    bool canFire = true;
    private float normalizedFireRateDifficulty = 0f;

    private void Start()
    {
        GameManager.Instance.onLevelUp.AddListener(OnLevelUp);
    }

    void FixedUpdate()
    {
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {
        int chosenChild = Random.Range(0, this.transform.childCount);
        if (canFire)
        { 
            if (this.transform.childCount >= chosenChild)
            {
                Vector3 spawnPosition = this.transform.GetChild(chosenChild).transform.position;
                if (m_muzzleTransform)
                {
                    spawnPosition = m_muzzleTransform.position;
                }
                if (m_projectile)
                {
                    GameObject projectileInst = Instantiate(m_projectile);
                    projectileInst.transform.position = spawnPosition;
                    canFire = false;
                    float fireRateMultiplier = m_fireRateMultiplierCurve.Evaluate(normalizedFireRateDifficulty);
                    float firingInterval = Random.Range(m_maxFireRate, m_minFireRate) * fireRateMultiplier;
                    yield return new WaitForSecondsRealtime(firingInterval);
                    canFire = true;
                }
            }
        }
    }

    void OnLevelUp(int currentLevel, int maxLevel)
    {
        normalizedFireRateDifficulty = Mathf.Clamp((float)currentLevel / (float)maxLevel, 0f, 1f);
        Debug.Log(normalizedFireRateDifficulty);
        Debug.Log(currentLevel);
        Debug.Log(maxLevel);
    }
}
