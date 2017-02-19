using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialSprites {
	public int id;
	public Sprite sprite;
}

[System.Serializable]
public class MaterialStats {
	public int id;
	public string name;
    public int defense;
    public int health;
    public int capacity;
	public float weight;
}
public class Database : MonoBehaviour {

	public static Database selfDb;

	public List<Item> items;

	public List<MaterialSprites> materialSprites;
	public List<MaterialStats> materialStats;

	void Start(){
		selfDb = this;
	}


	public Sprite GetMaterial(int materialID){
		List<Sprite> sprites = new List<Sprite>();
		foreach(MaterialSprites ms in materialSprites){
			if(ms.id == materialID){
				sprites.Add(ms.sprite);
			}
		}
		if(sprites.Count > 0){
			return sprites[Random.Range(0,sprites.Count)];
		}
		else{
			return null;
		}
	}


}
