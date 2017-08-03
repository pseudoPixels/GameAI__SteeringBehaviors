using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_EnterMineAndDigForNugget : State<Miner>
{


    //states are made singleton
    private static State_EnterMineAndDigForNugget instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();


    EntityNames entityNames = new EntityNames();


    private State_EnterMineAndDigForNugget()
    {

    }

    public static State_EnterMineAndDigForNugget Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new State_EnterMineAndDigForNugget();
                }

                return instance;
            }

        }
    }



    public override void Enter(Miner miner)
    {
        if(miner.CurrentLocation() != locationType.goldmine)
        {
            Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Walking to the goldmine");

            miner.ChangeLocation(locationType.goldmine);
        }
    }

    public override void Execute(Miner miner)
    {
        miner.AddToGoldCarried(1);
        miner.IncreaseFatigue();

        Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Picking up a Nugget...");

        if(miner.IsPocketFull() == true)
        {
            miner.GetFSM().ChangeState(State_VisitBankAndDepositGold.Instance);
        }

        if(miner.IsThirsty() == true)
        {
            miner.GetFSM().ChangeState(State_QuenchThirst.Instance);
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Ah'm leavin' the goldmine with mah pockets full o' sweet gold");
    }

    public override bool OnMessage(Miner miner, Telegram msg)
    {
        return false;
    }
}
