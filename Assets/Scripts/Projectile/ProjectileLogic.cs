using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    [SerializeField] private float m_projectileSpeed = 20f;

    private AlienEffects alienScript;

    private void FixedUpdate()
    {
        transform.position += Vector3.up * m_projectileSpeed * Time.fixedDeltaTime;

        //destroy projectile when it's out of sight
        Camera cam = Camera.main;
        float aspect = (float)Screen.width / Screen.height;
        float worldHeight = cam.orthographicSize;
        float borderOffset = worldHeight + transform.localScale.y * 0.5f;
        if (transform.position.y > borderOffset) { Destroy(this.gameObject); }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Alien")
        {
            alienScript = other.gameObject.GetComponent<AlienEffects>();
            alienScript.OnDeath();
            //Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
