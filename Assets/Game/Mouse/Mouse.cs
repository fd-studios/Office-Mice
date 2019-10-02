using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : Enemy
{
    Vector2 _direction;
    float _someScale;
    bool _beenHit;
    bool _targetPlayer;
    GameObject playerObj;
    Player player;

    public GameObject hitEffect;
    public GameObject deathEffect;
    public Rigidbody2D rb;
    public AudioSource Shot;
    public int Damage = 10;
    public int RushIncrement = 10;
    public float MaxSpeed = 30f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        _someScale = transform.localScale.x;
    }

    void OnEnable()
    {
        Speed = BaseSpeed * StatMultiplier;
        Health = BaseHealth * (int)StatMultiplier;
        Debug.Log($"Health:{Health} Speed:{Speed}");
    }

    void Update()
    {
        var heading = playerObj.transform.position - transform.position;
        var distance = heading.magnitude;
        if (_beenHit || distance < 10 || _targetPlayer)
        {
            _direction = heading / distance;
        }
        else
        {
            _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        var angle = Vector2.SignedAngle(Vector2.down, rb.velocity.normalized);
        var rotateVector = new Vector3(0, 0, angle);
        transform.eulerAngles = rotateVector;

        if (rb.velocity.x >= 0)
        {
            transform.localScale = new Vector2(-_someScale, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(_someScale, transform.localScale.y);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(_direction * System.Math.Min(Speed, RushIncrement));
    }

    public void TakeDamage(int damage)
    {
        if (Shot != null) Shot.Play();
        _beenHit = true;
        StartRush();
        var position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        var effect = Instantiate(hitEffect, position, transform.rotation);
        effect.transform.localScale = transform.localScale;
        Health -= damage;
        Debug.Log($"Mouse Hit Health:{Health}");

        if (Health <= 0)
        {
            Die();
        }
        Destroy(effect, 0.08f);
    }

    void StartRush()
    {
        Speed += RushIncrement;
        // cap the max speed
        if (Speed > MaxSpeed) Speed = MaxSpeed;
        StartCoroutine(EndRush());
    }

    IEnumerator EndRush()
    {
        yield return new WaitForSeconds(5);
        Speed -= RushIncrement;
        yield break;
    }

    void Die()
    {
        Debug.Log("Mouse dead");
        var deadMouse = Instantiate(deathEffect, transform.position, Quaternion.identity);
        deadMouse.transform.localScale = transform.localScale;
        Destroy(gameObject);
        Score score = player.GetComponentInChildren<Score>();
        score.IncreaseScore(1 * StatMultiplier);
        Destroy(deadMouse, 2f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player != null)
            {
                player.TakeDamage(Damage);
            }
        }
    }

    public override void Kill()
    {
        base.Kill();
        _targetPlayer = true;
        Speed = MaxSpeed;
    }
}
