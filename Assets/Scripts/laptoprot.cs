using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laptoprot : MonoBehaviour
{

    public float corrorientation;
    public bool orientgood;
    public camfps camscript;
    // Start is called before the first frame update
    void Start()
    {
        orientgood = false;
        camscript = GameObject.FindObjectOfType<camfps>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(camscript.go==gameObject)
        {
            if(Input.GetMouseButtonDown(1))
            {
                transform.rotation =Quaternion.Euler( new Vector3(0, transform.rotation.eulerAngles.y+90, 0));
            }else if(Input.GetMouseButtonDown(0))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y - 90, 0));
            }
        }
       if(transform.rotation.eulerAngles.y-corrorientation<3f&& transform.rotation.eulerAngles.y - corrorientation > -3f)
        {
            orientgood = true;
        }
        else
        {
            orientgood = false;
        }
    }
}
