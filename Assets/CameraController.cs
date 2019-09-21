using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _target;
    Transform _self;

    // Start is called before the first frame update
    void Start()
    {
        _self = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            var diff = _target.position - _self.position;
            _self.Translate(diff.x, diff.y, 0);
        }
    }
}
