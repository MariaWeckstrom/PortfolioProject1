using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AlienManager : MonoBehaviour
{
    [SerializeField] List<GameObject> m_alienTypes = new List<GameObject>();
    [SerializeField] Vector2 m_colsAndRows = Vector2.zero;
    [SerializeField] Vector2 m_padding = Vector2.zero;
    [SerializeField] Vector3 parentPosition = Vector3.zero;
    private float movementAmountX = 0.3f;
    private float movementAmountY = -0.5f;
    private bool directionSwitch = false;
    private bool newSpeed = true;
    private float movementSpeedSeconds = 0.5f;
    private float accelerationMultiplier = 0.95f;
    private int childrenCount = 0;

    void Start()
    {
        parentPosition = transform.position;
        SpawnAliens();
        childrenCount = this.transform.childCount;
    }

    public void SpawnAliens()
    {
        transform.position = parentPosition;
        Vector2 startPosition = Vector2.zero; ;
        if (m_alienTypes.Count > 0)
        {
            for (int i = 0; i < m_colsAndRows.y; i++)
            {
                float paddingY = m_padding.y * i;
                
                for (int j = 0; j < m_colsAndRows.x; j++)
                {
                    float paddingX = m_padding.x * j;
                    Vector2 currentPosition = new Vector2 (startPosition.x + paddingX, startPosition.y + paddingY);
                    GameObject alienInst = Instantiate(m_alienTypes[Random.Range(0, m_alienTypes.Count-1)]);
                    alienInst.transform.position = currentPosition;
                    alienInst.transform.parent = this.transform;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (newSpeed)
        {
            InvokeRepeating("MoveAliens", movementSpeedSeconds, movementSpeedSeconds);
            newSpeed = false;
        }
        if (this.transform.childCount == 0)
        {
            Debug.Log("voitto");
            SpawnAliens();
        }
        if (childrenCount > this.transform.childCount)
        {
            CancelInvoke();
            movementSpeedSeconds *= accelerationMultiplier;
            Mathf.Clamp(movementSpeedSeconds, 0.05f, movementSpeedSeconds);
            newSpeed = true;
            childrenCount = this.transform.childCount;
        }
    }

    private void MoveAliens()
    {
        Camera cam = Camera.main;
        float aspect = (float)Screen.width / Screen.height;
        float worldHeight = cam.orthographicSize;
        float worldWidth = worldHeight * aspect;

        if (!directionSwitch)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Vector3 child_position = this.transform.GetChild(i).transform.position;
                float borderOffset = worldWidth - this.transform.GetChild(i).transform.localScale.x * 0.5f;
                if (child_position.x + movementAmountX*2 >= borderOffset || child_position.x + movementAmountX*2 <= -borderOffset)
                {
                    movementAmountX = -movementAmountX;
                    Vector3 movementY = new Vector3(0, movementAmountY, 0);
                    transform.position += movementY;
                    directionSwitch = true;
                    return;
                }
            }
        }
        directionSwitch = false;
        Vector3 movementX = new Vector3(movementAmountX, 0, 0);
        transform.position += movementX;
    }
}
