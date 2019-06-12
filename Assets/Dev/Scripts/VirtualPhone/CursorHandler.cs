using UnityEngine;
using UnityEngine.UI;

public class CursorHandler : MonoBehaviour {
    public static bool OnMarker;
    public static GameObject marker;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Enter marker");
        if (other.CompareTag("Marker")) {
            other.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-yellow");
            OnMarker = true;
            marker = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Marker")) {
            other.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-purple");
            OnMarker = false;
            marker = null;
        }

        Debug.Log("leave marker");
    }
}
