using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienProjectileLogic : MonoBehaviour
{
    [SerializeField] private float m_projectileSpeed = 20f;
    [SerializeField] private int m_damage = 1;

    private void FixedUpdate()
    {
        transform.position += Vector3.down * m_projectileSpeed * Time.fixedDeltaTime;

        //destroy projectile when it's out of sight
        Camera cam = Camera.main;
        float aspect = (float)Screen.width / Screen.height;
        float worldHeight = cam.orthographicSize;
        float borderOffset = - worldHeight - transform.localScale.y * 0.5f;
        if (transform.position.y < borderOffset) { Destroy(this.gameObject); }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(m_damage);
                Destroy(this.gameObject);
            }
        }
    }
}
