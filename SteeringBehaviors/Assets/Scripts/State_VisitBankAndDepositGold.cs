using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_VisitBankAndDepositGold : State<Miner>
{


    //states are made singleton
    private static State_VisitBankAndDepositGold instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();


    EntityNames entityNames = new EntityNames();

    private State_VisitBankAndDepositGold()
    {

    }

    public static State_VisitBankAndDepositGold Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new State_VisitBankAndDepositGold();
                }

                return instance;
            }

        }
    }





    public override void Enter(Miner miner)
    {
        if(miner.CurrentLocation() != locationType.bank)
        {
            Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Going to the Bank.");
            miner.ChangeLocation(locationType.bank);
        }


    }

    public override void Execute(Miner miner)
    {
        miner.AddToWealth(miner.GetGoldCarried());
        miner.SetGoldCarried(0);


        Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Depositing gold. Total savings now: " + miner.GetWealth());


        if(miner.GetWealth() >= miner.GetComfortLevel())
        {
            Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "WooHoo! Rich enough for now. Back home to mah li'lle lady");

            miner.GetFSM().ChangeState(State_GoHomeAndSleepTilRested.Instance);
        }
        else
        {
            Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Need to Dig some more Nuggets...");
            miner.GetFSM().ChangeState(State_EnterMineAndDigForNugget.Instance);
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Leavin' the bank");
    }

    public override bool OnMessage(Miner miner, Telegram msg)
    {
        return false;
    }
}
