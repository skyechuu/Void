using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamageText : MonoBehaviour {

	public float second;
	public float cooldown=0.0f;

	void OnEnable(){
		cooldown = Time.time+second;
	}

	void Update(){
		if(Time.time > cooldown) {
            cooldown = Time.time + second;
			gameObject.SetActive(false);
        }
	}

	void DisableText(){
		if(Time.time > cooldown) {
            cooldown = Time.time + second;
			gameObject.SetActive(false);
        }
	}

	public void ResetTimer(){
		cooldown = Time.time + second;
	}
}
