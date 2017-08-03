using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine <entityType> {

    entityType stateMachineOwner;

    State<entityType> currentState;
    State<entityType> previousState;
    State<entityType> globalState;


    public StateMachine(entityType owner)
    {
        this.stateMachineOwner = owner;

        this.currentState = null;
        this.previousState = null;
        this.globalState = null;
    }



    //Setters for the States
    public void SetCurrentState(State<entityType> currState)
    {
        this.currentState = currState;
    }

    public void SetPreviousState(State<entityType> prevState)
    {
        this.previousState = prevState;
    }

    public void SetGlobalState(State<entityType> globalState)
    {
        this.globalState = globalState;
    }


    //Getters for the states
    public State<entityType> GetCurrentState()
    {
        return this.currentState;
    }

    public State<entityType> GetPreviousState()
    {
        return this.previousState;
    }

    public State<entityType> GetGlobalState()
    {
        return this.globalState;
    }


    




    //call this to update FSM
    public void Update()
    {
        //execute global state if exists
        if (this.globalState != null) this.globalState.Execute(stateMachineOwner);

        //execute current state if exists
        if (this.currentState != null) this.currentState.Execute(stateMachineOwner);
    }

    //change state. keep track of previous state
    public void ChangeState(State<entityType> newState)
    {
        this.previousState = this.currentState;

        this.previousState.Exit(stateMachineOwner);

        this.currentState = newState;

        this.currentState.Enter(stateMachineOwner);
    }

    //change state back to the previous state
    public void RevertToPreviousState()
    {
        ChangeState(this.previousState);
    }

    //check if the FSM currenty in the passed state or not.
    public bool isInState(State<entityType> st)
    {
        if (this.currentState == st) return true;

        return false;
    }



    //handle the recieved msg
    public bool HandleMessage(Telegram msg)
    {
        
        //first see if the current state has message handling capability 
        if(this.currentState!=null && this.currentState.OnMessage(stateMachineOwner, msg)==true)
        {
            return true;
        }

        //otherwise call the global state
        if(this.globalState!=null && this.globalState.OnMessage(stateMachineOwner, msg)==true)
        {
            return true;
        }


        return false;

    }



}
