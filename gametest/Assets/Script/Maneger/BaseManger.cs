using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BaseManeger<T> where T : new()
{
    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
        {
            if (instance == null)
            {
                instance = new T();
            }
        }
        return instance;
    }
}
