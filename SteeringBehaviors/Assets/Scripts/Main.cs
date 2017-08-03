using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    Miner minerBob;
    MinersWife elsa;

    float elapsedTime = 0;
    public float coolDownTime = 1.0f;

   

	// Use this for initialization
	void Start () {
        //create the entities...
        minerBob = new Miner((int)EntityNames.entityNames.ent_Miner_Bob);
        elsa = new MinersWife((int)EntityNames.entityNames.ent_Elsa);

        //Register each of the created entities for keeping track.
        EntityManager.Instance.RegisterEntity(minerBob);
        EntityManager.Instance.RegisterEntity(elsa);
    }
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;

        if(elapsedTime>= coolDownTime)
        {
            //update the entities...
            minerBob.Update();
            elsa.Update();


            //look for and dispatch delayed messages at each update steps...
            MessageDispatcher.Instance.DispatchDelayedMessages();

            elapsedTime = 0;
        }

        
	}
}
