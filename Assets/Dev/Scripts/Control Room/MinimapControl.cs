using UnityEngine; using UnityEngine.UI;  public class MinimapControl : MonoBehaviour {      // Viarables set in Unity Editor     [SerializeField]     private GameObject defaultMarker;     [SerializeField]      // Private viarables     private GameObject plane;     private GameObject minimap;     private LocationSync locationSync;      private float xScale;     private float yScale;      private float minRangeX;
    private float maxRangeX;
    private float minRangeY;
    private float maxRangeY;      // Use this for initialization     void Start () {         minimap = this.transform.gameObject;
        Setup();     }      public void SetNotificationMinimapLocation (Notification notification) {
                 // As all notifications are relevant now         SetRelevantNotificationLocation(notification);     }      // Not needed for now, perhaps in the future     private void SetIrrelevantNotificationLocation(Notification notification) {         float corX = Random.Range(minRangeX, maxRangeX);         float corY = Random.Range(minRangeY, maxRangeY);         notification.MinimapLocation = new Vector2(corX, corY);
    }      private void SetRelevantNotificationLocation(Notification notification) {
        // Calculate location of POI relatively to minimap
        //Vector2 POILocation = new Vector2(-1 * (notification.location.x * xScale) + defaultMarker.GetComponent<RectTransform>().rect.width / 2, (-1 * (notification.POILocation.z * yScale)) + defaultMarker.GetComponent<RectTransform>().rect.height);
        //notification.MinimapLocation = POILocation + Random.insideUnitCircle * 60;
        notification.MinimapLocation = locationSync.GetSuspectMinimapLocation() + Random.insideUnitCircle * 60;     }      public void CreateNewMarker(Vector2 minimapLocation, bool selectedMarker = false) {
        GameObject marker = Instantiate(defaultMarker) as GameObject;          if (selectedMarker)
            marker.GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-yellow");          marker.SetActive(true);         marker.transform.SetParent(this.transform, false);          // Set marker on correct location         marker.transform.localPosition = minimapLocation;
    }      public void DeleteSpecifiqMarker(Vector2 minimapLocation) {         // Begin at 2 as the first two items are the playerIcon and the marker prefab
        for (int i = 2; i < transform.childCount; i++) {
            Vector2 markerlocation = transform.GetChild(i).localPosition;              if (minimapLocation == markerlocation) {                 Destroy(transform.GetChild(i).gameObject);                 break;
            }
        }
    }      private void Setup() {
        locationSync = this.GetComponent<LocationSync>();
        CalculateScale();         CalculateBoundries();
    }      private void CalculateScale() {
        float offsetX = 1.7f;
        float offsetY = 2f;          Vector2 mapSize = minimap.GetComponent<RectTransform>().sizeDelta;         Vector2 planeSize = plane.GetComponent<RectTransform>().sizeDelta;         xScale = planeSize.x / mapSize.x + offsetX;         yScale = planeSize.y / mapSize.y + offsetY;
    }      private void CalculateBoundries() {
        float saveMargin = 100f; 
        minRangeX = (minimap.GetComponent<RectTransform>().sizeDelta.x / 2 * -1) + saveMargin;         maxRangeX = (minimap.GetComponent<RectTransform>().sizeDelta.x / 2) - saveMargin;         minRangeY = (minimap.GetComponent<RectTransform>().sizeDelta.y / 2 * -1) + saveMargin;         maxRangeY = (minimap.GetComponent<RectTransform>().sizeDelta.y / 2) - saveMargin;
    }  } 