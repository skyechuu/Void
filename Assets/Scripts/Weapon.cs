using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {


    public float bulletSpeed;
    public GameObject bulletPrefab;
    public float rate=1f;

    private float cooldown=0.0f;



    public void Shot(CapsuleCollider2D selfCollider){
        if(Time.time > cooldown) {
            cooldown = Time.time + rate;
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.GetChild(0).position, transform.rotation);
            bullet.GetComponent<BulletLife>().setSelfCollider(selfCollider);
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(bulletSpeed, 0));
        }
    }
}
