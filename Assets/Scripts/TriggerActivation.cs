using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivation : MonoBehaviour
{

    public PlayerPosManager pla;
    public wallGenerator wallgen;
    bool iscolliding = false;
    ContactPoint point;
    // Start is called before the first frame update
    void Start()
    {
        pla = GameObject.Find("Manager").GetComponent<PlayerPosManager>();
        wallgen = GameObject.Find("Manager").GetComponent<wallGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        iscolliding = false;
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ontrigger is called");
        pla.collisionListener(other);
        wallgen.collisionListenerTwo(other);
    }
    */
    private void OnCollisionExit(Collision collision)
    {
        if (iscolliding == false)
        {
            Debug.Log("OnCollision is called");
            var contactPoint = point;
            Debug.Log("The normal of the cotnact point is " + contactPoint.normal);
            Debug.Log("The direction of the prefab is " + collision.gameObject.transform.rotation * Vector3.forward);
            if (Vector3.Dot(-contactPoint.normal, collision.gameObject.transform.rotation * Vector3.forward) > 0)
            {
                Debug.Log("Forward Collison");
                pla.Forward = true;
                wallgen.Forward = true;
            }
            else
            {
                Debug.Log("Backward Collision");
                pla.Forward = false;
                wallgen.Forward = false;
            }
            pla.collisionListener(collision.collider);
            wallgen.collisionListenerTwo(collision.collider);
        }
        iscolliding = true;
    }
    
    private void OnCollisionEnter (Collision collision)
    {
        point = collision.GetContact(0);
    }
    
}
