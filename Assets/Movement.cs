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
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            move += Vector3.up;
        if (Input.GetKey(KeyCode.A))
            move += Vector3.left;
        if (Input.GetKey(KeyCode.S))
            move += Vector3.down;
        if (Input.GetKey(KeyCode.D))
            move += Vector3.right;

        if (move.magnitude > 0)
        {
            move = move.normalized * .1f;
            _rigidbody.MovePosition(_rigidbody.position + new Vector2(move.x, move.y));
        }

        //rotate player sprite toward mouse pointer
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var vector = mouse - _transform.position;
        //get angle against the default sprite direction
        var angle = Vector2.SignedAngle(Vector3.right, vector);
        _transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
