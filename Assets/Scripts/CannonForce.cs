using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonForce : MonoBehaviour
{
    LineRenderer lr;
    Rigidbody rb;
    Vector3 startPosition;
    Vector3 startVelocity;
    int i;
    int NumberofPoints = 10;
    float timer = 0.1f;
    float rot;
    Vector2 turn;
    [SerializeField] int r;
    [SerializeField] float power;
    // Start is called before the first frame update
    void Start()
    {
     lr = GetComponent<LineRenderer>();
     rb = GetComponent<Rigidbody>();   
     Physics.autoSimulation = false;
    }

    // Update is called once per frame
    void Update()
    {
        CannonRotate();
        Shoot();
    }

    void Shoot()
    {
        Physics.autoSimulation = true;
        if (Input.GetMouseButtonDown(0))
        {
            drawLine();
            lr.enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            lr.enabled = false;
            rb.velocity = -turn.y *(power * transform.forward) * Time.deltaTime;

        }
    }

     void CannonRotate()
    {
        turn.y -= Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(turn.y * r,0,0);
    }


    void drawLine()
    {
        i=0;
        rot = -turn.y;
        lr.positionCount = NumberofPoints;
        lr.enabled=true;
        startPosition=transform.position;
        startVelocity = rot*(power*transform.forward)/rb.mass;
        lr.SetPosition(i,startPosition);
        
        for(float j=0;i<lr.positionCount-1;j+=timer)
        {
            i++;
            Vector3 linePosition = startPosition+j * startVelocity;
            linePosition.y = startPosition.y + startVelocity.y * j+0.5f * Physics.gravity.y*j*j;
            lr.SetPosition(i,linePosition);
        }
    }
}
