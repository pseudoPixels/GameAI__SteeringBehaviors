using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public GameObject aiAgent;
    public GameObject target;

    Vehicle aiVehicle;


	// Use this for initialization
	void Start () {
        aiVehicle = new Vehicle(aiAgent, 0, new Vector2(0,0), new Vector2(0, 0), 1.0f, 2.0f, 10.0f, 20.0f);
        aiVehicle.SetTarget(target);
	}
	
	// Update is called once per frame
	void Update () {
        aiVehicle.Update(Time.deltaTime);
	}
}
