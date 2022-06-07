using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    protected virtual void Awake()
    {
        SetInstance();
    }

    protected void SetInstance()
    {
        if (Instance == null)
            Instance = (T)(object)this;
        else
            Destroy(Instance.gameObject);
    }
}