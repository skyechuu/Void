using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCanvas : MonoBehaviour {

	private Transform damageText;
	void Awake(){
		damageText = transform.GetChild(0);
	}

	void Update(){
		GetComponent<RectTransform>().localRotation = Quaternion.Euler(0,0,transform.parent.localRotation.eulerAngles.z * (-1));
	}

	public void ShowDamage(int damage){
		if(damageText.gameObject.activeInHierarchy){
			int prevDamage = int.Parse(damageText.GetComponent<Text>().text) * (-1);
			damageText.GetComponent<Text>().text = ((prevDamage + damage) * (-1)).ToString();
			damageText.GetComponent<FloatingDamageText>().ResetTimer();
		}
		else{
			damageText.gameObject.SetActive(true);
			damageText.GetComponent<Text>().text = (damage * (-1)).ToString();
		}
	}
}
