using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class State_GoHomeAndSleepTilRested : State<Miner> {

    //states are made singleton
    private static State_GoHomeAndSleepTilRested instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();


    EntityNames entityNames = new EntityNames();


    private State_GoHomeAndSleepTilRested()
    {

    }

    public static State_GoHomeAndSleepTilRested Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new State_GoHomeAndSleepTilRested();
                }

                return instance;
            }
            
        }
    }

    public override void Enter(Miner miner)
    {
        if(miner.CurrentLocation() != locationType.shack)
        {
            Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Walking Home...");

            miner.ChangeLocation(locationType.shack);


            //telegram for informing miners wife that he arrived home
            Telegram t = new Telegram((int)EntityNames.entityNames.ent_Miner_Bob, (int)EntityNames.entityNames.ent_Elsa, (int)messageTypes.Msg_HiHoneyImHome, 0);

            


            MessageDispatcher.Instance.DispatchMessage(t);

        }
    }

    public override void Execute(Miner miner)
    {
        //if miner is no more fatigued... lets go for dign' some gold again
        if(miner.IsFatigued() == false)
        {
            Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Had a Fantastic Nap... Lets go for digging some more nuggets...");

            //change the state for Digging some Nuggets...
            miner.GetFSM().ChangeState(State_EnterMineAndDigForNugget.Instance);

        }
        else
        {
            miner.DecreaseFatigue();
            Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "ZZZ...");
        }
    }

    public override void Exit(Miner miner)
    {
        //Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Leaving House...");
    }

    public override bool OnMessage(Miner miner, Telegram msg)
    {
        if(msg.GetMessage() == (int)messageTypes.Msg_StewReady)
        {
            Debug.Log(entityNames.GetEntityName(miner.GetID()) + ": " + "Okay Hun, ahm a comin'!");

            miner.GetFSM().ChangeState(State_EatStew.Instance);

            return true;

        }


        return false;
    }
}
