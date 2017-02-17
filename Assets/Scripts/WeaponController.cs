using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {


	public List<Weapon> weapons;


	private Vector3 direction;
	private ShipController sc;


	void Start () {	
		sc = transform.GetComponent<ShipController>();
	}
	


	void Update () {
		AimTheGuns();
		if(Input.GetMouseButton(0))
			FireTheGuns();
	}



	void AimTheGuns(){
		if(weapons.Count > 0){
			foreach (Weapon w in weapons)
			{
				Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				pz.z = 0;
				direction = (pz - transform.position).normalized;
				if (direction != Vector3.zero)
				{
					float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
					w.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				}
			}
		}
	}

	void FireTheGuns(){
		if(weapons.Count > 0){
			foreach (Weapon w in weapons)
			{
				w.Shot(transform.GetComponent<CapsuleCollider2D>());
			}
		}
	}
}
