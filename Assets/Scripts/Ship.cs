using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Ship : MonoBehaviour {


    

    [Header("Ship Info")]
    public string shipName;
    public string shipOwner;
    public string shipID;
    public bool randomShip=true;

    [Header("Ship Stats")]
    public float capacity;
    public float fuel;
    public float health;
    public float shield;
    public bool isDestroyed;
    public bool isShieldDown;
    

    [Header("Debug")]
    [SerializeField]
    private float maxCapacity;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxShield;
    [SerializeField]
    private float maxFuel;     //w = 10 foreach fueltank, c = 8 foreach tank
    [SerializeField]
    private float weight;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float rotationSpeed;
    private bool radarActive = false;
    [SerializeField]
    //private List<ShipBodyMaterial> shipMatrix;
    private ShipInventory shipInventory;
    private bool initialized = false;

    private Transform ship;
    


    void Start()
    {   
        ship = transform.FindChild("Ship");
        if(!randomShip)
            ship.GetComponent<ShipGenerator>().BuildShip(BuilderGrid.loadShip(0));
        else{
            ship.GetComponent<ShipGenerator>().GenerateRandomShip();
        }
        StartCoroutine("InitializeStats");
        InitializeStats();

    }


    void Update()
    {
        updateCurrentWeight();
        updateCurrentCapacity();
        shieldRegeneration();

    }

    public IEnumerator InitializeStats(){
        yield return new WaitForEndOfFrame();
        calculateMaxCapacity();
        calculateMaxHealth();
        updateCurrentWeight();
        calculateMaxShield();
        heal(maxHealth);
        fuel = 1800;
        initialized = true;
    }

    public void updateCurrentWeight() {
        float w = 0;
        foreach(Transform t in ship){
            w += t.GetComponent<ShipMaterialHolder>().stats.weight;
        }
        w += fuel * 0.1f;
        weight = w;
    }

    public void updateCurrentCapacity() {
        
    }

    public void calculateMaxCapacity(){
        float c = 0;
        foreach(Transform t in ship){
            c += t.GetComponent<ShipMaterialHolder>().stats.capacity;
        }
        maxCapacity = c;
    }

    public void calculateMaxHealth(){
        float hp = 0;
        foreach(Transform t in ship){
            hp += t.GetComponent<ShipMaterialHolder>().stats.health;
        }
        maxHealth = hp;
    }

    public void calculateMaxShield(){
        float shield = 0;
        foreach(Transform t in ship){
            shield += t.GetComponent<ShipMaterialHolder>().stats.defense;
        }
        maxShield = shield;
    }

    public void calculateMaxFuel(){
        
    }

    public void heal(float heal){
        if(!isDestroyed){
            health += heal;
            if(health>maxHealth)
                health=maxHealth;
        }
        else
            Debug.Log("Ship is already destroyed!");
    }

    public void takeDamage(float damage){
        if(!isDestroyed){
            if(!isShieldDown){
                if(damage>shield){
                    float remainingDamage = damage-shield;
                    shield = 0;
                    health -= remainingDamage;
                    transform.FindChild("DamageCanvas").GetComponent<DamageCanvas>().ShowDamage((int)remainingDamage);
                    healthStatusControl();
                }
                else{
                    shield -= damage;
                    transform.FindChild("DamageCanvas").GetComponent<DamageCanvas>().ShowDamage((int)damage);
                }
            }
            else{
                health -= damage;
                transform.FindChild("DamageCanvas").GetComponent<DamageCanvas>().ShowDamage((int)damage);
                healthStatusControl();
            }
        }
        else
            Debug.Log("Ship is already destroyed!");

    }

    public void spentFuel(float cost){
        if(!isDestroyed){
            if(fuel > 0){
                fuel -= cost;
                if(fuel < 0)
                    fuel = 0;
            }
        }
    }

    public void shieldRegeneration(){
        if(!isDestroyed){
            if(shield <= 0){
                shield = 0;
                isShieldDown = true;
            }
            if(shield<=maxShield){
                shield += 12f * Time.deltaTime;
                isShieldDown = false;
            }
            if(shield>maxShield)
                shield=maxShield;
            
            
        }
    }

    public void healthStatusControl(){
        if(health<0){
            health=0;
            isDestroyed = true;
            destroyShip();
        }
        int smokeEmission = (float.IsNaN(health/maxHealth))? 5 : 5 - (int)(health/maxHealth*5);
        transform.FindChild("Smoke").GetComponent<ParticleSystem>().emissionRate = (int)Mathf.Pow(smokeEmission,2);
    }

    public void destroyShip(){
        transform.GetComponent<ShipController>().enabled = false;
    }
    public void activateShip(){
        transform.GetComponent<ShipController>().enabled = true;
    }

    void OnMouseOver()
    {
        if (radarActive)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Radar.SetFocus(this.gameObject.transform);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.collider.GetComponent<Ship>()){
                Vector2 colVel = col.collider.GetComponent<ShipController>().getPreviousVelocity();
                Vector2 myVel = GetComponent<ShipController>().getPreviousVelocity();
                float collisionAngle = Vector2.Angle(colVel,myVel);
                float hitDamage=0;
                //Debug.LogError(collisionAngle);
                if(colVel.magnitude+myVel.magnitude >= 60){
                    hitDamage = (colVel.magnitude+myVel.magnitude)*(weight+col.collider.GetComponent<Ship>().weight)*(collisionAngle/180)*0.05f;
                    Debug.Log(collisionAngle);
                    takeDamage(hitDamage);
                }
        }   
    }

    void OnDrawGizmos(){
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + GetComponent<Rigidbody2D>().velocity);
    }


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public float getMaxHealth(){
        return maxHealth;
    }
    
    public float getMaxFuel(){
        return maxFuel;
    }

    public float getMaxShield()
    {
        return maxShield;
    }

    public float getMaxCapacity()
    {
        return maxCapacity;
    }

    public float getWeight(){
        return weight;
    }

    public void setShipMatrix(List<ShipBodyMaterial> _shipMatrix){
        //shipMatrix = _shipMatrix;
    }
    public bool getInitialized(){
        return initialized;
    }


}
