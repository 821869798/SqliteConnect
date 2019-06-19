using UnityEngine;
using System.Collections;
using System;

public class Singleton<T> where T: Singleton<T>, new()
{

    private static T m_instance;
    private static readonly object m_staticSyncRoot = new object();

    public static T Instance
    {
        get {
            lock (m_staticSyncRoot)
            {
                if (m_instance == null)
                {
                    m_instance = new T();
                    m_instance.Initialize();
                }
            }
            return m_instance;
        }
    }

    protected virtual void Initialize()
    {
    }
}
