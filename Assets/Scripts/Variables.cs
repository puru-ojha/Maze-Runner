using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public static Variables instance { get; private set; }
    public int pathlen = 2;
    public int pathwid = 1;
    public int numWall = 1;
    private void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
    }
}
