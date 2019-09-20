using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        Vector2 aim = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            move += Vector3.up;
        if (Input.GetKey(KeyCode.A))
            move += Vector3.left;
        if (Input.GetKey(KeyCode.S))
            move += Vector3.down;
        if (Input.GetKey(KeyCode.D))
            move += Vector3.right;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.I))
            aim += Vector2.up;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.K))
            aim += Vector2.down;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.J))
            aim += Vector2.left;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.L))
            aim += Vector2.right;            

        if (move.magnitude > 0)
        {
            move = move.normalized * .1f;
            _transform.position += move;
        }
        
        //get angle against the default sprite direction
        var angle = Vector2.SignedAngle(Vector2.right, aim);
        
        //_transform.eulerAngles = new Vector3(0, 0, angle);
        _transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
