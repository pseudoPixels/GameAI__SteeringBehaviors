using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_WifeVisitBathroom : State<MinersWife>
{


    //states are made singleton
    private static State_WifeVisitBathroom instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();


    EntityNames entityNames = new EntityNames();


    private State_WifeVisitBathroom()
    {

    }

    public static State_WifeVisitBathroom Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new State_WifeVisitBathroom();
                }

                return instance;
            }

        }
    }

    public override void Enter(MinersWife minerswife)
    {
        Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "Walkin' to the can. Need to powda mah pretty li'lle nose");
    }

    public override void Execute(MinersWife minerswife)
    {
        Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "Ahhhhhh! Sweet relief!");

        minerswife.GetFSM().RevertToPreviousState();
    }

    public override void Exit(MinersWife minerswife)
    {
        Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "Leavin' the Jon");
    }

    public override bool OnMessage(MinersWife miner, Telegram msg)
    {
        return false;
    }
}
