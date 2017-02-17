using UnityEngine;
using System.Collections;

public class BulletLife : MonoBehaviour {

    public float lifetime;
	public float damage;
	public GameObject p;
	private CapsuleCollider2D selfCollider;


	
	void Start () {
        Destroy(this.gameObject, lifetime);
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if(col.collider != selfCollider){
			if(col.transform.GetComponent<Ship>()){
				col.transform.GetComponent<Ship>().takeDamage(damage);
				Destroy((GameObject)Instantiate(p, transform.position, Quaternion.identity),1);
				Destroy(this.gameObject);
			}
		}
	}

	public void setSelfCollider(CapsuleCollider2D _selfCollider){
		selfCollider = _selfCollider;
		Physics2D.IgnoreCollision(transform.GetComponent<BoxCollider2D>(),selfCollider);
	}
}
