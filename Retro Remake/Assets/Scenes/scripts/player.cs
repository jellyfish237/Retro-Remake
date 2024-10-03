using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class player : MonoBehaviour
{
    public enum State
    {
        Default,
        Slow,
        Fast,
        Reverse,
        Frozen,
    }

    public enum Mode
    {
        normal,
        mode_1,
        mode_2,
    }

    public float Accel = 5.0f;
    public float MaxSpeed = 7.77f;
    public float RotateSpeed = 100.0f;
    public float BulletSpeed = 60.0f;

    public Transform Player;
    public Rigidbody2D BulletPrefab;
    public ParticleSystem destroyParticles;
    private Rigidbody2D ShipRigidBody2D;
    private bool Alive = true;
    private bool Moving = false;
    private bool MovingBack = false;
    private bool Moving2 = false;
    private bool MovingBack2 = false;
    public bool can_attack = true;
    public float attack_cooldown = 0.4f;
    private State state;
    public Mode mode;

    void Start()
    {
        ShipRigidBody2D = GetComponent<Rigidbody2D>();
        ShipRigidBody2D.velocity = Vector2.ClampMagnitude(ShipRigidBody2D.velocity, MaxSpeed);
    }


    private void Update()
    {
        if (Alive)
        {
            HandleShipAccel();
            HandleShipRotation();
            HandleFiring();
        }

        switch (state)
        {
            case State.Default:
                Accel = 4f;
                MaxSpeed = 7.77f;
                RotateSpeed = 150.0f;
                break;

            case State.Slow:
                Accel = 1.0f;
                MaxSpeed = 3.66f;
                RotateSpeed = 80.0f;
                break;

            case State.Fast:
                Accel = 20.0f;
                MaxSpeed = 33.00f;
                RotateSpeed = 300.0f;
                break;

            case State.Reverse:
                RotateSpeed = -150.0f;
                break;
            case State.Frozen:
                Accel = 0.0f;
                MaxSpeed = 0.0f;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (Alive && Moving || Alive && Moving2)
        {
            ShipRigidBody2D.AddForce(Accel * transform.up);
        }
        if (Alive && MovingBack || Alive && MovingBack2)
        {
            ShipRigidBody2D.AddForce(-Accel * transform.up);
        }

    }

    private void HandleShipAccel()
    {
        Moving = Input.GetKey(KeyCode.UpArrow);
        MovingBack = Input.GetKey(KeyCode.DownArrow);
        Moving2 = Input.GetKey(KeyCode.W);
        MovingBack2 = Input.GetKey(KeyCode.S);
    }

    private void HandleShipRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A)))
        {
            transform.Rotate(RotateSpeed * transform.forward * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D)))
        {
            transform.Rotate(-RotateSpeed * transform.forward * Time.deltaTime);
        }
    }

    private void HandleFiring()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (can_attack == true)
            {
                StartCoroutine(EnterCooldown());
                Rigidbody2D bullet = Instantiate(BulletPrefab, Player.position, Quaternion.Euler(0, 0, Player.eulerAngles.z));
                if (mode == Mode.mode_1)
                {
                    Rigidbody2D bullet2 = Instantiate(BulletPrefab, Player.position, Quaternion.Euler(0, 0, Player.eulerAngles.z - 3.0f));
                    Rigidbody2D bullet3 = Instantiate(BulletPrefab, Player.position, Quaternion.Euler(0, 0, Player.eulerAngles.z + 3.0f));
                }
            }
        }
    }

    private IEnumerator EnterCooldown()
    {
        can_attack = false;
        yield return new WaitForSeconds(attack_cooldown);
        can_attack = true;
    }

    public void StartCoroutine2()
    {
        StartCoroutine(Mode2());
    }
    public IEnumerator Mode2()
    {
        attack_cooldown = 0.1f;
        yield return new WaitForSeconds(15.0f);
        attack_cooldown = 0.4f;
    }

    public void StartCoroutine3()
    {
        StartCoroutine(Mode1());
    }
    public IEnumerator Mode1()
    {
        mode = Mode.mode_1;
        yield return new WaitForSeconds(15.0f);
        mode = Mode.normal;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Normal"))
        {
            state = State.Default;
        }
        if (collision.CompareTag("SlowRadius"))
        {
            state = State.Slow;
        }
        if (collision.CompareTag("FastRadius"))
        {
            state = State.Fast;
        }
        if (collision.CompareTag("ReverseRadius"))
        {
            state = State.Reverse;
        }
        if (collision.CompareTag("FreezeRadius"))
        {
            state = State.Frozen;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowRadius"))
        {
            state = State.Default;
        }
        if (collision.CompareTag("FastRadius"))
        {
            state = State.Default;
        }
        if (collision.CompareTag("ReverseRadius"))
        {
            state = State.Default;
        }
        if (collision.CompareTag("FreezeRadius"))
        {
            state = State.Default;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Remove();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Remove();
        }
        if (collision.gameObject.CompareTag("Enemy_Attack"))
        {
            Remove();
        }
    }

    public void Remove()
        {
            Alive = false;
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.GameOver();
            Instantiate(destroyParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }