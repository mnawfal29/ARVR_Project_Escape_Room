using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource winmusic;
    public GameObject[] cups,Chairs,Laptops;
    public int gamestate;
    public camfps camscript;
    public string tex;
    public GameObject Maindoor;
    // Start is called before the first frame update
    void Start()
    {
        cups = GameObject.FindGameObjectsWithTag("Cup");
        Laptops= GameObject.FindGameObjectsWithTag("LaptopRot");
        Chairs= GameObject.FindGameObjectsWithTag("Chair");
        camscript = GameObject.FindObjectOfType<camfps>();
        Maindoor = GameObject.FindGameObjectWithTag("MainDoor");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        textchanger();
        switch (gamestate)
        {
            case 0:
                
               if(cupsCollected())
                {
                    camscript.CanPickUpCups = true;
                    StateComplete();
                }
                else
                {
                    camscript.CanPickUpCups = true;
                }
                break;
            case 1:
                if (AreChairGood())
                {
                    foreach (var item in Chairs)
                    {
                        item.GetComponent<chairmove>().enabled = false;
                    }
                    StateComplete();
                }
                else
                {
                    
                }
                break;
            case 2:
                if(arelaptopsgood())
                {
                    foreach (var item in Laptops)
                    {
                        item.GetComponent<laptoprot>().enabled = false;
                        
                    }
                    StateComplete();
                }
                break;
            case 3:
                Maindoor.GetComponent<MainDoor>().Islocked = false;
                break;
        }
       
       
    }
    public void textchanger()
    {
        switch (camscript.go.tag)
        {
            case "LaptopRot":
                if(gamestate<3)
                tex = "Left/Right Click to rotate the LAPTOP";
                break;
            case "Cup":
                if (camscript.CanPickUpCups)
                    tex= "Press \"E\" Click to Pickup/Drop the CUP";
                break;
            case "Laptop":
                
                tex = "Press \"Q\" Click to Pickup/Drop the LAPTOP";
                break;
            case "Chair":
                if(gamestate<2)
                tex= "Left/Right Click to Move the CHAIR";
                break;
            case "MainDoor":
                if (Input.GetMouseButton(1))
                {
                    if (camscript.go.GetComponent<MainDoor>().Islocked)
                    {
                        tex = "DOOR is LOCKED";
                    }
                    else
                    {
                        tex= "DOOR is OPENED";
                    }
                }
                else
                {
                    tex= "Right Click to OPEN the DOOR";
                }
                break;
            default:
                tex= "";
                break;
        }
    }
    public void Win()
    {
        winmusic.Play();
    }
    public void StateComplete()
    {
        gamestate++;
        gamestate = Mathf.Clamp(gamestate, 0, 4);
    }
    public bool cupsCollected()
    {

        foreach (var item in cups)
        {
            if (!item.GetComponent<cupcontrol>().IsInPlace)
            {
                return false;
            }

        }
        return true;
    }
    public bool AreChairGood()
    {
        foreach (var item in Chairs)
        {
            if (!item.GetComponent<chairmove>().isinplace)
            {
                return false;
            }

        }
        return true;
    }
    public bool arelaptopsgood()
    {
        foreach (var item in Laptops)
        {
            if (!item.GetComponent<laptoprot>().orientgood)
            {
                return false;
            }

        }
        return true;
    }
}