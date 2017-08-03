using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State <entityType>  {

    public abstract void Enter(entityType miner);

    public abstract void Execute(entityType miner);

    public abstract void Exit(entityType miner);

    public abstract bool OnMessage(entityType miner, Telegram msg);


}
