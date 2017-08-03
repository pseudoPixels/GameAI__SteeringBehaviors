using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class Telegram  {

    int sender;

    int receiver;

    //enumerated messages
    int message;

    //message can be dispatched immediately or delayed for a specified time
    double dispatchTime;


    public Telegram(int sender, int receiver, int message, double delay)
    {
        this.sender = sender;
        this.receiver = receiver;
        this.message = message;

        System.TimeSpan span = System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1, 0,0,0, System.DateTimeKind.Utc));
        double seconds = span.TotalSeconds;
        this.dispatchTime = seconds + delay;
    }


    public int GetSender()
    {
        return this.sender;
    }

    public int GetReceiver()
    {
        return this.receiver;
    }


    public int GetMessage()
    {
        return this.message;
    }


    public double GetDispatchTime()
    {
        return this.dispatchTime;
    }

    






}
