using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{

    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> wayPoints;
    int wayPointIdx = 0;

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        wayPoints = waveConfig.GetWaypoints();
        transform.position = wayPoints[wayPointIdx].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (wayPointIdx < wayPoints.Count)
        {
            Vector3 targetPosition = wayPoints[wayPointIdx].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                wayPointIdx++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
