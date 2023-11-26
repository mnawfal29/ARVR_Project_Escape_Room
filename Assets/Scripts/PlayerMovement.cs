using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int a;
    [Header("Movementspeeds")]
    public float Speed,Sprintspeed,truespeed,jumpspeed,AirSpeedCon;
    
    public Transform lookdir;
    float xinput, yinput;
    Vector3 movedir;
    Rigidbody rd;

    [Header("Ground")]
    public bool Onground;
    public LayerMask GroundLayer;
    [SerializeField] float GroundDrag, AirDrag;


    public bool ShouldJump;
    public float JumpDelay;
    // Start is called before the first frame update
    void Start()
    {
        rd = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Moveinput();
        Sprintcheck();
        Speedcon();
        Onground = Physics.Raycast(transform.position, Vector3.down, 2 * 0.5f + 0.2f, GroundLayer);

        if (Onground)
            rd.drag = GroundDrag;
        else rd.drag = AirDrag;
        if (Input.GetKey(KeyCode.Space)&&Onground&&ShouldJump)
        {
            ShouldJump = false;
            Jump();
            Invoke(nameof(resetjump), JumpDelay);
        }
    }
    private void FixedUpdate()
    {
        moveplayer();
    }
    public void Moveinput()
    {
        xinput = Input.GetAxis("Horizontal");
        yinput = Input.GetAxis("Vertical");
        if (xinput == 0 && yinput == 0)
            rd.velocity = new Vector3(0, 0, 0);
    }
    public void moveplayer()
    {
        //movedir = lookdir.forward * yinput + lookdir.right * xinput;
         movedir = transform.forward * yinput + transform.right * xinput;


        if(Onground)
        //onground
        rd.AddForce(movedir.normalized*truespeed*10,ForceMode.Force);
        else if(!Onground)
        {//inair
            rd.AddForce(movedir.normalized * truespeed*10*AirSpeedCon, ForceMode.Force);
        }
    }
    public void Sprintcheck()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            truespeed = Sprintspeed;

        }
        else
        {
            truespeed = Speed;
        }

    }
    public void Jump()
    {
        rd.velocity=new Vector3(rd.velocity.x,0,rd.velocity.z);
        rd.AddForce(Vector3.up*jumpspeed,ForceMode.Impulse);

    }
    public void resetjump()
    {
        ShouldJump = true;
    }
    public void Speedcon()
    {
        Vector3 CurSpeed = new Vector3(rd.velocity.x,0,rd.velocity.z);
        if(CurSpeed.magnitude>Speed)
        {
            Vector3 LimitSpeed = CurSpeed.normalized * Speed;
            rd.velocity = new Vector3(LimitSpeed.x,rd.velocity.y,LimitSpeed.z);
        }
    }
}
