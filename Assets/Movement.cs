using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform _transform;
    Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();

        // Start the player off facing to the right
        _transform.eulerAngles = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal axis is under Project Settings... Input... Axis
        // The scale and gravity are set to 1000 so there's no (little)
        // lag otherwise the GetAxis simulates a joystick and smooths out
        // the transition. Apparently the "snap" setting being cleared means
        // the position doesn't snap to zero before moving to a new angle if
        // when switching to say left straight to right and not letting up
        // in between.
        var horiz = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");
        var move = new Vector3(horiz, vert, 0);

        var shootHoriz = Input.GetAxis("ShootHoriz");
        var shootVert  = Input.GetAxis("ShootVert");
        var shoot      = new Vector3(shootHoriz, shootVert, 0);   

        // TODO: snap movement to 45 deg increments so if using a 
        // joystick you don't get more accuracy in movement than
        // using the keyboard 
        if (move.magnitude > 0.15) {
            move = move.normalized * .1f;
            _rigidbody.MovePosition(_rigidbody.position + new Vector2(move.x, move.y));
        }
        
        // Only change direction she's facing on input, prevents
        // her snapping back to pointing right when you release
        // the shoot keys. Also, snap to 45 degree shoot angle.
        if (shoot.magnitude > 0.15) {
            var angle = Vector2.SignedAngle(Vector2.right, shoot);
            angle = Mathf.Round(angle/45.0f) * 45.0f;
            _transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
