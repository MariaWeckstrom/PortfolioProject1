using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEffects : MonoBehaviour
{
    public GameObject explosion;
    public void OnDeath()
    {
        GameObject deadAlienInst = Instantiate(explosion);
        deadAlienInst.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }
}
