using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;

    private void FixedUpdate()
    {
        Camera cam = Camera.main;
        float aspect = (float)Screen.width / Screen.height;
        float worldHeight = cam.orthographicSize;
        float worldWidth = worldHeight * aspect;
        float direction = Input.GetAxis("Horizontal");
        float y_pos = transform.position.y;
        transform.position += new Vector3(direction * m_speed * Time.fixedDeltaTime, 0, 0);
        float borderOffset = worldWidth - transform.localScale.x * 0.5f;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, cam.transform.position.x - borderOffset, borderOffset + cam.transform.position.x), y_pos, 0);
    }
}
