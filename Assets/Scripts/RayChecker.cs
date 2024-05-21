using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayChecker : MonoBehaviour
{
    //This script takes playerPosition and playerDirection as input, checks for available paths for next n iterations and returns an array of available indices.
    //PlayerPosManager playerPosManager;
    Vector3 playerPosition;
    Vector3 playerRotation;
    public int pathlen = 2;
    public int pathwid = 1;
    float movelength;
    float raylen;
    [Range(1, 3)]
    public int maxpath;
    public List<int> indices;
    // Start is called before the first frame update
    void Start()
    {
        //playerPosManager = GetComponent<PlayerPosManager>();
        //playerPosition = playerPosManager.PlayerPos;
        //playerRotation = playerPosManager.PlayerDir;
        //Debug.Log("Player rotation is " + playerRotation);
        raylen =  pathlen + 3 / 2 * pathwid;
        movelength = pathlen + pathwid;
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerPosition = PlayerPosManager.PlayerPos;
            playerRotation = PlayerPosManager.PlayerDir;
            Debug.Log("PlayerPosition is " + playerPosition);
            Debug.Log("PlayerDirection is " + playerRotation);
            indices = MainFunction(playerPosition, playerRotation);
            foreach (int p in indices)
            {
                Debug.Log("Not hitting at " + p);
            }
        }
        */
    }

    public void raycheckrunner()
    {
        playerPosition = PlayerPosManager.PlayerPos;
        playerRotation = PlayerPosManager.PlayerDir;
        //Debug.Log("PlayerPosition is " + playerPosition);
        //Debug.Log("PlayerDirection is " + playerRotation);
        indices = MainFunction(playerPosition, playerRotation);
        /*
        foreach (int p in indices)
        {
            Debug.Log("Not hitting at " + p);
        }
        */

    }
    List<int> MainFunction(Vector3 pos, Vector3 rot)
    {
        List<int> nohit = new();
        Node root = new(00);
        RaycastWrap(pos, rot, root);
        foreach (Node child in root.children)
        {
            nohit.Add(child.data);
        }
        return nohit;
    }
    void RaycastWrap(Vector3 pos, Vector3 rot, Node rootnode)
    {
        for (int i = -1; i < 2; i++)
        {
            Vector3 localpos = pos;
            Vector3 localrot = new(0, 0, 0);
            localrot.y = rot.y + 90 * i;
            if (!Physics.Raycast(localpos, Quaternion.Euler(localrot) * Vector3.forward, raylen))
            {
                Node gi = new(i);
                rootnode.Add_child(gi);
                //Debug.Log("Not hitting first turn " + i);
                for (int j = -1; j < 2; j++)
                {
                    //Instead of changing values from localpos change them from pos
                    localpos = pos + Quaternion.AngleAxis(rot.y + 90 * i , Vector3.up) * Vector3.forward *movelength;
                    localrot.y = rot.y + 90*i + 90*j;
                    //Debug.Log(" localpos is" + localpos + " localrot is " + localrot);
                    if (!Physics.Raycast(localpos, Quaternion.Euler(localrot) * Vector3.forward, raylen))
                    {
                        Node hi = new(j);
                        gi.Add_child(hi);
                        //Debug.Log("Not hitting second turn " + j);
                        //Debug.Log(" localpos is" + localpos + " localrot is " + localrot);
                        for (int k = -1; k < 2; k++)
                        {
                            localpos = pos + Quaternion.AngleAxis(rot.y + 90 * i + 90 * j , Vector3.up) * Vector3.forward * movelength;
                            localrot.y = rot.y + 90 * i + 90 * j + 90 * k;
                            //Debug.Log(" localpos is" + localpos + " localrot is " + localrot);
                            if (!Physics.Raycast(localpos, Quaternion.Euler(localrot) * Vector3.forward, raylen))
                            {
                                Node ki = new(k);
                                hi.Add_child(ki);
                                //Debug.Log("Not hitting third turn " + k);
                                //Debug.Log(" localpos is" + localpos + " localrot is " + localrot);

                            }
                        }
                    }
                }
            }
        }

        return;
    }

    void PlayerMover(ref Vector3 pos, Vector3 rot)
    {
        Debug.Log("The original position is " + pos);
        pos += Quaternion.Euler(rot) * Vector3.forward * pathwid;
        Debug.Log("The new position is " + pos);
    }


}
