using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Here : MonoBehaviour
{
    public bool is_call = false;

    // Update is called once per frame
    void Update()
    {

        if (is_call)
        {
            is_call = false;
            Debug.Log(transform.position);
        }
        
    }
}
