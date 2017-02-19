using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderGridNode : MonoBehaviour {

	public bool isSelected=false;
	public bool isAvaliable=false;
	public bool isMain = false;
	[SerializeField]
	public ShipBodyMaterial material;

	void Start(){
		material.cell = new Vector3(transform.position.x, transform.position.y, 0);
		GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.2f);
		if(isMain)
			SelectNode();
		//Debug.Log(material.cell.pos);
	}


	void OnMouseEnter(){
		if(!isSelected){
			if(isAvaliable)
				GetComponent<SpriteRenderer>().color = Color.green;
			else	
				GetComponent<SpriteRenderer>().color = Color.red;
		}
		else{
			GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.7f);
		}
	}

	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
			if(isSelected)
				DeselectNode();
			else{
				if(isAvaliable){
					SelectNode();
				}
			}
		}
	}

	void OnMouseExit(){
		if(!isSelected){
			if(isAvaliable)
				GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.2f);
			else
				GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.1f);
		}
		else{
			GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
		}
	}

	


	public void SelectNode(){
		isSelected = true;
		GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
		transform.parent.GetComponent<BuilderGrid>().CheckAvaliableStates();
		transform.localScale = Vector2.one * 0.95f;
	}

	public void DeselectNode(){
		if(!isMain){
			isSelected = false;
			GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.2f);
			transform.parent.GetComponent<BuilderGrid>().CheckAvaliableStates();
			transform.localScale = Vector2.one * 0.8f;
		}
	}

	public void ChangeAvaliable(bool a){
		if(!isSelected){
			if(a){
				isAvaliable = a;
				GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.2f);
			}
			else{
				isAvaliable = a;
				GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.1f);
			}
		}
		else{
			if(!a){
				DeselectNode();
				isAvaliable=a;
			}
		}
	}

}
