﻿using System.Collections;
using System.Collections.Generic;

public class Singleton<T> where T : class, new()
{
    static T instance;

    public static T Ins
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

}