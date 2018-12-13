using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.UI;  public class MinimapControl : MonoBehaviour {      [SerializeField]     private GameObject defaultMarker;     [SerializeField]     private GameObject plane;     private GameObject minimap;      private float xScale;     private float yScale;      float minRangeX;
    float maxRangeX;
    float minRangeY;
    float maxRangeY;      // Use this for initialization     void Start () {         minimap = this.transform.gameObject;

        Setup();          TestPrint();     }      public void SetNotificationMinimapLocation (Notification notification) {
        // Irrelevant notifications
        if (notification.POI == 0) {             SetIrrelevantNotificationLocation(notification);             //CreateNewMarker(notification);         } else {             SetRelevantNotificationLocation(notification);             //CreateNewMarker(notification);         }     }      private void SetIrrelevantNotificationLocation(Notification notification) {         float corX = Random.Range(minRangeX, maxRangeX);         float corY = Random.Range(minRangeY, maxRangeY);         notification.MinimapLocation = new Vector2(corX, corY);
    }      private void SetRelevantNotificationLocation(Notification notification) {
        // Calculate location of POI relatively to minimap
        Vector2 POILocation = new Vector2(-1 * (notification.POILocation.x * xScale), (-1 * (notification.POILocation.z * yScale)) + defaultMarker.GetComponent<RectTransform>().rect.height);         notification.MinimapLocation = POILocation + Random.insideUnitCircle * 60;     }      public void CreateNewMarker(Notification notification) {
        GameObject marker = Instantiate(defaultMarker) as GameObject;         marker.SetActive(true);         marker.transform.SetParent(this.transform, false);          // Set marker on correct location         marker.transform.localPosition = notification.MinimapLocation;
    }      private void Setup() {
        CalculateScale();         CalculateBoundries();
    }      private void CalculateScale() {
        float offset = 2f;          Vector2 mapSize = minimap.GetComponent<RectTransform>().sizeDelta;         Vector2 planeSize = plane.GetComponent<RectTransform>().sizeDelta;         xScale = planeSize.x / mapSize.x + offset;         yScale = planeSize.y / mapSize.y + offset;
    }      private void CalculateBoundries() {
        float saveMargin = 100f; 
        minRangeX = (minimap.GetComponent<RectTransform>().sizeDelta.x / 2 * -1) + saveMargin;         maxRangeX = (minimap.GetComponent<RectTransform>().sizeDelta.x / 2) - saveMargin;         minRangeY = (minimap.GetComponent<RectTransform>().sizeDelta.y / 2 * -1) + saveMargin;         maxRangeY = (minimap.GetComponent<RectTransform>().sizeDelta.y / 2) - saveMargin;
    }      private void TestPrint() {         Debug.Log("Minimap: " + minimap.GetComponent<RectTransform>().sizeDelta);         Debug.Log("Plane: " + plane.GetComponent<RectTransform>().sizeDelta);
    } } 