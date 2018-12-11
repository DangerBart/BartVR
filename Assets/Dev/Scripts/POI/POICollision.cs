using System;
using UnityEngine;

public class POICollision : MonoBehaviour
{

    [SerializeField]
    private POIManager POIManager;

    public void Start()
    {
        POIManager = transform.parent.gameObject.GetComponent<POIManager>();
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "MainCamera") {

            // Disabling the collider so this POI won't detect collision anymore
            GetComponent<SphereCollider>().enabled = false;
  
            // Notify POI Manager
            POIManager.POIReached();
        } 
    }
    
}
