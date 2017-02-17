using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    public float speedLimit;
    public string planetName;
    public float mass;
    public int population;
    public string economy;
    public float radius;

    private float currentMaxSpeed;

    void Awake(){
        GetComponent<SpriteRenderer>().sortingOrder = -32000;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -31000;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ShipController>())
        {
            currentMaxSpeed = other.GetComponent<ShipController>().maxSpeed;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<ShipController>())
        {
            other.GetComponent<ShipController>().maxSpeed = speedLimit;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<ShipController>())
        {
            other.GetComponent<ShipController>().maxSpeed = 120;
        }
    }
}
