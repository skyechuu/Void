using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	private Transform startButton,buildShipButton,optionsButton,exitButton;
	

	void Awake(){
		startButton = transform.GetChild(0);
		buildShipButton = transform.GetChild(1);
		optionsButton = transform.GetChild(2);
		StartCoroutine("getProfileName");
	}
	
	public void OnOptionsButtonClick(){
		Transform t = transform.parent.FindChild("OptionsMenu");
		t.gameObject.SetActive(!t.gameObject.activeInHierarchy);
	}

	public void OnStartButtonClick(){
    	SceneManager.LoadScene("Test Scene");
	}

	public void OnBuildShipButtonClick(){
    	SceneManager.LoadScene("Builder Scene");
	}

	public void OnExitButtonClick(){
    	Application.Quit();
	}


	IEnumerator getProfileName(){
		yield return new WaitForEndOfFrame();
		transform.FindChild("Profile").GetComponent<Text>().text ="Profile: "+ OptionsController.GetConstantValue(0);
	}
}
