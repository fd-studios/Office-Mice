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
    bool _stunned;
    Player player;
    NavMeshAgent agent;
    new Collider2D collider;
    float directionChangeDelay = 0f;
    Animator animator;

    public Rigidbody2D rb;
    public AudioSource Shot;
    public AudioSource chirp;


    enum Animations
    {
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

    protected override void ResetStats()
    {
        base.ResetStats();
        _stunned = false;
        _targetPlayer = false;
        _beenHit = false;

        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        animator.SetInteger("state", (int)Animations.Moving);
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
        _playerInSight = distance < PlayerDetectionDistance;
        if (_beenHit || _playerInSight || _targetPlayer)
        {
            agent.SetDestination(player.transform.position);
            _direction = heading / distance;
        }
        else
        {
            agent.ResetPath();
            GetHeading();
        }

        if (!IsDead)
        {
            var angle = Vector2.SignedAngle(Vector2.down, _direction);
            var rotateVector = new Vector3(0, 0, angle);
            transform.eulerAngles = rotateVector;
        }
    }

    void GetHeading()
    {
        if (directionChangeDelay > 0f)
        {
            directionChangeDelay -= Time.deltaTime;
            return;
        }
        else
        {
            directionChangeDelay = Random.Range(0, 5);
            _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }

    void FixedUpdate()
    {
        if (_stunned || IsDead)
            return;

        if (_beenHit || _playerInSight || _targetPlayer)
        {
            rb.velocity = Vector2.zero;
            agent.speed = System.Math.Min(Speed, RushIncrement);
        }
        else
        {
            rb.velocity = _direction * System.Math.Min(Speed, RushIncrement);
            agent.speed = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        if (Shot != null) Shot.Play();
        Health -= damage;
        rb.velocity = Vector2.zero;
        agent.speed = 0;

        if (Health <= 0)
        {
            collider.enabled = false;
            IsDead = true;
            animator.SetInteger("state", (int)Animations.Stunned);
            StartCoroutine(MouseDying());
        }
        else
        {
            _beenHit = true;
            StartRush();
            _stunned = true;
            animator.SetInteger("state", (int)Animations.Hit);
            StartCoroutine(EndHit());
        }
    }

    void StartRush(bool temp = false)
    {
        if (IsDead) return;
        Speed += RushIncrement;
        if (temp) StartCoroutine(EndRush());

        if (Random.value < .2f)
            chirp.Play();
    }

    IEnumerator EndRush()
    {
        if (_targetPlayer) yield break;
        yield return new WaitForSeconds(5);
        Speed -= RushIncrement;
        yield break;
    }

    IEnumerator EndHit()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetInteger("state", (int)Animations.Moving);
        _stunned = false;
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
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (!IsDead)
        {
            if (otherCollider.tag == "Player" && player != null)
            {
                player.TakeDamage(Damage);
            }
            else if (otherCollider.tag == "Wall")
            {
                directionChangeDelay = 0f;
                GetHeading();
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
