using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float moveRate;
    private float start;

    void Start()
    {
        start = transform.position.x;
    }
        void FixedUpdate()
    {
        transform.position = new Vector3(start + cam.position.x * moveRate, transform.position.y, transform.position.z);
    }
}
