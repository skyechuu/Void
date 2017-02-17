using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

	public Transform targetPanel;
	public Sprite activeSprite, deactiveSprite;

	public void togglePanel(){
		if(targetPanel.gameObject.active){
			targetPanel.gameObject.SetActive(false);
			GetComponent<Image>().sprite = deactiveSprite;
		}
		else{
			targetPanel.gameObject.SetActive(true);
			GetComponent<Image>().sprite = activeSprite;
		}
	}
}
