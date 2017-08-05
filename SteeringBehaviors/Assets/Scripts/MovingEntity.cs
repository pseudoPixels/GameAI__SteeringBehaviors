using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEntity : BaseGameEntity {

    protected Vector2 velocity;
    protected Vector2 heading;
    protected Vector2 side;

    protected double mass;
    protected double maxSpeed;
    protected double maxForce;
    protected double maxTurnRate;

    public MovingEntity(GameObject go, int enitityType, Vector2 velocity, Vector2 heading, double mass, double maxForce, double maxSpeed, double maxTurnRate) : base(go, enitityType)
    {
        this.velocity = velocity;
        this.heading = heading;
        this.mass = mass;
        this.maxSpeed = maxSpeed;
        this.maxForce = maxForce;
        this.maxTurnRate = maxTurnRate;

        this.side = GetSideVector();
       
    }

    //setters and getters
    public Vector2 GetSideVector()
    {
        //calculating the side vector which is perpendicular to heading direction
        this.side.x = this.heading.y;
        this.side.y = -1*this.heading.x;

        return this.side;
    }

    public Vector2 GetVelocity()
    {
        return this.velocity;
    }
    public void SetVelocity(Vector2 newVelocity)
    {
        this.velocity = newVelocity;
    }


    public Vector2 GetHeading()
    {
        return this.heading;
    }
    public void SetHeading(Vector2 newHeading)
    {
        this.heading = newHeading;
        //this vector is always perpendicular to the heading vector.
        this.side = GetSideVector();
    }


    public void SetMass(double newMass)
    {
        this.mass = newMass;
    }
    public double GetMass()
    {
        return this.mass;
    }


    public void SetMaxForce(double newMaxForce)
    {
        this.maxForce = newMaxForce;
    }
    public double GetMaxForce()
    {
        return this.maxForce;
    }


    public void SetMaxSpeed(double newMaxSpeed)
    {
        this.maxSpeed = newMaxSpeed;
    }
    public double GetMaxSpeed()
    {
        return this.maxSpeed;
    }

    public bool IsSpeedMaxedOut()
    {
        if (this.maxSpeed * this.maxSpeed >= this.velocity.magnitude * this.velocity.magnitude) return true;

        return false;
    }

    public void SetMaxTurnRate(double newMaxTurnRate)
    {
        this.maxTurnRate = newMaxTurnRate;
    }
    public double GetMaxTurnRate()
    {
        return this.maxTurnRate;
    }


    public abstract void Update(double time_elapsed);


}
