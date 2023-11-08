using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour
{
    [SerializeField] List<GameObject> m_alienTypes = new List<GameObject>();
    [SerializeField] Vector2 m_colsAndRows = Vector2.zero;
    [SerializeField] Vector2 m_padding = Vector2.zero;

    void Start()
    {
        SpawnAliens();
    }

    public void SpawnAliens()
    {
        Vector2 startPosition = Vector2.zero;
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
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
