using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPlayer : ShipController {

	
    public KeyCode throttleKey,brakeKey;
    private float inputY;
    private GameObject infoPanel;
    [SerializeField]
    private bool autoPilot=false;

    new void Awake()
    {
        base.Awake();

    }
    new void Start()
    {
        base.Start();
        infoPanel = GameObject.FindWithTag("InfoPanel");
        infoPanel.SetActive(false);
    }

    void Update(){
        inputY = Input.GetAxis("Horizontal") * rotation;
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(throttleKey))
        {
            autoPilot=!autoPilot;
        }
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        
        if(autoPilot)
            ForwardThrust();

        //throttle
        if (Input.GetKey(throttleKey) && !Input.GetKey(KeyCode.LeftShift))
        {
            if(autoPilot)
                autoPilot=false;
            else
            {
                if (!Input.GetKey(brakeKey))
                    ForwardThrust();
            }
        }

        //brake
        if (Input.GetKey(brakeKey))
        {
            Brake();
        }
        //rotate
        Rotate(inputY);

        base.setPreviousVelocity(base.getRB().velocity);
    }

    void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, transform.forward*10f);
    }
    

	public void OnGUI()
    {
        if(infoPanel.activeInHierarchy){
            infoPanel.transform.FindChild("Acceleration").GetComponent<Text>().text      = "ACC  : " + acceleration;
            infoPanel.transform.FindChild("Velocity").GetComponent<Text>().text          = "VEL  : " + base.getVel();
            infoPanel.transform.FindChild("Drag").GetComponent<Text>().text              = "DRAG : " + base.getRB().drag;
            infoPanel.transform.FindChild("System Max Speed").GetComponent<Text>().text  = "MSPD : " + maxSpeed;
            if(lockedTarget)
                infoPanel.transform.FindChild("Target Distance").GetComponent<Text>().text   = "TDST : " + Vector3.Distance(transform.position, lockedTarget.position);
        
        }
		
    }



}
