using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior  {

    Vehicle vehicleAgent;


    public SteeringBehavior(Vehicle vehicle)
    {
        this.vehicleAgent = vehicle;
    }
	


    //======================================================================
    //Different Steering Behaviors starts
    //======================================================================


    //**********************************************************************
    //                           Seek
    //**********************************************************************
    //Seek towards a target 
    public Vector2 Seek(Vector2 targetPosition)
    {
        Vector2 desiredVelocity = targetPosition - this.vehicleAgent.GetPosition();
        desiredVelocity = desiredVelocity.normalized;
        desiredVelocity = desiredVelocity * (float)this.vehicleAgent.GetMaxSpeed();

        return desiredVelocity - this.vehicleAgent.GetVelocity();

    }






    //**********************************************************************
    //                           Flee
    //**********************************************************************
    //Flee from a target, which is just opposite to Seek behavior
    public Vector2 Flee(Vector2 targetPosition)
    {
        Vector2 desiredVelocity = this.vehicleAgent.GetPosition() - targetPosition;
        desiredVelocity = desiredVelocity.normalized;
        desiredVelocity = desiredVelocity * (float)this.vehicleAgent.GetMaxSpeed();

        return desiredVelocity - this.vehicleAgent.GetVelocity();

    }







    //**********************************************************************
    //                           Arrive
    //**********************************************************************
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









    //**********************************************************************
    //                           Pursuit
    //**********************************************************************
    //this behavior creates a force that steers the agent towards the 
    //evader
    public Vector2 Pursuit(Vehicle evader)
    {
        //if the evader is ahead and facing the agent then we can just seek
        //for the evader's current position.
        Vector2 toEvader = evader.GetPosition() - this.vehicleAgent.GetPosition();


        float relativeHeading = Vector2.Dot(this.vehicleAgent.GetHeading() , evader.GetHeading());

        if (Vector2.Dot(toEvader, this.vehicleAgent.GetHeading()) > 0 && relativeHeading < -0.95)//acos(0.95)=18 degs
        {
            return Seek(evader.GetPosition());
        }


        //Not considered ahead so we predict where the evader will be.

        //the lookahead time is propotional to the distance between the evader
        //and the pursuer; and is inversely proportional to the sum of the
        //agent's velocities
        float lookAheadTime = (float)(toEvader.magnitude /
                              (this.vehicleAgent.GetMaxSpeed() + evader.GetSpeed()));

        //now seek to the predicted future position of the evader
        return Seek(evader.GetPosition() + evader.GetVelocity() * lookAheadTime);
     }


    //**********************************************************************
    //                           Evade
    //**********************************************************************
    //similar to pursuit except the agent Flees from the estimated future
    //  position of the pursuer
    public Vector2 Evade(Vehicle pursuer)
    {
        /* Not necessary to include the check for facing direction this time */
        Vector2 toPursuer = pursuer.GetPosition() - this.vehicleAgent.GetPosition();

        //uncomment the following two lines to have Evade only consider pursuers 
        //within a 'threat range'
        float threatRange = 100.0f;
        if (toPursuer.magnitude > threatRange) return new Vector2(0,0);

        //the lookahead time is propotional to the distance between the pursuer
        //and the pursuer; and is inversely proportional to the sum of the
        //agents' velocities
        float lookAheadTime = (float)(toPursuer.magnitude / (this.vehicleAgent.GetMaxSpeed() + pursuer.GetSpeed()));
        
        //now flee away from predicted future position of the pursuer
        return Flee(pursuer.GetPosition() + pursuer.GetVelocity() * lookAheadTime);

    }

    

}
