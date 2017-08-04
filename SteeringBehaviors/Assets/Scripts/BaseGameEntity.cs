using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEntity {

    private int m_id;
    private static int m_nextValidID = 0;
    private int entityType;
    private bool tag;

    



    protected double boundingRadius;
    protected GameObject gameObj;




    public BaseGameEntity()
    {
        m_id = this.GetNextValidID();
        entityType = -1;
        tag = false;
      
    }


    public BaseGameEntity(GameObject go, int entity_type)
    {
        m_id = this.GetNextValidID();
        entityType = entity_type;
        tag = false;


        this.gameObj = go;


        boundingRadius = GetBoundingRadius();

    }


    public BaseGameEntity(GameObject go, int entity_type, Vector2 position, double radius)
    {
        m_id = this.GetNextValidID();
        entityType = entity_type;
        tag = false;

        this.gameObj = go;
        boundingRadius = radius;

    }

    public BaseGameEntity(GameObject go, int entity_type, Vector2 position)
    {
        m_id = this.GetNextValidID();
        entityType = entity_type;
        tag = false;

        this.gameObj = go;
        this.gameObj.transform.position = new Vector3(position.x, position.y, this.gameObj.transform.position.z);
        boundingRadius = GetBoundingRadius();

    }

    //this can be used to create an entity with a 'forced' ID. It can be used
    //when a previously created entity has been removed and deleted from the
    //game for some reason. For example, The Raven map editor uses this ctor 
    //in its undo/redo operations. 
    //USE WITH CAUTION!
    public BaseGameEntity(GameObject go, int entity_type, int forcedID)
    {
        m_id = forcedID;
        entityType = entity_type;
        tag = false;


        this.gameObj = go;
        boundingRadius = GetBoundingRadius();

    }




    private int GetNextValidID()
    {
        return m_nextValidID++;
    }





    //setters and getters...
    public int GetID()
    {
        return m_id;
    }



    public float GetBoundingRadius()
    {
        return this.gameObj.GetComponent<Renderer>().bounds.extents.magnitude;
    }

    public void SetPosition(Vector2 newPos)
    {
        this.gameObj.transform.position = newPos;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(this.gameObj.transform.position.x, this.gameObj.transform.position.y);
    }



    //public abstract bool HandleMessage(Telegram msg);

}
