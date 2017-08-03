using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_WifeDoHouseWork : State<MinersWife> {


    //states are made singleton
    private static State_WifeDoHouseWork instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();


    EntityNames entityNames = new EntityNames();


    private State_WifeDoHouseWork()
    {

    }

    public static State_WifeDoHouseWork Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new State_WifeDoHouseWork();
                }

                return instance;
            }

        }
    }

    public override void Enter(MinersWife minerswife)
    {
        Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "Time to do some more housework...");
    }

    public override void Execute(MinersWife minerswife)
    {
        System.Random rnd = new System.Random();

        int houseHoldTask = rnd.Next(0, 3);

        //do some random house hold works...
        switch (houseHoldTask)
        {
            case 0:
                Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "Moppin' the floor");
                break;
            case 1:
                Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "Washin' the dishes");
                break;
            case 2:
                Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "Makin' the bed");
                break;
        }

    }

    public override void Exit(MinersWife miner)
    {
        
    }

    public override bool OnMessage(MinersWife miner, Telegram msg)
    {
        return false;
    }
}
