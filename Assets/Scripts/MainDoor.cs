using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public camfps camscript;
    public bool Islocked=true;
    
    // Start is called before the first frame update
    void Start()
    {
        camscript = GameObject.FindObjectOfType<camfps>();
    }

    // Update is called once per frame
    void Update()
    {
        if(camscript.go==gameObject)
        {
            if(Input.GetMouseButtonDown(1))
            {
                if(Islocked)
                {

                }
                else
                {
                    FindObjectOfType<GameManager>().Win();
                    Destroy(gameObject);
                }
            }
        }
        
    }
}
