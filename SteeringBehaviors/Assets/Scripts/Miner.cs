using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : BaseGameEntity {

    //State<Miner> currentState;

    StateMachine<Miner> stateMachine;

    locationType minerCurrentLocation; 

    int goldCarried; //gold carrying in the pocket currently
    int maxNuggets = 3; //max number of nuggets miner can hold in his pocket.

    int moneyInBank; //miner's wealth
    int comfortLevel = 5; //the amount of gold a miner must have before he feels comfortable

    int thirst; //higher the value, thirstier the miner
    int thirstLevel = 5; //threshold for thirst for the miner

    int fatigue; //higher the value, the more tired the miner
    int tirednessThreshold = 5; //Mining Stamina for the Miner

    


    public Miner(int id) : base(id)
    {
        minerCurrentLocation = locationType.shack;
        goldCarried = 0;
        moneyInBank = 0;
        thirst = 0;
        fatigue = 0;

        stateMachine = new StateMachine<Miner>(this);

        //initialize the FSM with a state
        stateMachine.SetCurrentState(State_GoHomeAndSleepTilRested.Instance);

       
    }

    //FSM for the Miner
    public StateMachine<Miner> GetFSM()
    {
        return stateMachine;
    }





    public void AddToGoldCarried(int val)
    {
        this.goldCarried += val;

        if (this.goldCarried < 0) this.goldCarried = 0; //for safety
    }

    public int GetGoldCarried()
    {
        return this.goldCarried;
    }

    public void SetGoldCarried(int val)
    {
        this.goldCarried = val;
    }

    public bool IsPocketFull()
    {
        if (this.goldCarried >= this.maxNuggets) return true;
        return false;
    }

    public void AddToWealth(int val)
    {
        this.moneyInBank += val;

        if (this.moneyInBank < 0) this.moneyInBank = 0; //for safety
    }

    public int GetWealth()
    {
        return this.moneyInBank;
    }

    public int GetComfortLevel()
    {
        return this.comfortLevel;
    }




    public bool IsThirsty()
    {
        if (this.thirst >= this.thirstLevel) return true;

        return false;
    }


    public void BuyAndDrinkAWhiskey()
    {
        this.thirst = 0;
        this.moneyInBank -= 2;
    }






    public bool IsFatigued()
    {
        if (this.fatigue >= this.tirednessThreshold) return true;

        return false;
    }

    public void IncreaseFatigue()
    {
        this.fatigue += 1;
    }

    public void DecreaseFatigue()
    {
        this.fatigue -= 1;
    }








    public locationType CurrentLocation()
    {
        return minerCurrentLocation;
    }

    public void ChangeLocation(locationType newLoc)
    {
        minerCurrentLocation = newLoc;
    }









    //calling FSM update
    public void Update()
    {

        this.thirst += 1; //increase thirst level for each of the update

        stateMachine.Update();
    }

    public override bool HandleMessage(Telegram msg)
    {
        

        return this.stateMachine.HandleMessage(msg);
    }
}
