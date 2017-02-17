using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusBarScript : MonoBehaviour {


    public float maxValue;
    public float currentValue;

    [Range(0,1)]
    public float ratio;
    
    public Color statusColor;
    public string statusLabel;

    [Range(0, 1)]
    public float backgroundColorDarkness;

    public Transform background, foreground, label;

    public Ship ship;
    public enum eStatusType{ Health, Shield, Fuel, Ammo, Capacity }
    
    public eStatusType statusType;

    void Start(){
        InitializeBar();
    }

    void Update(){
    }

    void OnValidate()
    {
        
    }

    void OnGUI()
    {
        if(ship){
            if(ship.getInitialized()){
                switch(statusType){
                    case eStatusType.Ammo:
                        break;
                    case eStatusType.Health:
                            currentValue = ship.health;
                            maxValue = ship.getMaxHealth();
                        break;
                    case eStatusType.Shield:
                            currentValue = ship.shield;
                            maxValue = ship.getMaxShield();
                        break;
                    case eStatusType.Capacity:
                            currentValue = ship.capacity;
                            maxValue = ship.getMaxCapacity();
                        break;
                    case eStatusType.Fuel:
                            currentValue = ship.fuel;
                            maxValue = /*ship.getMaxFuel()*/2000;
                        break;
                    default:
                        break;
                }
                
                ratio = currentValue / maxValue;
                if(float.IsNaN(ratio))
                    ratio = 0;
                foreground.GetComponent<RectTransform>().localScale = new Vector3(ratio, 1f, 1f);
            }

        }
    }

    void InitializeBar(){
        label.GetComponent<Text>().text = statusLabel;
        foreground.GetComponent<Image>().color = statusColor;
        float r = statusColor.r * backgroundColorDarkness;
        float g = statusColor.g * backgroundColorDarkness;
        float b = statusColor.b * backgroundColorDarkness;
        Color bgColor = new Color(r, g, b);
        background.GetComponent<Image>().color = bgColor;
        transform.name = statusLabel+"StatusBar";
    }

}
