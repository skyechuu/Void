using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShipAI : ShipController {
	public WaypointNode targetWaypoint;



    new void Awake()
    {
        base.Awake();

    }


    new void Start()
    {
        base.Start();
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (targetWaypoint)
        {
            base.maxSpeed = targetWaypoint.getWaypoint().recommendedSpeed;
            //Rotation
            Vector2 direction = targetWaypoint.getTransform().position - transform.position;
            //Debug.DrawRay(transform.position, direction);
            //Debug.DrawRay(transform.position, transform.up*5f, Color.red);
            if (base.getVel() < 100f)
            {
                if (Vector2.Angle(transform.up, direction) >= 3f)
                {
                    Rotate(rotation * AngleDir(transform.up, direction));
                }
            }
            else
            {
                if (Vector2.Angle(base.getRB().velocity, direction) >= 3f)
                {
                    Rotate(rotation * AngleDir(base.getRB().velocity, direction));
                }
            }


            //Thrusters
            if (direction.magnitude >= 5f)
            {
                ForwardThrust();
            }
            else
            {
                Brake();
            }
        }


        base.setPreviousVelocity(base.getRB().velocity);
    }


    void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, transform.forward*10f);
    }

    public void setWaypointNode(WaypointNode wn){
        targetWaypoint = wn;
    }



}
