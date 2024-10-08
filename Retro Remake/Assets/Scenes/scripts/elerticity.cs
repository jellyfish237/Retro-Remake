using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elerticity : MonoBehaviour
{
    public float BulletLifetime = 3.3f;
    public Rigidbody2D rb;

    private void Start()
    {
        rb.AddForce(1.8f * transform.up, ForceMode2D.Impulse);
    }
    private void Awake()
    {
        Destroy(gameObject, BulletLifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<player>().Remove();
            Destroy(gameObject);
        }
    }
}
