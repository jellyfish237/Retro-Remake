using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vulture_bullet : MonoBehaviour
{
    public float BulletLifetime = 5.0f;
    public Rigidbody2D rb;

    private void Start()
    {
        rb.AddForce(26 * transform.up, ForceMode2D.Impulse);
    }
    private void Awake()
    {
        Destroy(gameObject, BulletLifetime);
    }
}