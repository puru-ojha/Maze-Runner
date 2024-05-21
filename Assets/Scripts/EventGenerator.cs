using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Trigger Activation script is being used in place of Event Generator
/*
[System.Serializable]
public class collidedevent : UnityEvent<Collider>
{
}
public class EventGenerator : MonoBehaviour
{
    public collidedevent collided;
    public PlayerPosManager pla;
    public wallGenerator wallgen;
    bool isColiiding;
    PhysicMaterial previous;
    // Start is called before the first frame update
    void Start()
    {
        pla = GameObject.Find("Manager").GetComponent<PlayerPosManager>();
        wallgen = GameObject.Find("Manager").GetComponent<wallGenerator>();
        if (collided != null)
        {
            collided = new collidedevent();
        }
        
        collided.AddListener(pla.collisionListener);
        collided.AddListener(wallgen.collisionListenerTwo);
    }

    // Update is called once per frame
    void Update()
    {
        isColiiding = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.material != previous)
        {
            previous = other.material;
        }
        
        //Debug.Log("collided");
        if (isColiiding)
        {
            return;
        }
        isColiiding = true;
        if (isColiiding)
        {
            collided.Invoke(other);
        }       
    }
    private void OnCollisionEnter(Collision collision)
    {
        var contactPoint = collision.GetContact(0);
        if (Vector3.Dot(contactPoint.normal, collision.gameObject.transform.position) > 0 )
        {
            //Forward Direction)
        }
        else
        {
            //Backward Direction
        }
    }


}
*/