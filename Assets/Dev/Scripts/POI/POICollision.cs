using UnityEngine;

public class POICollision : MonoBehaviour
{

    [SerializeField]
    private POIManager POIManger;

    public void Start()
    {
        POIManger = transform.parent.gameObject.GetComponent<POIManager>();
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "MainCamera") {

            // Disabling the collider so this POI won't detect collision anymore
            GetComponent<SphereCollider>().enabled = false;
  
            // Notify POI Manager
            POIManger.POIReached();
        } 
    }
    
}
