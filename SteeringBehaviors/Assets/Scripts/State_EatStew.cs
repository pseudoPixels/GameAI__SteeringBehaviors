using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_EatStew : State<Miner>
{


    //states are made singleton
    private static State_EatStew instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();


    EntityNames entityNames = new EntityNames();


    private State_EatStew()
    {

    }

    public static State_EatStew Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new State_EatStew();
                }

                return instance;
            }

        }
    }







    public override void Enter(Miner miner)
    {
        Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Smells Reaaal goood Elsa!");
    }

    public override void Execute(Miner miner)
    {
        Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Tastes real good too!");

        miner.GetFSM().RevertToPreviousState();
    }

    public override void Exit(Miner miner)
    {
        Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Thankya li'lle lady. Ah better get back to whatever ah wuz doin'");
    }

    public override bool OnMessage(Miner miner, Telegram msg)
    {
        return false;
    }
}
