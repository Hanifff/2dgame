using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Delivery : MonoBehaviour
{
    bool hasPkg;
    [SerializeField] float deltaDestroy = 0.5f;

    [SerializeField] Color32 hasPkgColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPkgColor = new Color32(1, 1, 1, 1);
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Ouch!");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Package" && !hasPkg)
        {
            Debug.Log("Package is picked up!");
            hasPkg = true;
            spriteRenderer.color = hasPkgColor;
            Destroy(other.gameObject, deltaDestroy);
        }
        if (other.tag == "Customer" && hasPkg)
        {
            Debug.Log("Package is delivered!");
            hasPkg = false;
            spriteRenderer.color = noPkgColor;
        }
    }
}
