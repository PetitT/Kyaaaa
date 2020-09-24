using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public List<Transform> pathsPoints;
    public int currentPoint = 1;

    public float speed;
    public float pushSpeed;

    private Vector2 targetPos;
    private Vector2 pushPos;

    public static event Action onDeath;

    private void Start()
    {
        pushPos = transform.position;
    }

    private void Update()
    {
        GetInputs();
        Move();
    }

    private void Move()
    {
        Vector2 translation = Vector2.zero;
        if (transform.position.y > targetPos.y + 0.1f)
        {
            translation = Vector2.down;
        }
        else if (transform.position.y < targetPos.y - 0.1f)
        {
            translation = Vector2.up;
        }

        transform.Translate(translation * speed * Time.deltaTime);

        if (transform.position.x > pushPos.x)
        {
            transform.Translate(Vector2.left * pushSpeed * Time.deltaTime);
        }
    }

    private void GetInputs()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentPoint++;
            currentPoint = Mathf.Clamp(currentPoint, 0, pathsPoints.Count - 1);
            UpdatePlayerPos();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentPoint--;
            currentPoint = Mathf.Clamp(currentPoint, 0, pathsPoints.Count - 1);
            UpdatePlayerPos();
        }
    }

    private void UpdatePlayerPos()
    {
        targetPos = new Vector2(transform.position.x, pathsPoints[currentPoint].transform.position.y);
    }

    public void UpdatePushPos(float pushForce)
    {
        pushPos = new Vector2(pushPos.x - pushForce, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tentacle"))
        {
            gameObject.SetActive(false);
            onDeath?.Invoke();
        }
    }
}
