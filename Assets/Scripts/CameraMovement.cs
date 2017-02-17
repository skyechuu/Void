using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    public float smoothness;
    private Vector3 length;
    private float zoom=2;

    private float changedSmoothness;


    void Update()
    {
        
        zoom -= Input.GetAxisRaw("Mouse ScrollWheel")*10;

        if(Input.GetKeyDown(KeyCode.KeypadPlus))
            zoom += 1;
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            zoom -= 1;

        zoom = Mathf.Clamp(zoom,0,5);
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 10 + (zoom * 20), smoothness * Time.deltaTime);
        if(zoom == 0)
            changedSmoothness = 20;
        else 
            changedSmoothness = smoothness;


    }

	// Update is called once per frame
	void FixedUpdate () {
        if(target){
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;

            length = (pz - transform.position)/2;
            //if (length.magnitude < 8)
            //    transform.position = Vector3.Slerp(transform.position, new Vector3(target.position.x, target.position.y, -10f), smoothness * Time.deltaTime);
            transform.position = Vector3.Slerp(transform.position, new Vector3(target.position.x + (length.x / 1.5f), target.position.y + (length.y / 1.5f), -10f), changedSmoothness * Time.deltaTime);
            //transform.position = new Vector3(target.position.x + (length.x/2), target.position.y+(length.y/2), -10f);
        }
	}
}
