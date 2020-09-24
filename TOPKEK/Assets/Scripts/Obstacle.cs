using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;
    public float pushForce;

    public static event Action onObstacleHit;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterController>().UpdatePushPos(pushForce);
            onObstacleHit?.Invoke();
        }
        Destroy(gameObject);
    }
}
