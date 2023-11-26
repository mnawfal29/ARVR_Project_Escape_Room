using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chairmove : MonoBehaviour
{
    public camfps camscript;
    public bool isinplace=false;
    public int place;
    // Start is called before the first frame update
    void Start()
    {
        isinplace = false;
        camscript = GameObject.FindObjectOfType<camfps>();
    }

    // Update is called once per frame
    void Update()
    {
        if(camscript.go==gameObject)
        {

            Debug.Log("Press mouse button 1 to moveback");
            if (Input.GetMouseButtonDown(0) && isinplace)
            {
                transform.position = transform.position + transform.forward;
                place++;
            }
            if(Input.GetMouseButtonDown(1) && !isinplace)
            {
                transform.position = transform.position - transform.forward;
                place--;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
      if(other.CompareTag("table"))
        {
            isinplace = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("table"))
        {
            isinplace = false;
        }
    }

}
