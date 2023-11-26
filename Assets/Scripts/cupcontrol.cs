using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupcontrol : MonoBehaviour
{
    public bool IsInPlace;
    public Material green, red;
    public float abc;
    public bool orientgood;
    public camfps camscript;

    void Start()
    {
        IsInPlace = false;
        orientgood = false;
        camscript = GameObject.FindObjectOfType<camfps>();   
        abc = (Random.Range(0, 2) == 0) ? 0f : 180f;
    }

    void Update()
    {
        if (camscript.go == gameObject)
        {
            if (Input.GetMouseButtonDown(1))
            {
                transform.rotation = Quaternion.Euler(new Vector3(180 + transform.rotation.eulerAngles.z, 0, 0));
                transform.Translate(0, -0.13f, 0);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                transform.rotation = Quaternion.Euler(new Vector3(-180 + transform.rotation.eulerAngles.z, 0, 0));
                transform.Translate(0, -0.13f, 0);

            }
        }

        if (Mathf.Abs(transform.rotation.eulerAngles.z - abc) < 3f)
        {
            orientgood = true;
        }
        else
        {
            orientgood = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("cupholder"))
        {
            IsInPlace = true;
            if (orientgood)
                other.GetComponent<Renderer>().material = green;
            else
            {
                IsInPlace = true;
                other.GetComponent<Renderer>().material = red;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("cupholder"))
        {
            IsInPlace = false;
            other.GetComponent<Renderer>().material = red;
        }
    }
}
