﻿using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A timer
/// </summary>
public class Timer : MonoBehaviour
{
    #region Fields

    // timer duration
    float totalSeconds = 0;

    // timer execution
    float elapsedSeconds = 0;
    bool running = false;

    // support for Finished property
    bool started = false;

    //bool such that timer runs on unscaled time
    bool unscaled = false;

    // event for when the timer finishes
    TimerFinishedEvent timerFinishedEvent = new TimerFinishedEvent();

    #endregion

    #region Properties

    /// <summary>
    /// Sets the duration of the timer
    /// The duration can only be set if the timer isn't currently running
    /// </summary>
    /// <value>duration</value>
    public float Duration
    {
        set
        {
            if (!running)
            {
                totalSeconds = value;
            }
        }
    }
    /// <summary>
    /// Gets whether or not the timer is currently running
    /// </summary>
    /// <value>true if running; otherwise, false.</value>
    public bool Running
    {
        get { return running; }
    }

    public float TimeLeft
    {
        get
        {
            if (running)
                return totalSeconds - elapsedSeconds;
            else
            {
                return 0f;
            }
        }
    }

    #endregion

    #region Methods
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // update timer and check for finished
        if (running)
        {
            elapsedSeconds += (unscaled)?Time.unscaledDeltaTime : Time.deltaTime;
            if (elapsedSeconds >= totalSeconds)
            {
                running = false;
                timerFinishedEvent.Invoke();
            }
        }
    }

    /// <summary>
    /// Runs the timer
    /// Because a timer of 0 duration doesn't really make sense,
    /// the timer only runs if the total seconds is larger than 0
    /// This also makes sure the consumer of the class has actually 
    /// set the duration to something higher than 0
    /// </summary>
    public void Run()
    {
        // only run with valid duration
        if (totalSeconds > 0)
        {
            started = true;
            running = true;
            unscaled = false;
            elapsedSeconds = 0;
        }
    }

    public void UnScaledRun()
    {
        // only run with valid duration
        if (totalSeconds > 0)
        {
            started = true;
            running = true;
            unscaled = true;
            elapsedSeconds = 0;
        }
    }

    public void Stop(bool bruteStop)
    {
        started = false;
        running = false;
        unscaled = false;
        if(!bruteStop)
            timerFinishedEvent.Invoke();
    }

    public void AddTimerFinishedListener(UnityAction callback)
    {
        timerFinishedEvent.AddListener(callback);
    }
    #endregion
}

