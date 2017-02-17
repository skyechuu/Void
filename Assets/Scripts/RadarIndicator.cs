using UnityEngine;
using System.Collections;

public class RadarIndicator : MonoBehaviour {

    public Color indicatorColor;
    public Transform target;
    public float indicatorSize;

    private Vector3 direction;
    private Vector3 playerShipPosition;



    void Update()
    {
        if (target)
        {
            playerShipPosition = GameObject.FindGameObjectWithTag("PlayerShip").transform.position;
            direction = (target.position - playerShipPosition);
            if (direction.magnitude < indicatorSize)
            {
                if (transform.GetChild(0).transform.GetComponent<SpriteRenderer>().enabled)
                    transform.GetChild(0).transform.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                if (!transform.GetChild(0).transform.GetComponent<SpriteRenderer>().enabled)
                    transform.GetChild(0).transform.GetComponent<SpriteRenderer>().enabled = true;
                direction = direction.normalized;
                if (direction != Vector3.zero)
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                }
            }
        }
        else
        {
            if (transform.GetChild(0).transform.GetComponent<SpriteRenderer>().enabled)
                transform.GetChild(0).transform.GetComponent<SpriteRenderer>().enabled = false;
        }


        

    }
}
