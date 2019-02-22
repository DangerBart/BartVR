using UnityEngine;
using UnityEngine.UI;

public class VRButtonInteractTest : MonoBehaviour {
    [SerializeField]
    private GameObject notification;
    [SerializeField]
    private GameObject validate;

	public void buttonTest() {
        validate.SetActive(!validate.activeInHierarchy);
        Debug.Log("I have been clicked");
    }

    public void notificationTest() {
        notification.SetActive(!notification.activeSelf);
    }
}
