using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEeffect;
    [SerializeField] bool applyCamerShake;
    [SerializeField] bool isPlayer;
    [SerializeField] int score = 50;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damages)
    {
        health -= damages;
        if (health <= 0)
        {
            Die();
        }
    }

    void PlayHitEffect()
    {
        if (hitEeffect != null)
        {
            ParticleSystem instance = Instantiate(hitEeffect,
             transform.position, quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
    void ShakeCamera()
    {
        if (cameraShake != null && applyCamerShake)
        {
            cameraShake.Play();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }
}
