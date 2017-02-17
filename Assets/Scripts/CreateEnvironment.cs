using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateEnvironment : MonoBehaviour {

    public float bgSize=100;
	public GameObject defaultBackGround;
	public List<GameObject> listOfEnvironmentObjects;
	public int areaX,areaY;
	public int objectAddChance;

	// Use this for initialization
	void Awake	 () {

		int initialX = (int)(areaX / 2) * (-1);
		int initialY = (int)(areaY / 2) * (-1);
		int xLimit = (int)(areaX / 2);
		int yLimit = (int)(areaY / 2);

		for (int i=initialX; i<xLimit; i++) {
            for (int j = initialY; j < yLimit; j++)
            {
                Vector3 p = new Vector3(i * bgSize, j * bgSize, 0);
				Quaternion q =Quaternion.identity;
				GameObject o = (GameObject)Instantiate(defaultBackGround,p,q);
				o.transform.SetParent(this.transform);
			}
		}
	}
	
	void addObjectFromList(int index,Vector3 pos, Quaternion qua){
		Instantiate(listOfEnvironmentObjects[index],pos,qua);
	}

	void addRandomObjectFromList(Vector3 pos, Quaternion qua){
		int r = Random.Range (0, listOfEnvironmentObjects.Count);
		addObjectFromList (r, pos, qua);
	}

	bool canAdd(){
		int r = Random.Range (1, 100);
		if (r > objectAddChance)
			return true;
		else
			return false;
	}
}
