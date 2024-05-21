using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosManager : MonoBehaviour
{
    //This script decides the direction that is chosen by the player and supplies pos,dir to raychecker and wallGenerator.
    [SerializeField]
    GameObject player;
    Vector3 InitialPlayerPos;
    float InitialPlayerDir;
    [HideInInspector]
    public static Vector3 PlayerPos { get; private set; }
    [HideInInspector]
    public static Vector3 PlayerDir { get; private set; }
    List<int> indicesCopy;
    string last;
    string secondLast;
    public bool Forward = true;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = player.transform.position;
        PlayerDir = player.transform.rotation.eulerAngles;
        
        //new Vector3(0f, player.transform.rotation.y,0f);
        //Debug.Log(" the direction is " + PlayerDir);
        
    }
    public void collisionListener(Collider current)
    {
        
        
        Debug.Log("The current collider is " + current.name + " the last name is " + last + " The previous name is " + secondLast);
        if (Forward)
        {
            movePlayer(current.name);
            //previous = current;
            Debug.Log("Forward in playerpos");
        }
        
        else
        {
            Debug.Log("For backward the current name is " + current.name + " The previous name is " + secondLast);
            movePlayerbackwards(current.name);
            movePlayerbackwards(secondLast);
            //Debug.Log("backward in playerpos");
        }
        secondLast = last;
        last = current.name;
    }

    void movePlayer(string name)
    {
        int angle = int.Parse( name)*90;
        //Debug.Log("The angle for movement is " + angle);
        //Debug.Log("BEfore movement position is " + PlayerPos);
        PlayerDir = PlayerDir + new Vector3(0,angle,0);
        PlayerPos = PlayerPos + Quaternion.Euler(PlayerDir) * Vector3.forward * (Variables.instance.pathlen + Variables.instance.pathwid);
        
        Debug.Log("After movement position is " + PlayerPos + "The Direction is " + PlayerDir);
    }
    void movePlayerbackwards(string name)
    {
        int angle = int.Parse(name) * 90;
        //Debug.Log("The angle for movement is " + angle);
        //Debug.Log("BEfore movement position is " + PlayerPos);
        
        PlayerPos = PlayerPos - Quaternion.Euler(PlayerDir) * Vector3.forward * (Variables.instance.pathlen + Variables.instance.pathwid);
        PlayerDir = (PlayerDir - new Vector3(0, angle, 0));

        Debug.Log("Backward position is " + PlayerPos + "The Direction is " + PlayerDir);
    }
}
