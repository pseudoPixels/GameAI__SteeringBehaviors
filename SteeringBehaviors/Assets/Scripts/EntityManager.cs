using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager  {


    //states are made singleton
    private static EntityManager instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();


    Dictionary<int, BaseGameEntity> EntityMap = new Dictionary<int, BaseGameEntity>();



    private EntityManager()
    {

    }

    public static EntityManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new EntityManager();
                }

                return instance;
            }

        }
    }



    public void RegisterEntity(BaseGameEntity newEntity)
    {
        EntityMap.Add(newEntity.GetID(), newEntity);
    }


    public BaseGameEntity GetEntityFromID(int id)
    {
        BaseGameEntity entity;

        if(EntityMap.TryGetValue(id, out entity))
        {
            return entity;
        }

        return null;

    }


    public void RemoveEntity(BaseGameEntity removeEntity)
    {
        EntityMap.Remove(removeEntity.GetID());
    }




}
