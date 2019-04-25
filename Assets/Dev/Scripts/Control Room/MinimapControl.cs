using UnityEngine; using UnityEngine.UI;  public class MinimapControl : MonoBehaviour {      // Viarables set in Unity Editor     [SerializeField]     private GameObject MarkerPrefab;     [SerializeField]     private GameObject markersContainer;      // Private viarables     private GameObject plane;     private GameObject minimap;     private LocationSync locationSync;      private float xScale;     private float yScale;      private float minRangeX;
    private float maxRangeX;
    private float minRangeY;
    private float maxRangeY;      // Use this for initialization     void Start () {         minimap = this.transform.gameObject;
        Setup();     }       // Public Functions     public void SetNotificationMinimapLocation (Notification notification) {
                 // As all notifications are relevant now         SetRelevantNotificationLocation(notification);     }      public void InitiateNotificationOnMinimap(Notification notification) {
        // Set location nearby suspect         SetRelevantNotificationLocation(notification);         CreateNewMarker(notification.MinimapLocation);
    }

    // Not needed for now, perhaps in the future
    private void SetIrrelevantNotificationLocation(Notification notification) {         float corX = Random.Range(minRangeX, maxRangeX);         float corY = Random.Range(minRangeY, maxRangeY);         notification.MinimapLocation = new Vector2(corX, corY);
    }      private void SetRelevantNotificationLocation(Notification notification) {
        notification.MinimapLocation = locationSync.GetSuspectMinimapLocation() + Random.insideUnitCircle * 100;     }      public void CreateNewMarker(Vector2 minimapLocation, bool selectedMarker = false) {
        GameObject marker = Instantiate(MarkerPrefab) as GameObject;          if (selectedMarker)
            marker.GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-yellow");          marker.SetActive(true);         marker.transform.SetParent(markersContainer.transform, false);          // Set marker on correct location         marker.transform.localPosition = minimapLocation;
    }      //New     public void CreateNewMarker2(Notification notification) {         SetRelevantNotificationLocation(notification);         GameObject marker = Instantiate(MarkerPrefab) as GameObject;         marker.SetActive(true);         marker.transform.SetParent(markersContainer.transform, false);          // Set marker on correct location         marker.transform.localPosition = notification.MinimapLocation;          // Set up main notification data         SetUpMainNotification(marker, notification);     }      public void DeleteSpecifiqMarker(Vector2 minimapLocation) {         // Begin at 2 as the first two items are the playerIcon and the marker prefab
        for (int i = 2; i < transform.childCount; i++) {
            Vector2 markerlocation = transform.GetChild(i).localPosition;              if (minimapLocation == markerlocation) {                 Destroy(transform.GetChild(i).gameObject);                 break;
            }
        }
    }      // Private functions     private void SetUpMainNotification(GameObject marker, Notification notif) {
        MainNotification mainNotif = marker.GetComponent<MainNotification>();         mainNotif.MinimapLocation = notif.MinimapLocation;         mainNotif.keyNote = GetKeyNotes(notif.Message);         mainNotif.notifications.Add(notif);
    }      private string GetKeyNotes(string message)
    {
        return message;         //ToDo actualy get the keynotes
    }

    private void Setup() {
        locationSync = this.GetComponent<LocationSync>();
        CalculateScale();         CalculateBoundries();
    }      private void CalculateScale() {
        float offsetX = 1.7f;
        float offsetY = 2f;          Vector2 mapSize = minimap.GetComponent<RectTransform>().sizeDelta;         Vector2 planeSize = plane.GetComponent<RectTransform>().sizeDelta;         xScale = planeSize.x / mapSize.x + offsetX;         yScale = planeSize.y / mapSize.y + offsetY;
    }      private void CalculateBoundries() {
        float saveMargin = 100f; 
        minRangeX = (minimap.GetComponent<RectTransform>().sizeDelta.x / 2 * -1) + saveMargin;         maxRangeX = (minimap.GetComponent<RectTransform>().sizeDelta.x / 2) - saveMargin;         minRangeY = (minimap.GetComponent<RectTransform>().sizeDelta.y / 2 * -1) + saveMargin;         maxRangeY = (minimap.GetComponent<RectTransform>().sizeDelta.y / 2) - saveMargin;
    }  } 