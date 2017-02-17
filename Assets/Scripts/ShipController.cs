using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour {
    public float maxSpeed;
    public float acceleration;
    public float thrusters;
    public float rotation;


    public Transform lockedTarget;

    private Rigidbody2D r;
    private float velocity;
    private Ship ship;
    private float calculatedRotation;
    
    private Vector2 previousVelocity;

    public void Awake()
    {
        if (!ship)
        {
            ship = transform.GetComponent<Ship>();
        }
        
    }


	public void Start () {
        r = GetComponent<Rigidbody2D>();
	}
	
	public void FixedUpdate () {
        Values();
	}


    // UPDATE END
    public void Values()
    {
        velocity = r.velocity.magnitude;
        r.drag = 0.1f;
        if(acceleration>30) //30 is default min. acc.
                    acceleration -= 10 * Time.deltaTime;
    }

    public void ForwardThrust(){
        if(ship.fuel > 0){
            r.drag = 1;
            if (velocity < maxSpeed){
                r.AddRelativeForce(new Vector2(0, acceleration * Time.fixedDeltaTime * thrusters));
                ship.spentFuel(0.2f * Time.fixedDeltaTime * acceleration);
            }

            if (velocity >= acceleration - 5f) //10f is sensitivity 
            {
                acceleration += 12 * Time.deltaTime;
            }
        }
        else {
            Debug.Log("Not enough fuel to work thrusters.");
        }
    }

    public void Brake()
    {
        r.drag = 2;
        if(velocity < 3f)
            r.drag = 10;
    }

    public void Rotate(float rotation)
    {
        r.transform.Rotate(new Vector3(0.0f, 0.0f, -rotation * Time.fixedDeltaTime));
        //if (rotation != 0.0f)
            //r.drag = 1.3f;
    }

    

    public float getVel(){
        return velocity;
    }
    public Rigidbody2D getRB(){
        return r;
    }

    public float AngleDir(Vector2 A, Vector2 B)
    {
        float angle = -A.x * B.y + A.y * B.x;
        return (angle>=0f)?1:-1;
    }


	public Vector2 getPreviousVelocity(){
        return previousVelocity;
    }

    public void setPreviousVelocity(Vector2 vel){
        previousVelocity = vel;
    }

}
