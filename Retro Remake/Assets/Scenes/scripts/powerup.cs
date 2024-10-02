using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class powerup : MonoBehaviour
{
    public int type;
    public GameObject double_points;
    public GameObject one;
    public GameObject two;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        double_points.SetActive(false);
        one.SetActive(false);
        two.SetActive(false);
        type = Random.Range(0, 3);
        if (type == 0)
        {
            double_points.SetActive(true);
        }
        if (type == 1)
        {
            one.SetActive(true);
        }
        if (type == 2)
        {
            two.SetActive(true);
        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(Random.Range(-3.0f, -2.66f), Random.Range(2.66f, 3.0f));
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (type == 0)
            {
                gameManager.StartCoroutine();
            }
            if (type == 1)
            {
                collision.gameObject.GetComponent<player>().StartCoroutine3();
            }
            if (type == 2)
            {
                collision.gameObject.GetComponent<player>().StartCoroutine2();
            }
            Destroy(gameObject);
        }
    }
}
