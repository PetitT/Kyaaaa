using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingItem : MonoBehaviour
{
    public Vector2 speedRange;
    private float speed;

    private void Start()
    {
        speed = UnityEngine.Random.Range(speedRange.x, speedRange.y);
    }

    private void Update()
    {
        gameObject.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
