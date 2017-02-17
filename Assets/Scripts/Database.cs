using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Database : MonoBehaviour {

	public List<ShipBodyMaterial> bodyMaterials;
	public List<Item> items;

	void Start(){
		//items.Add(new ShipFuelTank("emerald",55,"kursunsuz benzin", 24, 8));
	}

	public ShipBodyMaterial getBodyMaterial(int id){
		foreach(ShipBodyMaterial sbm in bodyMaterials){
			if(sbm.stats.id == id){
				return sbm;
			}
		}
		return new ShipBodyMaterial{};
	}

	public Item getItem(int id){
		foreach(Item i in items){
			if(i.id == id){
				return i;
			}
		}


		return null;
	}


}
