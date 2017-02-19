using UnityEngine;
using System.Collections;
using System;

[System.Serializable]

public class ShipMaterialStats 
{
	public int id;
	public string name;
    public int defense;
    public int health;
    public int capacity;
	public float weight;
}

[System.Serializable]
public class ShipBodyMaterial {
	public Vector3 cell;
	public ShipMaterialStats stats;
}

[System.Serializable]
public class ShipMaterialHolder : MonoBehaviour {
	
	public int materialID;
	public Vector3 cell;
	public ShipMaterialStats stats;
	

	public ShipBodyMaterial getMaterial(){
		return new ShipBodyMaterial { cell=cell, stats=stats};
	}

    
}
