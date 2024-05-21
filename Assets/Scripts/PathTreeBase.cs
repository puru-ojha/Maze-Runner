using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTreeBase 
{
    //This class is there to store path tree for backtracking capability.
    public int index;
    public PathTreeBase parent;
    public List<PathTreeBase> Children = new List<PathTreeBase>();

    public PathTreeBase(int ind)
    {
        this.index = ind;
    }
    
    public PathTreeBase(int ind, PathTreeBase parent)
    {
        this.index = ind;
        this.parent = parent;
        parent.Children.Add(this);
    }
    public void Add_parent(PathTreeBase par)
    {
        parent = par;
        //parent.Children.Add(this);
    }
    public void Add_child(PathTreeBase child)
    {
        //child.parent = this;
        this.Children.Add(child);
    }
    public PathTreeBase GetParent()
    {
        return this.parent;
    }
    public PathTreeBase GetChild(int i)
    {
        foreach (PathTreeBase ch in this.Children)
        {
            //Debug.Log("The childrens index is " + ch.index);
            if (ch.index == i)
            {
                return ch;
            }
        }
        Debug.Log("Problem Detected in GetChild Function");
        return null;
        //return newParent;
    }
}
