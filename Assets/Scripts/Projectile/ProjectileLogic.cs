using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    [SerializeField] private float m_projectileSpeed = 20f;

    private void FixedUpdate()
    {
        transform.position += Vector3.up * m_projectileSpeed * Time.fixedDeltaTime;
    }
}
