using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilPrefab;
    [SerializeField] float projectilSpeed = 10f;
    [SerializeField] float projectilLifeTime = 5f;
    [SerializeField] float firingRate = 0.1f;
    public bool IsFiring;

    Coroutine firingCoroutine;
    void Start()
    {

    }
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (IsFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if (!IsFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilPrefab,
             transform.position,
             quaternion.identity);

            Rigidbody2D rigidbody2D = instance.GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                rigidbody2D.velocity = transform.up * projectilSpeed;
            }
            Destroy(instance, projectilLifeTime);
            yield return new WaitForSeconds(firingRate);
        }
    }
}

