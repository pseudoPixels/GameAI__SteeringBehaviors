using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_QuenchThirst : State<Miner>
{


    //states are made singleton
    private static State_QuenchThirst instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();


    EntityNames entityNames = new EntityNames();


    private State_QuenchThirst()
    {

    }

    public static State_QuenchThirst Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new State_QuenchThirst();
                }

                return instance;
            }

        }
    }






    public override void Enter(Miner miner)
    {
        if(miner.CurrentLocation() != locationType.saloon)
        {
            Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Boy, ah sure is thusty! Walking to the saloon");

            miner.ChangeLocation(locationType.saloon);
        }

        
    }

    public override void Execute(Miner miner)
    {
        if (miner.IsThirsty())
        {
            miner.BuyAndDrinkAWhiskey();

            Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "That's mighty fine sippin liquer");

            miner.GetFSM().ChangeState(State_EnterMineAndDigForNugget.Instance);
        }
        else
        {
            Debug.Log("ERROR...");
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Leaving the saloon, feelin' good");
    }

    public override bool OnMessage(Miner miner, Telegram msg)
    {
        return false;
    }
}
