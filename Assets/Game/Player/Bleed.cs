using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : MonoBehaviour
{
    SpriteRenderer _blood;
    Vector3 _startScale;

    // Start is called before the first frame update
    void Start()
    {
        _blood = GetComponent<SpriteRenderer>();
        _startScale = _blood.transform.localScale;
    }

    private void OnEnable()
    {
        _blood.transform.localScale = _startScale;
    }

    // Update is called once per frame
    void Update()
    {
        _blood.transform.localScale += new Vector3(.01f, .01f, .01f);
    }
}
