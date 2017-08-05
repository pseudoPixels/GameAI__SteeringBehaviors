using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MovingEntity {

    SteeringBehavior vehicleSteeringBehavior;

    //target gameobject for some behaviors: Seek, Flee etc.
    GameObject target;

	public Vehicle(GameObject go, int enitityType, Vector2 velocity, Vector2 heading, double mass, double maxForce, double maxSpeed, double maxTurnRate) : base(go, enitityType,  velocity,  heading, mass, maxForce, maxSpeed,  maxTurnRate)
    {
        vehicleSteeringBehavior = new SteeringBehavior(this);
    }

    public void SetTarget(GameObject targetGameObj)
    {
        this.target = targetGameObj;
    }

    private Vector2 GetGameObjectPosition(GameObject go)
    {
        return new Vector2(go.transform.position.x, go.transform.position.y);
    }


    public override void Update(double time_elapsed)
    {
        Vector2 steeringForce = this.vehicleSteeringBehavior.Arrive(GetGameObjectPosition(target), 2);

        Vector2 acceleration = steeringForce / (float)this.GetMass();

        Vector2 newVelocity = this.GetVelocity() + acceleration * (float)time_elapsed;

        //truncate the velocity to max speed before updating it.
        newVelocity = Vector2.ClampMagnitude(newVelocity, (float)this.GetMaxSpeed());

        this.SetVelocity(newVelocity);

        Vector2 newPosition = this.GetPosition() + this.GetVelocity() * (float)time_elapsed;

        this.SetPosition(newPosition);



    }
}
