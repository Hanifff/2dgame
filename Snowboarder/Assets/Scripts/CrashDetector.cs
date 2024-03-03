using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float InvokeDelay = 0.5f;
    [SerializeField] ParticleSystem crashEffact;
    [SerializeField] AudioClip crashSFX;
    bool hasCrashed = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") && !hasCrashed)
        {
            hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            crashEffact.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", InvokeDelay); // Delays the action
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
