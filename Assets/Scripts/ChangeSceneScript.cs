using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour {

	public void ReturnToMainMenu(){
		SceneManager.LoadScene(0);
	}

	public void ChangeScene(int s){
		SceneManager.LoadScene(s);
	}




}
