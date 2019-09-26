using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respwan(GameObject obj, int seconds)
    {
        StartCoroutine(_respawn(obj, seconds));
    }

    IEnumerator _respawn(GameObject obj, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(true);
        obj.transform.position = new Vector3(0, 1, -1);
        obj.transform.rotation = new Quaternion();
        yield break;
    }
}
