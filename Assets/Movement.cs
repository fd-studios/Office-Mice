using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform _transform;
    Rigidbody2D _rigidbody;

    public bool SnapShoot = true;
    public bool SnapMove = true;
    public bool Firing = false;
    public float FireAngle = 0;
    public Vector3 FireVector = Vector3.zero;

    const float MIN_DIR_MAG  = 0.15f;
    const float MIN_MOVE_MAG = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();

        // Start the player off facing to the right
        _transform.eulerAngles = Vector2.right;
    }

    /// <summary>
    /// Have the character face in the direction of this vector, checking
    /// for minimum magnitude
    /// </summary>
    /// <param name="dir"></param>
    void Face(Vector3 dir)
    {
        if (dir.magnitude > MIN_DIR_MAG)
        {
            Firing = true;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                move = move.normalized * 0.1f;
            _rigidbody.MovePosition(_rigidbody.position + new Vector2(move.x, move.y));
        }

        Face(shoot);
    }
}
