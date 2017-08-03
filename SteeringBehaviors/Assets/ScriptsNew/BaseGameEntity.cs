using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEntity {

    int m_id;
    int m_nextValidID = 0;


    public BaseGameEntity(int id)
    {
        SetID(id);
    }

    //ensure unique id for each of the Entities
    private void SetID(int id)
    {
        if(id >= m_nextValidID)
        {
            m_id = id;
            m_nextValidID += 1;
        }
        else
        {
            Debug.Log("Invalid Entity ID!");
        }
    }


    public int GetID()
    {
        return m_id;
    }

    public abstract bool HandleMessage(Telegram msg);
    
}
