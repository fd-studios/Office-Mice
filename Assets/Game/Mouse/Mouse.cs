using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mouse : Enemy
{
    Vector2 _direction;
    float xScale;
    float yScale;
    bool _beenHit;
    bool _targetPlayer;
    bool _playerInSight;
    GameObject playerObj;
    Player player;
    NavMeshAgent agent;

    public GameObject hitEffect;
    public GameObject deathEffect;
    public Rigidbody2D rb;
    public AudioSource Shot;
    public AudioSource chirp;
    public float Damage = 10f;
    public int RushIncrement = 10;
    public float MaxSpeed = 30f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void ResetStats()
    {
        Speed = BaseSpeed * (1 + StatMultiplier / 20f);
        Damage = BaseDamage * (1 + StatMultiplier / 10f);
        Health = BaseHealth * (int)(1 + StatMultiplier / 10f); 
    }

    void OnEnable()
    {
        ResetStats();
    }

    void Update()
    {
        var heading = playerObj.transform.position - transform.position;
        var distance = heading.magnitude;
        _playerInSight = distance < 10;
        agent.speed = System.Math.Min(Speed, RushIncrement);
        if (_beenHit || _playerInSight || _targetPlayer)
        {
            agent.SetDestination(player.transform.position);
            _direction = heading / distance;
        }
        else
        {
            agent.ResetPath();

            _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        var angle = Vector2.SignedAngle(Vector2.down, rb.velocity.normalized);
        var rotateVector = new Vector3(0, 0, angle);
        transform.eulerAngles = rotateVector;
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

        if (Health <= 0)
        {
            Die();
        }
        Destroy(effect, 0.08f);
    }

    void StartRush(bool temp = false)
    {
        Speed += RushIncrement;
        // cap the max speed
        if (Speed > MaxSpeed) Speed = MaxSpeed;
        if(temp) StartCoroutine(EndRush());

        if (Random.value < .2f)
            chirp.Play();
    }

    IEnumerator EndRush()
    {
        if(_targetPlayer) yield break;
        yield return new WaitForSeconds(5);
        Speed -= RushIncrement;
        yield break;
    }

    void Die()
    {
        var deadMouse = Instantiate(deathEffect, transform.position, Quaternion.identity);
        deadMouse.transform.localScale = transform.localScale;
        _targetPlayer = false;
        _beenHit = false;
        gameObject.SetActive(false);
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
        StartRush();
    }
}
