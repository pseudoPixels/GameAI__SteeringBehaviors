using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class State_WifeCookStew : State<MinersWife>
{


    //states are made singleton
    private static State_WifeCookStew instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();


    EntityNames entityNames = new EntityNames();


    private State_WifeCookStew()
    {

    }

    public static State_WifeCookStew Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new State_WifeCookStew();
                }

                return instance;
            }

        }
    }

    public override void Enter(MinersWife minerswife)
    {
        if(minerswife.IsCooking == false)
        {
            Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "Putting the stew in the oven");
            minerswife.IsCooking = true;

            //create the telegram with required information
            Telegram msg = new Telegram((int)EntityNames.entityNames.ent_Elsa, (int)EntityNames.entityNames.ent_Elsa, (int)messageTypes.Msg_StewReady, 3.0f); 

            MessageDispatcher.Instance.DispatchMessage(msg);
        }

        
    }

    public override void Execute(MinersWife minerswife)
    {

        Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "Fussin' over food");

    }

    public override void Exit(MinersWife minerswife)
    {
        Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "Puttin' the stew on the table");
    }

    public override bool OnMessage(MinersWife minerswife, Telegram msg)
    {
        if((int)msg.GetMessage() == (int)messageTypes.Msg_StewReady)
        {
            Debug.Log(entityNames.GetEntityName(minerswife.GetID()) + ": " + "StewReady! Lets eat");

            //prepare telegram for Miner Bob about Stew Being ready
            Telegram t = new Telegram((int)EntityNames.entityNames.ent_Elsa, (int)EntityNames.entityNames.ent_Miner_Bob, (int)messageTypes.Msg_StewReady, 0);

            MessageDispatcher.Instance.DispatchMessage(t);

            //change state to doing other house works.
            minerswife.GetFSM().ChangeState(State_WifeDoHouseWork.Instance);

            return true;

        }


        return false;
    }
}