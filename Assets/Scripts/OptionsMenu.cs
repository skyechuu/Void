using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	void Awake(){
		transform.parent.GetChild(0).FindChild("Profile").GetComponent<Text>().text ="Profile: "+ OptionsController.GetConstantValue(0);
		transform.FindChild("ProfileNameInputField").FindChild("Text").GetComponent<Text>().text = OptionsController.GetConstantValue(0);
	}

	public void OnSaveButtonClick(){
		
		string profileName = transform.FindChild("ProfileNameInputField").FindChild("Text").GetComponent<Text>().text;
		if(!string.IsNullOrEmpty(profileName)){
			OptionsController.ChangeConstant(0,profileName);
			transform.parent.GetChild(0).FindChild("Profile").GetComponent<Text>().text = "Profile: "+ OptionsController.GetConstantValue(0);
		}
		//...
		OptionsController.SaveGameOptions();
	}


	
}
