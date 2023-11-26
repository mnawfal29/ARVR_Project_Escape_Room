using UnityEngine;

public class camfps : MonoBehaviour
{
    public float sensx, sensy;
    public Transform player,aimpos;
    public Transform Attachpointright,Attchpointleft;
    public bool rightfill, leftfill;
    private GameObject Robject,Lobject;
    public float x, y;
    bool candropl,candropr;
   public GameObject go;
    public bool CanPickUpCups;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    // Update is called once per frame
    void Update()
    {
        Aiming();
        float mousex = Input.GetAxisRaw("Mouse X") * sensx * Time.deltaTime;
        float mousey = Input.GetAxisRaw("Mouse Y") * sensy * Time.deltaTime;

        y += mousex;
        x -= mousey;
       x= Mathf.Clamp(x,-90f,90f);
       // transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(x,y,0),10*Time.deltaTime);
        transform.rotation =Quaternion.Euler(x,y,0);
        player.rotation = Quaternion.Euler(0,y,0);

        

    }

    public void Aiming()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
       

        if (Physics.Raycast(ray, out RaycastHit hit, 4))
        {
            Debug.Log(hit.collider.name);
            go = hit.collider.gameObject;

            pickup();
            Drop();


            aimpos.position = Vector3.Lerp(aimpos.position, hit.point, 100 * Time.deltaTime);
        }
        else { go = gameObject; aimpos.position = Vector3.Lerp(aimpos.position, Vector3.zero, 1000 * Time.deltaTime); }

    }

    public void pickup()
    {
        if (Input.GetKeyDown("e"))
        {
            if (!rightfill)
            {
                if (go.CompareTag("Cup")&&CanPickUpCups)
                {

                    go.transform.position = Attachpointright.position;
                    go.transform.parent = Attachpointright;
                    Robject = go;
                    rightfill = true;
                    Invoke(nameof(pickupreset), 0.1f);

                }
            }

        }
        else if (Input.GetKeyDown("q"))
        {
            if (!leftfill)
            {
                if (go.CompareTag("Laptop"))
                {
                    Vector3 dir = transform.position - go.transform.position;
                    go.transform.position = Attchpointleft.transform.position;
                    Debug.Log(dir);
                    dir.y = 0;
                    go.transform.rotation = Quaternion.LookRotation(-dir);
                    go.transform.parent = Attchpointleft;
                    go.transform.rotation = Quaternion.Euler(0, go.transform.eulerAngles.y, go.transform.eulerAngles.z);
                    Lobject = go;
                    Lobject.layer = 2;
                    leftfill = true;
                    Invoke(nameof(pickupreset), 0.1f);
                }

            }


        }


    }
    public void Drop()
    {
            if (Input.GetKeyDown(KeyCode.Q) && candropl)
            {
                if (leftfill)
                {
                    Lobject.transform.position = aimpos.transform.position;
                    Lobject.transform.rotation = Quaternion.identity;
                    Lobject.transform.parent = null;
                    Lobject.transform.localScale = Vector3.one;
                    Lobject.layer = 0;
                    leftfill = false;
                    Lobject = null;
                    candropl = false;
                }

            }
            if (Input.GetKeyDown(KeyCode.E) && candropr)
            {
                if (rightfill)
                {
                    Robject.transform.position = aimpos.transform.position;
                    Robject.transform.rotation = Quaternion.identity;
                    Robject.transform.parent = null;
                    Robject.transform.localScale = new Vector3(1, 1, 1);
                    rightfill = false;
                    Robject = null;
                    candropr = false;
                }
            }

        }
    

    public void pickupreset()
    {
        if(rightfill)
        {
            candropr = true;
        }
        else { candropl = true; }
        
       
    }
}
