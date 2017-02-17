using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	public string waypointName="none";
	public List<ShipAI> ships;
	public List<WaypointNode> waypoints;
	public bool isPatrol;
	public bool isReturning;
	public float recommendedSpeed=-1; //-1(default) = maxspeed

	[SerializeField]
	private WaypointNode currentNode;

	void Start(){
		if(waypoints.Count>0){
			foreach(WaypointNode wn in waypoints){
				wn.setWaypoint(this);
			}
		}
		nextWaypoint();
		if(ships.Count>0){
			foreach(ShipAI ai in ships){
				ai.setWaypointNode(currentNode);
			}
		}
		
		
	}

	public void nextWaypoint(){

		//first node initalization
		if(waypoints.Count > 0){
			if(!currentNode) {
				currentNode = waypoints[0];
			}
			else{
				if(isPatrol){
					if(!isReturning){ 	//normal to returning
						if(waypoints.IndexOf(currentNode) == waypoints.Count-1){
							currentNode = waypoints[waypoints.IndexOf(currentNode)-1];
							isReturning=true;
						}
						else {	//normal normal
							currentNode = waypoints[waypoints.IndexOf(currentNode)+1];
						}
					}
					else{	//returning to normal
						if(waypoints.IndexOf(currentNode)-1 == -1){
							currentNode = waypoints[waypoints.IndexOf(currentNode)+1];
							isReturning=false;
						}
						else{ 	//normal returning
							currentNode = waypoints[waypoints.IndexOf(currentNode)-1];
						}
					}
				}
				else{ //no patrol
					if(waypoints.IndexOf(currentNode) == waypoints.Count-1){	//last node
						currentNode = waypoints[0];
					}
					else{	//normal
						currentNode = waypoints[waypoints.IndexOf(currentNode)+1];
					}
				}
			}

			changeShipsWaypoint();
		}
	}

	public void assignAI(ShipAI ai){
		ships.Add(ai);
		ai.setWaypointNode(currentNode);
	}

	public void changeShipsWaypoint(){
		foreach(ShipAI ai in ships){
			ai.setWaypointNode(currentNode);
		}
	} 




}
