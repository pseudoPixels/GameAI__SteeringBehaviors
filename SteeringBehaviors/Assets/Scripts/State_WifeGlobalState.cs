using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_WifeGlobalState : State<MinersWife>
{


    //states are made singleton
    private static State_WifeGlobalState instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();


    EntityNames entityNames = new EntityNames();


    private State_WifeGlobalState()
    {

    }

    public static State_WifeGlobalState Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new State_WifeGlobalState();
                }

                return instance;
            }

        }
    }

    public override void Enter(MinersWife miner)
    {
        
    }

    public override void Execute(MinersWife minerswife)
    {
        System.Random rnd = new System.Random();
        if (rnd.Next(0, 100) <= 10 && minerswife.GetFSM().isInState(State_WifeVisitBathroom.Instance) == false)
        {
            minerswife.GetFSM().ChangeState(State_WifeVisitBathroom.Instance);
        }
    }

    public override void Exit(MinersWife miner)
    {
        
    }

    public override bool OnMessage(MinersWife minerswife, Telegram msg)
    {
       // Debug.Log("$$$$$$$$$$$$$$$$ WIFes GLOBAL STATE....");

        //Miner Bob in Home.
        if(msg.GetMessage() == (int)messageTypes.Msg_HiHoneyImHome)
        {
            Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "Hi honey. Let me make you some of mah fine country stew");

            minerswife.GetFSM().ChangeState(State_WifeCookStew.Instance);

            return true;
        }

        return false;
    }
}
