using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform _transform;
    Rigidbody2D _rigidbody;
    Player _player;
    Vector3 _oldPosition = Vector3.zero;
    float _speed;


    public bool SnapShoot = true;
    public bool SnapMove = true;
    public bool MouseShoot = true;

    public bool Firing = false;
    public float FireAngle = 0;
    public Vector3 FireVector = Vector3.zero;
    public float WalkingSpeed = .1f;

    const float MIN_DIR_MAG  = 0.15f;
    const float MIN_MOVE_MAG = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        _speed = WalkingSpeed;

        // Start the player off facing to the right
        _transform.eulerAngles = Vector2.right;

        MouseShoot = PlayerPrefs.GetInt("aim") == 0;
    }

    /// <summary>
    /// Have the character face in the direction of this vector, checking
    /// for minimum magnitude
    /// </summary>
    /// <param name="dir"></param>
    void Face(Vector3 dir)
    {
        if (_player.Health <= 0) return;

        if (MouseShoot)
        {
            //rotate player sprite toward mouse pointer
            var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var vector = mouse - _transform.position;
            //get angle against the default sprite direction
            var angle = Vector2.SignedAngle(Vector3.right, vector);
            _transform.eulerAngles = new Vector3(0, 0, angle);

            Firing = Input.GetMouseButton(0);
            if (Firing)
                _player.State = Player.PlayerState.ShootingGun;
            else
                _player.State = Player.PlayerState.Walking;
            return;
        }

        

        if (dir.magnitude > MIN_DIR_MAG)
        {
            Firing = true;
            _player.State = Player.PlayerState.ShootingGun;
            var angle = Vector2.SignedAngle(Vector2.right, dir);
            if (SnapShoot)
                angle = Mathf.Round(angle / 45.0f) * 45.0f;

            FireAngle = angle;
            FireVector = new Vector3(0, 0, FireAngle);
            _transform.eulerAngles = FireVector;
        }
        else
        {
            Firing = false;
            if(_transform.position != _oldPosition)
            {
                _player.State = Player.PlayerState.Walking;
            }
            else
            {
                _player.State = Player.PlayerState.Standing;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_player.Health <= 0) return;

        // The Axis names are under Project Settings ... Input
        // When two axis settings have the same name the one with
        // the larger magnitude wins apparently. We have two of each:
        // Horizontal, Vertical, ShootHoriz, ShootVert; one each for
        // keyboard control and joystick
        var horiz = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");
        var move = new Vector3(horiz, vert, 0);

        var shootHoriz = Input.GetAxis("ShootHoriz");
        var shootVert  = Input.GetAxis("ShootVert");
        var shoot      = new Vector3(shootHoriz, shootVert, 0);   

        // Snap movement to 45 deg increments so if using a 
        // joystick you don't get more accuracy in movement than
        // using the keyboard 
        if (move.magnitude > MIN_MOVE_MAG) {
            Face(move);
            if (SnapMove)
            {
                var angle = Vector2.SignedAngle(Vector2.right, move);

                // I'm sure there's a one step way to do this minus the trig, but
                // I didn't spend enough time figuring out Quaterion
                angle = Mathf.Round(angle / 45.0f) * 45.0f * Mathf.Deg2Rad;
                move.x = Mathf.Cos(angle);
                move.y = Mathf.Sin(angle);
            }
            if (move.magnitude > 1)
                move = move.normalized;

            move *= _speed;
            _rigidbody.MovePosition(_rigidbody.position + new Vector2(move.x, move.y));
        }

        Face(shoot);

        _oldPosition = _transform.position;
    }

    public void Run(float timeout, float multiplier)
    {
        var newSpeed = WalkingSpeed * multiplier;
        if (_speed >= newSpeed) return;

        _speed = newSpeed;
        StartCoroutine(Tired(timeout));
    }

    IEnumerator Tired(float timeout)
    {
        yield return new WaitForSeconds(timeout);
        _speed = WalkingSpeed;
        yield break;
    }
}
