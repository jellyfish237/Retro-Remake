using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullets : MonoBehaviour
{
    public float BulletLifetime = 5.0f;
    public GameObject player;
    public Rigidbody2D rb;

    private void Start()
    {
        rb.AddForce(6 * transform.up, ForceMode2D.Impulse);
    }
    private void Awake()
    {
        Destroy(gameObject, BulletLifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (collision.gameObject.GetComponent<Asteroid>().disabled == false)
            {
                collision.gameObject.GetComponent<Asteroid>().Remove();
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<enemy>().disabled == false)
            {
                collision.gameObject.GetComponent<enemy>().Remove();
                Destroy(gameObject);
            }
        }
    }
}