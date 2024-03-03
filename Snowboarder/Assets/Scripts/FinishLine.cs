using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float InvokeDelay = 2f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Invoke("ReloadScene", InvokeDelay); // Delays the action
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
