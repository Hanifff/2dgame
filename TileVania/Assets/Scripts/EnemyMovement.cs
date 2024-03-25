using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] float moveSpeed = 1f;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidbody2D.velocity = new Vector2(moveSpeed, 0f);

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlippEnemyFacing();
    }

    void FlippEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)), 1f);
    }
}
