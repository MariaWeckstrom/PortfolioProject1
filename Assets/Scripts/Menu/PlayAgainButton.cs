using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ButtonPress()
    {
        audioSource.Play();
        StartCoroutine(PlayAgain());
    }

    private IEnumerator PlayAgain()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
