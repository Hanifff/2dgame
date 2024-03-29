using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilPrefab;
    [SerializeField] float projectilSpeed = 10f;
    [SerializeField] float projectilLifeTime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;
    [Header("AI")]
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minFiringRate = 0.1f;
    [SerializeField] bool useAI;


    [HideInInspector] public bool IsFiring;
    Coroutine firingCoroutine;

    void Start()
    {
        if (useAI)
        {
            IsFiring = true;
        }
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

            float timeToNxtProjectil = Random.Range(baseFiringRate - firingRateVariance,
            baseFiringRate + firingRateVariance);
            yield return new WaitForSeconds(Mathf.Clamp(timeToNxtProjectil, minFiringRate, float.MaxValue));
        }
    }
}

