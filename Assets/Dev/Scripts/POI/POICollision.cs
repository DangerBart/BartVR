using UnityEngine;

public class POICollision : MonoBehaviour
{

    [SerializeField]
    private POIManager POIManger;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Detected collission with POI");
        this.GetComponent<SphereCollider>().enabled = false;
    }
}
