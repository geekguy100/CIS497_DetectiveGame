/*****************************************************************************
// File Name :         ISubject.cs
// Author :            Kyle Grenier
// Creation Date :     04/14/2021
//
// Brief Description : A subject for an observer pattern. Will notify any registered observers of changes.
*****************************************************************************/
using UnityEngine;
using System.Collections.Generic;

public abstract class ISubject : MonoBehaviour
{
    private List<IObserver> observers;

    private void Awake()
    {
        observers = new List<IObserver>();
    }
    
    /// <summary>
    /// Registers an IObserver to this ISubject.
    /// </summary>
    /// <param name="observer">The IObserver to register.</param>
    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    /// <summary>
    /// Deregisters an IObserver from the ISubject.
    /// </summary>
    /// <param name="observer">The IObserver to deregister from the ISubject.</param>
    protected void RemoveObserver(IObserver observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
        else
            Debug.Log(gameObject.name + ": Could not remove " + observer + " for it is not registered with it.");
    }

    /// <summary>
    /// Notify all registered IObservers of a change in data.
    /// </summary>
    /// <param name="data">The changed data; the data to be passed to all registered IObservers.</param>
    protected void NotifyObservers(object data)
    {
        foreach (IObserver observer in observers)
            observer.UpdateData(data);
    }
}