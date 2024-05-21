using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public RayChecker rc;
    List<List<int>> indicesChosenList = new List<List<int>>();
    // Start is called before the first frame update
    void Start()
    {
        //rc = gameObject.GetComponent<RayChecker>();
    }

    public List<int> DirectionChoser()
    {
        rc.raycheckrunner();
        List<int> chosen;
        chosen = rc.indices;
        if (chosen.Count == 2)
        {
            int flip = Random.Range(0, 2);
            if (flip == 0)
            {
                chosen.RemoveAt(Random.Range(0, 2));
            }   
        }
        else if (chosen.Count == 3)
        {
            int flip = Random.Range(0, 3);
            if (flip == 0)
            {
                //remove two from chosen
                chosen.RemoveAt(Random.Range(0, 3));
                chosen.RemoveAt(Random.Range(0, 2));
            }
            else if (flip == 1)
            {
                chosen.RemoveAt(Random.Range(0, 3));
            }
        }
        indicesChosenList.Add(chosen);
        return chosen;
    }
}
