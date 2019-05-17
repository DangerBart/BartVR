using UnityEngine;
using UnityEngine.UI;

public class CursorHandler : MonoBehaviour { 
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Marker")) {
            other.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-yellow");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Marker")) {
            other.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-purple");
        }                                     
    }
}
