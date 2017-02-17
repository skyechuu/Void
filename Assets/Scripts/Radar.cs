using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour {

    public static float indicatorSize = 15f;
    public static GameObject indicatorPrefab;

    public static Transform focusShip;
    public static List<Transform> indicators;


    private Vector3 playerShipPosition;
    private Vector3 direction;

    void Start()
    {
        indicators = new List<Transform>();
        foreach(Transform t in transform)
        {
            indicators.Add(t);
        }
        
    }

    void Update()
    {
        
    }

    public static void CreateIndicator(Transform target, Color col)
    {
        GameObject i = (GameObject)Instantiate(indicatorPrefab, Vector3.zero, Quaternion.identity);
        i.GetComponent<RadarIndicator>().indicatorColor = col;
        i.GetComponent<RadarIndicator>().target = target;
        i.GetComponent<RadarIndicator>().indicatorSize = indicatorSize;
    }

    public static void SetFocus(Transform t)
    {
        focusShip = t;
        indicators[0].GetComponent<RadarIndicator>().target = t;
    }

}
