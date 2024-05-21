using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallGenerator : MonoBehaviour
{
    // This one takes playerpos and playerdir from PlayerPosManager and a variable range for wall gen and decides on the position and rotation for the wallPrefabs and instantiates them
    //PlayerPosManager playerposManager;
    Vector3 pos;
    Vector3 rot;
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private GameObject fillers;
    private Queue<GameObject[]> Wallqueue = new Queue<GameObject[]>();
    private Queue<GameObject[]> Fillqueue = new Queue<GameObject[]>();
    List<int> index_list = new List<int>();
    public MapMaker mapmaker;
    int wallcount = 0;
    int curr = 0;
    public PathTreeBase FirstParent;// = new PathTreeBase(00);
    Collider PreviousBox = new Collider();
    public bool Forward = true;
    void Start()
    {
        pos = PlayerPosManager.PlayerPos;
        rot = PlayerPosManager.PlayerDir;
        //mapmaker = gameObject.GetComponent<MapMaker>();
        //chosen = playerposManager.chosenIndex;
        //Debug.Log("FirstCall");
        FirstParent = new PathTreeBase(00);
        wallSpawner(FirstParent);
        //Debug.Log("SecondCall");
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < Variables.instance.numWall; i++)
            {
                int ind = mapmaker.DirectionChoser();
                index_list.Add(ind);
                InstantiateMethod(ind);
            }
            
        }
        */
    }

    public GameObject InstantiateMethod(int index)
    {
        
        pos = PlayerPosManager.PlayerPos;
        rot = PlayerPosManager.PlayerDir;
        GameObject thiswall = Instantiate(wall,pos + Quaternion.Euler(new Vector3(0, rot.y + 90 * index, 0f))*Vector3.forward * Variables.instance.pathwid / 2, Quaternion.Euler(new Vector3(0,rot.y + 90 *index, 0f)));
        BoxCollider thisbox = thiswall.GetComponent<BoxCollider>();
        thisbox.name = index.ToString();
        
        
        /*
        if (curr > 1)
        {
            Debug.Log("The player pos before fillers is " + pos + " The player dir is " + rot);
            Debug.Log(" The index is " + index);
            InstantiateFillersForSingle(index);
            
            Debug.Log("Last element of the list is " + index_list[0]);
            Debug.Log("Second Last element of the list is " + index_list[1]);
           
        }
        */
     
        
        return thiswall;
    }
    public GameObject[] InstantiateFillersForSingle(int current)
    {
        //Debug.Log("Singles is called with " + current);
        GameObject[] fillersArray = new GameObject[2];
        if (current == -1)
        {
            //GameObject[] fillersArray = new GameObject[2];
            //Debug.Log("For left turn " + pos + Quaternion.Euler(rot) * Vector3.forward * Variables.instance.pathwid / 2);
            fillersArray[0] = Instantiate(fillers, pos + Quaternion.Euler(rot) * Vector3.forward * Variables.instance.pathwid / 2, Quaternion.Euler(new Vector3(0, rot.y - 90, 0f)));
            fillersArray[1] = Instantiate(fillers, pos + Quaternion.Euler(new Vector3(0, rot.y + 90, 0f)) * Vector3.forward * Variables.instance.pathwid / 2, Quaternion.Euler(rot));
            //Fillqueue.Enqueue(fillersArray);
        }
        else if (current == 0)
        {
            //GameObject[] fillersArray = new GameObject[2];
            fillersArray[0] = Instantiate(fillers, pos + Quaternion.Euler(new Vector3(0, rot.y - 90, 0f)) * Vector3.forward * Variables.instance.pathwid / 2, Quaternion.Euler(rot));
            fillersArray[1] = Instantiate(fillers, pos + Quaternion.Euler(new Vector3(0, rot.y + 90, 0f)) * Vector3.forward * Variables.instance.pathwid / 2, Quaternion.Euler(rot));
            //Fillqueue.Enqueue(fillersArray);
        }
        else if (current == 1)
        {
            //GameObject[] fillersArray = new GameObject[2];
            fillersArray[0] = Instantiate(fillers, pos + Quaternion.Euler(rot) * Vector3.forward * Variables.instance.pathwid / 2, Quaternion.Euler(new Vector3(0, rot.y + 90, 0f)));
            fillersArray[1] = Instantiate(fillers, pos + Quaternion.Euler(new Vector3(0, rot.y - 90, 0f)) * Vector3.forward * Variables.instance.pathwid / 2, Quaternion.Euler(rot));
            //Fillqueue.Enqueue(fillersArray);

        }
        return fillersArray;
    }
    public GameObject[] InstantiateFillersFOrDouble(int currentOne, int currentTwo)
    {
        //Debug.Log("first of input is " + currentOne);
        //Debug.Log("second of input is " + currentTwo);
        GameObject[] fillersArray = new GameObject[1];
        if (currentOne + currentTwo == 1)
        {
            //GameObject[] fillersArray = new GameObject[1];
            fillersArray[0] = Instantiate(fillers, pos + Quaternion.Euler(new Vector3(0, rot.y - 90, 0f)) * Vector3.forward * Variables.instance.pathwid / 2, Quaternion.Euler(rot));
            //Fillqueue.Enqueue(fillersArray);
            //Debug.Log("They are here and enqueued");
        }
        else if (currentOne + currentTwo == -1)
        {
            //GameObject[] fillersArray = new GameObject[1];
            fillersArray[0] = Instantiate(fillers, pos + Quaternion.Euler(new Vector3(0, rot.y + 90, 0f)) * Vector3.forward * Variables.instance.pathwid / 2, Quaternion.Euler(rot));
            //Fillqueue.Enqueue(fillersArray);
            //Debug.Log("Ye instantiate kyu nahi hua");
        }
        else if (currentOne + currentTwo == 0)
        {
            //GameObject[] fillersArray = new GameObject[1];
            fillersArray[0] = Instantiate(fillers, pos + Quaternion.Euler(rot) * Vector3.forward * Variables.instance.pathwid / 2, Quaternion.Euler(new Vector3(0, rot.y + 90, 0f)));
            //Fillqueue.Enqueue(fillersArray);
        }
        return fillersArray;
    }

    void InstantiateFillersForThree()
    {
        //Do Nothing
    }

    void RemoveOlderWalls()
    {
        //if the wallcount is more than 3 remove old parts
        if (wallcount >2)
        {
            foreach (var f in Wallqueue.Dequeue())
            {
                Destroy(f);
            }
            wallcount -= 1;
            if ( curr > 2)
            {
                //Debug.Log("Dequefills runs");
                foreach (var f in Fillqueue.Dequeue())
                {
                    Destroy(f);
                    //Debug.Log("it destroys");
                }
            }
            
        }

    }
    void wallSpawner(PathTreeBase BaseNode)
    {
        List<int> ind = mapmaker.DirectionChoser();
        int indcount = ind.Count;
        GameObject[] wallsArray = new GameObject[indcount];
        GameObject[] fillersArray = new GameObject[3 - indcount];
        //Debug.Log("indcount is  " + indcount);
        for (int i = 0; i < indcount; i++)
        {
            index_list.Add(ind[i]);
            wallsArray[i] = InstantiateMethod(ind[i]);
            //Debug.Log("One child is created");
            //These lines are here to make the PathTreeBase
            PathTreeBase NewChild = new PathTreeBase(ind[i], BaseNode);   
            
        }
        Wallqueue.Enqueue(wallsArray);
        curr += 1;
        wallcount += 1;
        if (curr > 1)
        {
            if (ind.Count == 1)
            {
                fillersArray = InstantiateFillersForSingle(ind[0]);
            }
            else if (ind.Count == 2)
            {
                fillersArray = InstantiateFillersFOrDouble(ind[0], ind[1]);
            }
            else if (ind.Count == 3)
            {
                // This code is writtent so that there is something to remove when three options are there
                /*
                GameObject[] temp = new GameObject[1];
                temp[0] = fillers;
                fillersArray = temp;
                */
            }
        }
        Fillqueue.Enqueue(fillersArray);
        RemoveOlderWalls();
    }

    public void collisionListener(string st)
    {
        //Listens to collissions and accts accordingly
        Debug.Log("It collided with " + int.Parse(st));
        
        FirstParent = FirstParent.GetChild(int.Parse(st));
        if (FirstParent != null)
        {
            wallSpawner(FirstParent);
        }
        //BaseParent = new PathTreeBase(int.Parse(st));

        //When the User turns back
        // BackwardsWallSpawner() --> it will be called when the user is at start of current wall objects
        //Only one wall back and one wall forward is rendered.
        //When he is turning back aat the time when he hits the last collider then backwards wall Spawner should be 

    }

    public void collisionListenerTwo(Collider current)
    {
        //Debug.Log("Collision Listener in wallgen is called");
        if (Forward)
        {
            FirstParent = FirstParent.GetChild(int.Parse(current.name));
            if (FirstParent != null)
            {
                wallSpawner(FirstParent);
            }
            PreviousBox = current;
            Debug.Log("Player is moving Forward");
        }
        else
        {
            Debug.Log("Backwards is the player moving");
            wallSpawnerBackwards(FirstParent.parent.parent);
            FirstParent = FirstParent.parent;
            //We want to spawn it's parent's parent's children

        }
    }

    void wallSpawnerBackwards(PathTreeBase BaseNode)
    {
        List<int> ind = new List<int>();
        foreach (var ch in BaseNode.Children)
        {
            ind.Add(ch.index);
        }
        int indcount = ind.Count;
        GameObject[] wallsArray = new GameObject[indcount];
        GameObject[] fillersArray = new GameObject[3 - indcount];
        //Debug.Log("indcount is  " + indcount);
        for (int i = 0; i < indcount; i++)
        {
            index_list.Add(ind[i]);
            wallsArray[i] = InstantiateMethod(ind[i]);
            //Debug.Log("One child is created");
            //These lines are here to make the PathTreeBase
            PathTreeBase NewChild = new PathTreeBase(ind[i], BaseNode);

        }
        Wallqueue.Enqueue(wallsArray);
        curr += 1;
        wallcount += 1;
        if (curr > 1)
        {
            if (ind.Count == 1)
            {
                fillersArray = InstantiateFillersForSingle(ind[0]);
            }
            else if (ind.Count == 2)
            {
                fillersArray = InstantiateFillersFOrDouble(ind[0], ind[1]);
            }
            else if (ind.Count == 3)
            {
                // This code is writtent so that there is something to remove when three options are there
                /*
                GameObject[] temp = new GameObject[1];
                temp[0] = fillers;
                fillersArray = temp;
                */
            }
        }
        Fillqueue.Enqueue(fillersArray);
        //RemoveOlderWalls();
    }


    //As he is moving we want to spawn all the children, if he walks inside one of them again move forward should be called.
}
