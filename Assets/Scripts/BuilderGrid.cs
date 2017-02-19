using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BuilderGrid : MonoBehaviour {

	public GameObject pixelPrefab;
	public ShipMaterialHolder selectedBodyMaterial;
	public int gridSize;
	public Vector2 centerOfMass;
	public float strength;	
	public static string userName;
	public static int shipSlotNumber = 0;

	public Transform newShip;
	private BuilderGridNode mainNode;
	[SerializeField]
	private int shipSize;

	public enum BuilderMode { Build=0, Equip=1};
	public BuilderMode builderMode = BuilderMode.Build;


	

	void Start(){

		for(int x=0; x<gridSize; x++){
			for(int y=0; y<gridSize; y++){
				GameObject go = (GameObject)Instantiate(pixelPrefab,new Vector3(x/2.5f,y/2.5f*(-1f),0f),Quaternion.identity);
				go.GetComponent<BuilderGridNode>().material = selectedBodyMaterial.getMaterial();
				go.transform.parent=this.transform;
				if(x == ((gridSize-1)/2) && y == ((gridSize-1)/2))
					mainNode = go.GetComponent<BuilderGridNode>();
			}
		}
		StartCoroutine("ChooseMainGrid");
		userName = OptionsController.GetConstantValue(0);
		GameObject.Find("BuilderCanvas").transform.FindChild("ProfileName").GetComponent<UnityEngine.UI.Text>().text = "Profile: "+userName;
	}

	void Update(){
		
	}

	public void CheckAvaliableStates(){
		int count=0;
		Vector2 totalVec= Vector2.zero;
		foreach(Transform t in transform){
			if(t.GetComponent<BuilderGridNode>().isSelected){
				count++;
				totalVec += (Vector2)t.position;
			}
		}
		if(count>0){
			centerOfMass = totalVec/count;
			foreach(Transform t in transform){
				t.GetComponent<BuilderGridNode>().ChangeAvaliable((strength+(count*0.035f) >= (Vector2.Distance(t.position,centerOfMass))));
			}
			shipSize = count;
			newShip.GetComponent<ShipGenerator>().PreviewShip(transformToMatrix());
		}
	}

	IEnumerator ChooseMainGrid() {
        yield return new WaitForEndOfFrame();
		mainNode.isMain=true;
		mainNode.SelectNode();
    }

	public void saveShip() {
		List<ShipSaveHolder> ship = new List<ShipSaveHolder>();
		foreach(Transform t in transform){
			if(t.GetComponent<BuilderGridNode>().isSelected){
				ShipSaveHolder ssh = new ShipSaveHolder();
				//Debug.Log(t.GetComponent<BuilderGridNode>().material.getMaterial().cell.pos.x);
				ssh.posX = t.GetComponent<BuilderGridNode>().material.cell.x;
				ssh.posY = t.GetComponent<BuilderGridNode>().material.cell.y;
				//ssh.sms = t.GetComponent<BuilderGridNode>().material.stats;
				ship.Add(ssh);
			}
		}
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/"+userName+"_ship"+shipSlotNumber+".dat");
		try
		{
			bf.Serialize(file, ship);
			file.Close();
			Debug.Log("Save is successful: "+Application.persistentDataPath + "/"+userName+"_ship"+shipSlotNumber+".dat");
		}
		catch (System.Exception s)
		{
			Debug.Log("Save is unsuccessful!");
			throw s;
		}
	}

	public static List<ShipBodyMaterial> loadShip(){
		List<ShipBodyMaterial> shipMatrix = new List<ShipBodyMaterial>();
		List<ShipSaveHolder> ship = new List<ShipSaveHolder>();
		if(File.Exists(Application.persistentDataPath + "/"+userName+"_ship"+shipSlotNumber+".dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/"+userName+"_ship"+shipSlotNumber+".dat", FileMode.Open);
			ship = (List<ShipSaveHolder>)bf.Deserialize(file);
			file.Close();
    	}
		else{
			Debug.Log("There is no ship in Ship Slot:"+shipSlotNumber+" for Profile:"+userName);
		}
		if(ship.Count>0){
			foreach(ShipSaveHolder ssh in ship){
				ShipBodyMaterial sbm = new ShipBodyMaterial();
				sbm.cell.x = ssh.posX;
				sbm.cell.y = ssh.posY;
				//sbm.stats = ssh.sms;
				shipMatrix.Add(sbm);
			}
		}
		return shipMatrix;
	}

	public static List<ShipBodyMaterial> loadShip(int shipSlotNumber){
		List<ShipBodyMaterial> shipMatrix = new List<ShipBodyMaterial>();
		List<ShipSaveHolder> ship = new List<ShipSaveHolder>();
		if(File.Exists(Application.persistentDataPath + "/"+OptionsController.GetConstantValue(0)+"_ship"+shipSlotNumber+".dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/"+OptionsController.GetConstantValue(0)+"_ship"+shipSlotNumber+".dat", FileMode.Open);
			ship = (List<ShipSaveHolder>)bf.Deserialize(file);
			file.Close();
			if(ship.Count>0){
				foreach(ShipSaveHolder ssh in ship){
					ShipBodyMaterial sbm = new ShipBodyMaterial();
					sbm.cell.x = ssh.posX;
					sbm.cell.y = ssh.posY;
					//sbm.stats = ssh.sms;
					shipMatrix.Add(sbm);
				}
			}
    	}
		else{
			Debug.Log("There is no ship in Ship Slot:"+shipSlotNumber+" for Profile:"+OptionsController.GetConstantValue(0));
		}
		return shipMatrix;
	}

	public void setSlotNumber(int n){
		shipSlotNumber = n;
		newShip.GetComponent<ShipGenerator>().PreviewShip(loadShip());
	}

	public List<ShipBodyMaterial> transformToMatrix(){
		List<ShipBodyMaterial> shipMatrix = new List<ShipBodyMaterial>();
		foreach(Transform t in transform){
			if(t.GetComponent<BuilderGridNode>().isSelected){
				ShipBodyMaterial sbm = new ShipBodyMaterial();
				shipMatrix.Add(t.GetComponent<BuilderGridNode>().material);
			}
		}
		return shipMatrix;
	}


	public void changeBuildingMode(int i){
		builderMode = (BuilderMode)i;
	}




}//eof

[System.Serializable]
class ShipSaveHolder{
	public float posX;
	public float posY;
	//public ShipMaterialStats sms;
}