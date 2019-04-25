using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour{

    #region Variables

    private MainNotification MainNotif;

    #endregion

    // Use this for initialization
    void Start () {
        MainNotif = GetComponent<MainNotification>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Public Functions
    public void OnMouseClick()
    {
        // Detected click on panel
        Debug.Log("Detected panel click");
    }

    public void OnMouseEnter()
    {
        Debug.Log("Mouse Enter");
        SetActiveMarkerImage();
    }

    public void OnMouseExit()
    {
        Debug.Log("Mouse leave");
        SetInnactiveMarkerImage();
    }
    #endregion

    #region Private Functions 
    private void SetActiveMarkerImage() {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-yellow");
    }

    private void SetInnactiveMarkerImage()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-purple");
    }

    #endregion
}
