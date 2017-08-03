using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinersWife : BaseGameEntity
{
    StateMachine<MinersWife> stateMachine;

    bool isCooking;

    locationType currentLocation;

    public MinersWife(int id) : base(id)
    {
        stateMachine = new StateMachine<MinersWife>(this);

        isCooking = false;

        currentLocation = locationType.shack;

        //set the initital state...
        stateMachine.SetCurrentState(State_WifeDoHouseWork.Instance);

        //set the global state
        stateMachine.SetGlobalState(State_WifeGlobalState.Instance);
    }

    public bool IsCooking { get; set; }
    public locationType CurrentLocation { get; set; }



    //FSM for the Miner's Wife
    public StateMachine<MinersWife> GetFSM()
    {
        return stateMachine;
    }

    public void Update()
    {
        stateMachine.Update();
    }

    public override bool HandleMessage(Telegram msg)
    {
        return this.stateMachine.HandleMessage(msg);
    }
}
