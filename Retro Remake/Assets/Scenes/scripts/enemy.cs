using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int type;
    public GameObject enemy_ship;
    public GameObject vulture_ship;
    public GameObject player;
    public Rigidbody2D rb;
    public SpriteRenderer sprite_renderer;
    public GameObject laser;
    public GameObject orbiting_attack;
    public bool vulture_attacking = false;
    public Rigidbody2D bulletprefab;
    public Transform myself;
    public GameObject destroyParticles;
    public GameManager gameManager;
    public bool disabled = false;
    void Start()
    {
        disabled = false;
        enemy_ship.SetActive(false);
        vulture_ship.SetActive(false);
        if (type == 0)
        {
            enemy_ship.SetActive(true);
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180f, 180f));
            Vector2 direction = new Vector2(Random.value, Random.value).normalized;
            float spawnSpeed = -2.0f;
            rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (type == 1)
        {
            vulture_ship.SetActive(true);
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180f, 180f));
            Vector2 direction = new Vector2(Random.value, Random.value).normalized;
            float spawnSpeed = Random.Range(Random.Range(-1.0f, -0.5f), Random.Range(0.5f, 1.0f));
            rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
            StartCoroutine(VultureShip_Attack());
        }
    }

    private IEnumerator VultureShip_Attack()
    {
        laser.SetActive(true);
        vulture_attacking = true;
        yield return new WaitForSeconds(3.0f);
        vulture_attacking = false;
        sprite_renderer.color = Color.red;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.white;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.red;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.white;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.red;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.white;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.red;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.white;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.red;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.white;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.red;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.white;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.red;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.white;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.red;
        yield return new WaitForSeconds(0.06f);
        sprite_renderer.color = Color.white;
        laser.SetActive(false);
        vulture_attacking = false;
        Rigidbody2D vulture_bullet = Instantiate(bulletprefab, myself.position, Quaternion.Euler(0, 0, myself.eulerAngles.z));;
        yield return new WaitForSeconds(4.0f);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180f, 180f));
        yield return new WaitForSeconds(1.0f);
        sprite_renderer.color = Color.red;
        StartCoroutine(VultureShip_Attack());
    }
    // Update is called once per frame
    void Update()
    {

        if (type == 1)
        {
            if (vulture_attacking == true)
            {
                transform.up = player.transform.position - enemy_ship.transform.position;
            }
        }
        if (type == 0)
        {
            orbiting_attack.transform.Rotate(0, 0, 100 * Time.deltaTime);
        }
    }
    public void Remove()
    {
        disabled = true;
        Instantiate(destroyParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
