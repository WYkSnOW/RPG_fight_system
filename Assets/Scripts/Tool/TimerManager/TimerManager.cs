using System;
using System.Collections.Generic;
using UnityEngine;
using HuHu;

public class TimerManager : Singleton<TimerManager>
{
    [SerializeField, Header("计时器的数量")] private int timerCount;
    private Queue<GameTimer> notWorkTimers = new Queue<GameTimer>();
    private List<GameTimer> isWorkingTimers = new List<GameTimer>();

    protected void Start()
    {
        for (int i = 0; i < timerCount; i++) 
        {
            CreateTimer();
        }
    }
    private void Update()
    {
        UpdateTime();
        
    }
    private void CreateTimer()
    { 
        var timer = new GameTimer();
        notWorkTimers.Enqueue(timer);
    }
    /// <summary>
    /// 会受到ScaleTime影响的计时器
    /// </summary>
    /// <param name="timer">计时的时间</param>
    /// <param name="action">计时完成后调用的委托</param>
    public void GetOneTimer(float timer, Action action)
    {
        if (notWorkTimers.Count == 0) { CreateTimer(); }
        GameTimer gameTimer = null;
        gameTimer= notWorkTimers.Dequeue();
        gameTimer.StartTimer(false,timer, action);  
        isWorkingTimers.Add(gameTimer);
    }
   /// <summary>
   /// 不会受到ScaleTime影响的计时器
   /// </summary>
   /// <param name="time"></param>
   /// <param name="action"></param>
   /// <returns></returns>
    public GameTimer GetRealTimer(float time, Action action)
    {
        if (notWorkTimers.Count == 0) { CreateTimer(); }
        GameTimer gameTimer = new GameTimer();
        gameTimer = notWorkTimers.Dequeue();
        gameTimer.StartTimer(true,time, action);
        isWorkingTimers.Add(gameTimer);
        return gameTimer;

    }
    public GameTimer GetTimer(float time, Action action)
    {
        if (notWorkTimers.Count == 0) { CreateTimer(); }
        GameTimer gameTimer = new GameTimer();
        gameTimer = notWorkTimers.Dequeue();
        gameTimer.StartTimer(false, time, action);
        isWorkingTimers.Add(gameTimer);
        return gameTimer;

    }
    /// <summary>
    /// 向外部提供一个销毁计时器的方法
    /// </summary>
    /// <param name="gameTimer"></param>
    public void UnregisterTimer(GameTimer gameTimer)
    {
        if (gameTimer == null) { return; }
        //非工作计时器不能被销毁，因为可能会注册其他事件
        if (gameTimer.TimerStation != TimerStation.DoWorking) { return; }
        gameTimer.InitTimer();
        isWorkingTimers.Remove(gameTimer);
        notWorkTimers.Enqueue(gameTimer);
    }
    /// <summary>
    /// 推动WorkingTimer的计时方法，以及处理完成工作的Timer
    /// </summary>
    private void UpdateTime()
    {
        if (isWorkingTimers.Count == 0) { return; }
        for (int i = 0;i < isWorkingTimers.Count; i++) 
        {
            if (isWorkingTimers[i].TimerStation == TimerStation.DoWorking)
            {
                if (!isWorkingTimers[i].IsRealTime)
                {
                    isWorkingTimers[i].UpdateTimer();
                }
                else
                {
                    isWorkingTimers[i].UpdateRealTimer();
                }
            }       
            else if (isWorkingTimers[i].TimerStation==TimerStation.DoneWorked)
            {
                isWorkingTimers[i].InitTimer();
                notWorkTimers.Enqueue(isWorkingTimers[i]);
                isWorkingTimers.Remove(isWorkingTimers[i]);
            }
        }
    }
}
