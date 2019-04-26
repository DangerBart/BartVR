using System.Collections;
using UnityEngine; using UnityEngine.UI;  public class MinimapControl : MonoBehaviour {      // Viarables set in Unity Editor     [SerializeField]     private GameObject MarkerPrefab;     [SerializeField]     private GameObject markersContainer;
    [SerializeField]
    private GameObject plane;      // Private viarables     private GameObject minimap;     private LocationSync locationSync;      private float xScale;     private float yScale;      private float minRangeX;
    private float maxRangeX;
    private float minRangeY;
    private float maxRangeY;      // Use this for initialization     void Start () {         minimap = this.transform.gameObject;
        Setup();     }


    #region Public Functions     public void InitiateNotificationOnMinimap(Notification notification) {
        // Set location nearby suspect
        SetRelevantNotificationLocation(notification);         CreateMarker(notification);
        Debug.Log("Ay");

        // Coroutine
        StartCoroutine("LookForMergableNotificartionsAfterTime", 5);     }      public void SetNotificationMinimapLocation (Notification notification) {
                 // As all notifications are relevant now         SetRelevantNotificationLocation(notification);     }
    #endregion

    #region Private Functions     private IEnumerator LookForMergableNotificartionsAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        foreach (MainNotification foundMainNotif in markersContainer.GetComponentsInChildren<MainNotification>())
        {
            FindNearbyMarkers(foundMainNotif);
        }
    }

    private void FindNearbyMarkers(MainNotification mainNotif)
    {
        foreach (MainNotification foundMainNotif in markersContainer.GetComponentsInChildren<MainNotification>()) {
            if (mainNotif != foundMainNotif && (CalculateDifference(mainNotif.MinimapLocation.x, foundMainNotif.MinimapLocation.x) < 120) && (CalculateDifference(mainNotif.MinimapLocation.y, foundMainNotif.MinimapLocation.y) < 120))
            {
                Debug.Log("Difference x,y: " + CalculateDifference(mainNotif.MinimapLocation.x, foundMainNotif.MinimapLocation.x) + ", " + CalculateDifference(mainNotif.MinimapLocation.y, foundMainNotif.MinimapLocation.y));

                CreateMarker(CombineMainNotifications(mainNotif, foundMainNotif));

                DeleteSpecifiqMarker(mainNotif.MinimapLocation);
                DeleteSpecifiqMarker(foundMainNotif.MinimapLocation);
                break;
            }

        }
    } 
    private MainNotification CombineMainNotifications(MainNotification mainNotif1, MainNotification mainNotif2) {
        // Create marker first then get the component to fix warning
        MainNotification CombinedMainNotif = new MainNotification();
        float x = (mainNotif1.MinimapLocation.x + mainNotif2.MinimapLocation.x) / 2;
        float y = (mainNotif1.MinimapLocation.y + mainNotif2.MinimapLocation.y) / 2;

        CombinedMainNotif.MinimapLocation = new Vector2(x, y);
        CombinedMainNotif.keyNote = mainNotif1.keyNote += mainNotif2.keyNote;

        foreach(Notification notif in mainNotif1.notifications)
            CombinedMainNotif.notifications.Add(notif);

        foreach (Notification notif in mainNotif2.notifications)
            CombinedMainNotif.notifications.Add(notif);

        return CombinedMainNotif;
    }

    private float CalculateDifference(float nr1, float nr2) {
        return System.Math.Abs(nr1 - nr2);
    }      private void SetRelevantNotificationLocation(Notification notification) {
        notification.MinimapLocation = locationSync.GetSuspectMinimapLocation() + Random.insideUnitCircle * 100;     }      private void CreateMarker(Notification notif) {         GameObject marker = Instantiate(MarkerPrefab) as GameObject;         marker.SetActive(true);         marker.transform.SetParent(markersContainer.transform, false);          // Set marker on correct location         marker.transform.localPosition = notif.MinimapLocation;          // Set up main notification data         SetUpMainNotification(marker, notif);     }

    private void CreateMarker(MainNotification mainNotif)
    {
        GameObject marker = Instantiate(MarkerPrefab) as GameObject;
        marker.SetActive(true);
        marker.transform.SetParent(markersContainer.transform, false);

        // Set marker on correct location
        marker.transform.localPosition = mainNotif.MinimapLocation;

        MainNotification markerMainNoftif = marker.GetComponent<MainNotification>();
        markerMainNoftif = mainNotif;
    }

    private void DeleteSpecifiqMarker(Vector2 minimapLocation)
    {
        // Begin at 2 as the first two items are the playerIcon and the marker prefab
        foreach(Transform marker in markersContainer.GetComponentInChildren<Transform>())
        {
            if (minimapLocation == (Vector2)marker.localPosition) {
                Destroy(marker.gameObject);
                break;
            }
        }
    }      private void SetUpMainNotification(GameObject marker, Notification notif) {
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
    }

    // Not needed for now, perhaps in the future
    private void SetIrrelevantNotificationLocation(Notification notification)
    {         float corX = Random.Range(minRangeX, maxRangeX);         float corY = Random.Range(minRangeY, maxRangeY);         notification.MinimapLocation = new Vector2(corX, corY);     }
    #endregion 
} 