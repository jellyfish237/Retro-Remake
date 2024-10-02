using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public ParticleSystem destroyParticles;
    public GameManager gameManager;
    public int size = 3;
    public GameObject slow_radius;
    public GameObject fast_radius;
    public GameObject reverse_radius;
    public GameObject freeze_radius;
    public GameObject elerticity_icon;
    public Rigidbody2D elerticity;
    public Transform asteroid;
    public bool disabled = false;
    public int asteroidType;
    void Start()
    {
        disabled = false;
        slow_radius.SetActive(false);
        fast_radius.SetActive(false);
        reverse_radius.SetActive(false);
        freeze_radius.SetActive(false);
        elerticity_icon.SetActive(false);
        asteroidType = Random.Range(0, 24);
        if (asteroidType == 19)
        {
            slow_radius.SetActive(true);
        }
        if (asteroidType == 20)
        {
            fast_radius.SetActive(true);
        }
        if (asteroidType == 21)
        {
            reverse_radius.SetActive(true);
        }
        if (asteroidType == 22)
        {
            freeze_radius.SetActive(true);
        }
        if (asteroidType == 23)
        {
            elerticity_icon.SetActive(true);
        }
        transform.localScale = 0.75f * size * Vector3.one;
        transform.rotation = Quaternion.Euler(0,0, Random.Range(-180f, 180f));
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(Random.Range(-3.0f/size, -2.66f/size), Random.Range(2.66f/size, 3.0f/size));
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
        gameManager.asteroidCount++;
    }

    public void Remove()
    {
        disabled = true;
        gameManager.asteroidCount--;
        if (size > 1)
        {
            for (int i = 0; i < 2; i++)
            {
                Asteroid newAsteroid = Instantiate(this, transform.position, transform.rotation);
                newAsteroid.size = size - 1;
                newAsteroid.gameManager = gameManager;
            }
        }
        Instantiate(destroyParticles, transform.position, transform.rotation);
        gameManager.current_score = gameManager.current_score + 30/size;

        if (gameManager.current_score > PlayerPrefs.GetInt("HiScore"))
        {
            PlayerPrefs.SetInt("HiScore", gameManager.current_score);
            if (gameManager.double_point_bool == true)
            {
                gameManager.current_score = gameManager.current_score + 30 / size;
            }
        }
        if (asteroidType == 23)
        {
            Rigidbody2D bullet = Instantiate(elerticity, asteroid.position, Quaternion.Euler(0, 0, asteroid.eulerAngles.z + Random.Range(0, 360)));
            Rigidbody2D bullet2 = Instantiate(elerticity, asteroid.position, Quaternion.Euler(0, 0, asteroid.eulerAngles.z + Random.Range(0, 360)));
            Rigidbody2D bullet3 = Instantiate(elerticity, asteroid.position, Quaternion.Euler(0, 0, asteroid.eulerAngles.z + Random.Range(0, 360)));
            Rigidbody2D bullet4 = Instantiate(elerticity, asteroid.position, Quaternion.Euler(0, 0, asteroid.eulerAngles.z + Random.Range(0, 360)));
            Rigidbody2D bullet5 = Instantiate(elerticity, asteroid.position, Quaternion.Euler(0, 0, asteroid.eulerAngles.z + Random.Range(0, 360)));
        }
        Destroy(gameObject);
    }
}