using System;
using UnityEngine;
//Script to create a bus for instantiating Ball objects in a Unity game using ScriptableObject
// This script allows for the instantiation of Ball objects and notifies subscribers when a new Ball is instantiated.
// It uses the ScriptableObject pattern to create a bus that can be used to communicate between different parts of the game without tight coupling.

[CreateAssetMenu(fileName = "BallInstantiateDataBusSo", menuName = "Scriptable Objects/Bus/Ball")]
public class BallInstantiateDataBusSo : ScriptableObject
{
    public event Action<Ball> OnInstantiateBall;
    
    public void SendInstantiateBall(Ball ball)
    {
        OnInstantiateBall?.Invoke(ball);
    }
}
