using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mouse : Enemy
{
    Vector2 _direction;

    bool _beenHit;
    bool _targetPlayer;
    bool _playerInSight;
    Player player;
    NavMeshAgent agent;
    new Collider2D collider;

    public GameObject hitEffect;
    public GameObject deathEffect;
    public Rigidbody2D rb;
    public AudioSource Shot;
    public AudioSource chirp;
    public float Damage = 10f;
    public int RushIncrement = 10;
    public float MaxSpeed = 30f;


    Animator animator;

    enum Animations {
      Moving,
      Hit,
      Stunned,
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

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

        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        animator.SetInteger ("state", (int)Animations.Moving);
        collider.enabled = true;
    }

    void OnEnable()
    {
        ResetStats();
    }

    void Update()
    {
        var heading = player.transform.position - transform.position;
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

        if(!IsDead)
        {
            var angle = Vector2.SignedAngle(Vector2.down, rb.velocity.normalized);
            var rotateVector = new Vector3(0, 0, angle);
            transform.eulerAngles = rotateVector;
        }
    }

    void FixedUpdate()
    {
        if (IsDead)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.AddForce(_direction * System.Math.Min(Speed, RushIncrement));
        }
    }

    public void TakeDamage(int damage)
    {
        if (Shot != null) Shot.Play();
        _beenHit = true;
        StartRush();
        animator.SetInteger("state", (int)Animations.Hit);
        Health -= damage;

        if (Health <= 0)
        {
            collider.enabled = false;
            IsDead = true;
            Speed = 0;
            animator.SetInteger("state", (int)Animations.Stunned);
            StartCoroutine(MouseDying());
            return;
        }

        StartCoroutine(EndHit());
    }

    void StartRush(bool temp = false)
    {
        if (IsDead) return;
        Speed += RushIncrement;
        // cap the max speed
        if (Speed > MaxSpeed) Speed = MaxSpeed;
        if(temp) StartCoroutine(EndRush());

        if (Random.value < .2f)
            chirp.Play();
    }

    IEnumerator EndRush()
    {
        if (_targetPlayer) yield break;
        yield return new WaitForSeconds(5);
        Speed -= RushIncrement;
        if (Speed < 0) Speed = 0;
        yield break;
    }

    IEnumerator EndHit()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetInteger("state", (int)Animations.Moving);
        yield break;
    }

    IEnumerator MouseDying()
    {
      Score score = player.GetComponentInChildren<Score>();
      score.IncreaseScore(1 * StatMultiplier);
      yield return new WaitForSeconds(2);
      Die();
      yield break;
    }

    void Die()
    {
        _targetPlayer = false;
        _beenHit = false;
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsDead)
        {
            if (collision.tag == "Player" && player != null)
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
