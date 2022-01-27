using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is responsible for player's control.
/// </summary>
public class Player : MonoBehaviour
{
    Biology biology;
    private void Start()
    {
        biology = GetComponent<Biology>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("w"))
        {
            biology.Move(Vector3.forward);
        }
        if (Input.GetKey("s"))
        {
            biology.Move(Vector3.back);
        }
        if (Input.GetKey("a"))
        {
            biology.Move(Vector3.left);
        }
        if (Input.GetKey("d"))
        {
            biology.Move(Vector3.right);
        }
    }
}
