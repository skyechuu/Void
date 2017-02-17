using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public struct ShipMatrixCell
{	
	public Vector3 pos;
    public Color col;
}

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
	public ShipMatrixCell cell;
	public ShipMaterialStats stats;
}

[System.Serializable]
public class ShipMaterialHolder : MonoBehaviour {
	
	public ShipMatrixCell cell;
	public ShipMaterialStats stats;
	

	public ShipBodyMaterial getMaterial(){
		return new ShipBodyMaterial { cell=cell, stats=stats};
	}

    
}
