using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour {
	public ShipAI leader;
	public List<ShipAI> escorts;

	public void removeEscort(ShipAI ai){
		escorts.Remove(ai);
	}

	public void addEscort(ShipAI ai){
		escorts.Add(ai);
	}

	void FixedUpdate(){
		if(leader){
			
		}
	}

    Vector3 GetSeparationVector(Transform target)
    {
        var diff = transform.position - target.transform.position;
        var diffLen = diff.magnitude;
        var scaler = Mathf.Clamp01(1.0f - diffLen / /*neighborDist*/ 4f);
        return diff * (scaler / diffLen);
    }

  
	
}
