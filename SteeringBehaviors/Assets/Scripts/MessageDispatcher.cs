using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class MessageDispatcher {



    //states are made singleton
    private static MessageDispatcher instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();


    HashSet<Telegram> delayedMessages = new HashSet<Telegram>();


    private MessageDispatcher()
    {

    }

    public static MessageDispatcher Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MessageDispatcher();
                }

                return instance;
            }

        }
    }


    //Discharge the message to receiver.
    //Log if message sending fails.
    private void Discharge(BaseGameEntity msgReceiver, Telegram msg)
    {
        

        if (msgReceiver.HandleMessage(msg) == false)
        {
            Debug.Log("Message could not be discharged!");
        }

    }

    //check if the discharge time of the message is passed or not.
    private bool IsMessageOldEnoughToSend(Telegram msg)
    {
        System.TimeSpan span = System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc));
        double seconds = span.TotalSeconds;

        if (msg.GetDispatchTime() <= seconds) return true;

        return false;
    }

    //public method for dispatching messages. 
    //Messages can be send instantly or delayed.
    public void DispatchMessage(Telegram msg)
    {
        

        BaseGameEntity msgReceiver = EntityManager.Instance.GetEntityFromID(msg.GetReceiver());



        if (msgReceiver == null)
        {
            Debug.Log("No Receiver Entity with ID " + msg.GetReceiver() + " found!");

            return;
        }

        //msg with no delay
        if(IsMessageOldEnoughToSend(msg) == true)
        {
           

            Discharge(msgReceiver, msg);
        }
        //this is a delayed msg... store to the HashSet.
        else
        {
            delayedMessages.Add(msg);
        }

    }


    public void DispatchDelayedMessages()
    {
        List<Telegram> msgToBeDeleted = new List<Telegram>();

        foreach(var msg in delayedMessages)
        {
            //these messages are old enough to send. Discharge and then delete them from the set.
            if(IsMessageOldEnoughToSend(msg) == true)
            {
                Discharge(EntityManager.Instance.GetEntityFromID(1), msg);
                msgToBeDeleted.Add(msg);
            }
        }

        //delete the discharged messages.
        foreach(var msg in msgToBeDeleted)
        {
            delayedMessages.Remove(msg);
        }
    }




}
