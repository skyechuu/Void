using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour {

	public float waypointRadius=2f;
	private Waypoint waypoint;
	private Transform t;
	
	void Awake(){
		t = this.transform;
		GetComponent<CircleCollider2D>().radius=waypointRadius;
		GetComponent<CircleCollider2D>().isTrigger=true;
	}


	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.GetComponent<ShipAI>()){
			ShipAI ai = other.gameObject.GetComponent<ShipAI>();
			if(waypoint.ships.Contains(ai)){
				if(ai.targetWaypoint == this){
					waypoint.nextWaypoint();
				}
			}
		}
    }

	public void setWaypoint(Waypoint w){
		waypoint = w;
	}

	public Waypoint getWaypoint(){
		return waypoint;
	}

	public Transform getTransform(){
		return t;
	}

	
}
