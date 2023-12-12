using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class AlienManager : MonoBehaviour
{
    [SerializeField] List<GameObject> m_alienTypes = new List<GameObject>();
    [SerializeField] Vector2 m_colsAndRows = Vector2.zero;
    [SerializeField] Vector2 m_padding = Vector2.zero;
    private Vector3 initPosition;
    private bool startMoving = false;

    private float initMovementAmountX = 0.3f;
    private float movementAmountX;
    private float movementAmountY = -0.5f;
    private bool directionSwitch = false;
    private bool newSpeed = true;
    private float movementSpeedSeconds = 0.5f;
    private float accelerationMultiplier = 0.95f;
    private int childrenCount = 0;

    private GameObject player;
    private float alienMaxTravel;
    PlayerHealth playerHealthScript;
    FireCannon playerCannonScript;

    public Sprite[] spriteList;
    int spriteIndex = 0;

    void Start()
    {
        movementAmountX = initMovementAmountX;

        //kill the player if aliens get too close
        player = GameObject.FindWithTag("Player");
        alienMaxTravel = player.transform.position.y+2;
        playerHealthScript = player.GetComponent<PlayerHealth>();

        //disable firing while aliens are still on their way
        playerCannonScript = player.GetComponent<FireCannon>();

        initPosition = transform.position;
        SpawnAliens();
        childrenCount = this.transform.childCount;
    }

    public void SpawnAliens()
    {
        playerCannonScript.enabled = false;
        startMoving = false;
        this.GetComponent<AlienFire>().enabled = false;
        transform.position = initPosition;
        Vector2 startPosition = initPosition;
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
        transform.position = initPosition + new Vector3(0, 15, 0);
    }

    private void FixedUpdate()
    {
        if (startMoving)
        {
            if (newSpeed)
            {
                InvokeRepeating("MoveAliens", movementSpeedSeconds, movementSpeedSeconds);
                newSpeed = false;
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
        else
        {
            CancelInvoke();
            newSpeed = true;
            if (transform.position.y > initPosition.y)
            {
                transform.position += new Vector3(0, -7 * Time.fixedDeltaTime, 0);
            }
            else
            {
                this.GetComponent<AlienFire>().enabled = true;
                playerCannonScript.enabled = true;
                startMoving = true;
            } 
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
            if (spriteIndex == 1)
            {
                spriteIndex = 0;
            }
            else
            {
                spriteIndex = 1;
            }
            for (int i = 0; i < this.transform.childCount; i++)
            {
                //sprite change
                SpriteRenderer spriteRenderer = this.transform.GetChild(i).GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = spriteList[spriteIndex];

                //to kill the player if aliens get too close
                if (this.transform.GetChild(i).transform.position.y <= alienMaxTravel)
                {
                    playerHealthScript.TakeDamage(playerHealthScript.m_maxHealth);
                }
            }
            
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Vector3 child_position = this.transform.GetChild(i).transform.position;
                float borderOffset = worldWidth - this.transform.GetChild(i).GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
                if (child_position.x + movementAmountX >= borderOffset)
                {
                    movementAmountX = -initMovementAmountX;
                    Vector3 movementY = new Vector3(0, movementAmountY, 0);
                    transform.position += movementY;
                    directionSwitch = true;
                    return;
                }
                else if (child_position.x + movementAmountX <= -borderOffset)
                {
                    movementAmountX = initMovementAmountX;
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
