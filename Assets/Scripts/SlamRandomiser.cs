using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamRandomiser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
