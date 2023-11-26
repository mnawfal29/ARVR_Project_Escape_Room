using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class campos : MonoBehaviour
{
    public Transform camposoff;
    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {
        transform.position = camposoff.position;
    }
}
