using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior  {

    Vehicle vehicleAgent;


    public SteeringBehavior(Vehicle vehicle)
    {
        this.vehicleAgent = vehicle;
    }
	


    //================================================
    //Different Steering Behaviors
    //================================================



    //Seek towards a target 
    public Vector2 Seek(Vector2 targetPosition)
    {
        Vector2 desiredVelocity = targetPosition - this.vehicleAgent.GetPosition();
        desiredVelocity = desiredVelocity.normalized;
        desiredVelocity = desiredVelocity * (float)this.vehicleAgent.GetMaxSpeed();

        return desiredVelocity - this.vehicleAgent.GetVelocity();

    }


    //Flee from a target, which is just opposite to Seek behavior
    public Vector2 Flee(Vector2 targetPosition)
    {
        Vector2 desiredVelocity = this.vehicleAgent.GetPosition() - targetPosition;
        desiredVelocity = desiredVelocity.normalized;
        desiredVelocity = desiredVelocity * (float)this.vehicleAgent.GetMaxSpeed();

        return desiredVelocity - this.vehicleAgent.GetVelocity();

    }


    //Arrive to the target gently. The arrival can be tuned with second parameter
    //de-acceleration rates: 3=> slow, 2=> normal, 1=> fast
    //TODO: create enemurated list for de_accelerationRate
    public Vector2 Arrive(Vector2 targetPosition, int de_accelerationRate)
    {
        Vector2 toTarget = targetPosition - this.vehicleAgent.GetPosition();

        // calculate the distance to the target
        float dist = toTarget.magnitude;

        if(dist > 0)
        {
            //because Deceleration is enumerated as an int, this value is required
            //to provide fine tweaking of the deceleration..
            float de_accelerationTweaker = 0.3f;

            //calculate the speed required to reach the target given the desired
            //deceleration
            float speed = dist / (de_accelerationRate * de_accelerationTweaker);

            //we make sure the velocity does not exceed its max speed
            speed = Mathf.Min(speed, (float)this.vehicleAgent.GetMaxSpeed());


            Vector2 desiredVelocity = toTarget * speed / dist;

            return desiredVelocity - this.vehicleAgent.GetVelocity();

        }

        return new Vector2(0,0);
    }



}
