﻿using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MinimapControl : MonoBehaviour {

    // Viarables set in Unity Editor
    [SerializeField]
    private GameObject MarkerPrefab;
    [SerializeField]
    private GameObject markersContainer;
    [SerializeField]
    private GameObject plane;
    [SerializeField]
    private int MergeDistance;
    [SerializeField]
    private int RadiousAroundSuspect;

    // Private viarables
    private GameObject minimap;
    private LocationSync locationSync;
    private GameObject lastAddedMarker;
    private NotificationControl notificationControl;

    private float xScale;
    private float yScale;

    private float minRangeX;
    private float maxRangeX;
    private float minRangeY;
    private float maxRangeY;

    // Use this for initialization
    void Start() {
        minimap = transform.gameObject;
        Setup();
    }

    #region Public Functions
    public void InitiateNotificationOnMinimap(Notification notification) {

        // Set location of marker near suspect
        SetRelevantNotificationLocation(notification);
        MainNotification MainNotification = ConvertNotificationToMainNotification(notification);
        lastAddedMarker = CreateMarker(MainNotification);

        // Coroutine
        StartCoroutine("LookForMergableNotificationsAfterTime", 5);
    }

    public void DeselectMarkersExcept(MainNotification notif) {
        foreach (MainNotification foundMainNotif in markersContainer.GetComponentsInChildren<MainNotification>()) {
            if (foundMainNotif != notif)
                foundMainNotif.GetComponent<Marker>().SetSelected(false);
        }
    }
    #endregion

    #region Private Functions
    private IEnumerator LookForMergableNotificationsAfterTime(float time) {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        FindNearbyMarkersAndCombine(lastAddedMarker.GetComponent<MainNotification>());
    }

    private void FindNearbyMarkersAndCombine(MainNotification mainNotif) {
        MainNotification lastAddedMarkerBeforeLooking = mainNotif;

        foreach (MainNotification foundMainNotif in markersContainer.GetComponentsInChildren<MainNotification>()) {

            if (mainNotif != foundMainNotif && CloseEnoughToEachOther(mainNotif.MinimapLocation, foundMainNotif.MinimapLocation, MergeDistance)) {
                // Found notifications close enough to each other
                MainNotification combinedMainNotif = CombineMainNotifications(mainNotif, foundMainNotif);

                // Update marker location and update latest marker
                lastAddedMarker = CreateMarker(combinedMainNotif);
                bool selected = (mainNotif.GetComponent<Marker>().GetSelected() || foundMainNotif.GetComponent<Marker>().GetSelected());
                lastAddedMarker.GetComponent<Marker>().SetSelected(selected);

                // Delete old markers
                DeleteSpecifiqMarker(mainNotif.MinimapLocation);
                DeleteSpecifiqMarker(foundMainNotif.MinimapLocation);

                // Let control know a selected marker was merged
                if(selected)
                    notificationControl.SelecedMarkerMerged(lastAddedMarker);

                break;
            }
        }

        // Search again when notifications were merged
        if (lastAddedMarkerBeforeLooking != lastAddedMarker.GetComponent<MainNotification>())
            StartCoroutine("LookForMergableNotificationsAfterTime", 2);
    }

    private void SetRelevantNotificationLocation(Notification notification) {
        notification.MinimapLocation = locationSync.GetSuspectMinimapLocation() + Random.insideUnitCircle * RadiousAroundSuspect;
    }

    private GameObject CreateMarker(MainNotification mainNotif) {
        GameObject marker = Instantiate(MarkerPrefab) as GameObject;
        marker.SetActive(true);
        marker.transform.SetParent(markersContainer.transform, false);

        // Set marker on correct location
        if (GameManager.currentMode == PlayingMode.MultiplayerAM || GameManager.currentMode == PlayingMode.MultiplayerBM)
            mainNotif.MinimapLocation = new Vector2(mainNotif.MinimapLocation.x, mainNotif.MinimapLocation.y);
        else
            mainNotif.MinimapLocation = new Vector2(mainNotif.MinimapLocation.x, mainNotif.MinimapLocation.y / 2);

        marker.transform.localPosition = mainNotif.MinimapLocation;
        MainNotification markerMainNotif = marker.GetComponent<MainNotification>();

        // Copy values
        markerMainNotif.keyNote = mainNotif.keyNote;
        markerMainNotif.MinimapLocation = mainNotif.MinimapLocation;
        markerMainNotif.notifications = mainNotif.notifications;
        markerMainNotif.timeLatestNotification = mainNotif.timeLatestNotification;

        // Only excuted in singleplayer mode
        if (GameManager.currentMode == PlayingMode.Singleplayer) {
            marker.transform.Find("MarkerPanel").transform.Find("KeyNote").GetComponent<Text>().text = mainNotif.keyNote;
            marker.transform.Find("MarkerPanel").transform.Find("TimeText").GetComponent<Text>().text = mainNotif.timeLatestNotification.ToString("HH:mm");
            var rt = marker.transform.Find("MarkerPanel").GetComponent<RectTransform>();

            // Change location of panel to always be visible 
            float x = rt.localPosition.x;
            float y = rt.localPosition.y;
            float marginX = gameObject.GetComponent<RectTransform>().sizeDelta.x - rt.transform.GetComponent<RectTransform>().sizeDelta.x;
            float marginY = gameObject.GetComponent<RectTransform>().sizeDelta.y - rt.transform.GetComponent<RectTransform>().sizeDelta.y;

            if (mainNotif.MinimapLocation.x < -(marginX/2))
                x += (rt.transform.GetComponent<RectTransform>().sizeDelta.x / 2 - marker.transform.GetComponent<RectTransform>().sizeDelta.x * 2);
            else if (mainNotif.MinimapLocation.x > marginX)
                x += -(rt.transform.GetComponent<RectTransform>().sizeDelta.x / 2 - marker.transform.GetComponent<RectTransform>().sizeDelta.x * 2);

            if (mainNotif.MinimapLocation.y > 0)
                y += -rt.transform.GetComponent<RectTransform>().sizeDelta.y;

            Vector2 v2 = new Vector2(x, y);
            rt.localPosition = v2;
        }

        return marker.gameObject;
    }

    private bool CloseEnoughToEachOther(Vector2 v1, Vector2 v2, int maxDisbtance) {
        float differenceX = Math.Abs(v1.x - v2.x);
        float differenceY = Math.Abs(v1.y - v2.y);

        return (differenceX <= maxDisbtance && differenceY <= maxDisbtance);
    }

    private MainNotification ConvertNotificationToMainNotification(Notification notif) {
        MainNotification mainNotif = new MainNotification {
            keyNote = GetKeyNotes(notif.Message),
            MinimapLocation = notif.MinimapLocation
        };

        mainNotif.notifications.Add(notif);
        mainNotif.timeLatestNotification = DateTime.Parse(notif.PostTime, System.Globalization.CultureInfo.CurrentCulture);
        return mainNotif;
    }

    private MainNotification CombineMainNotifications(MainNotification mainNotif1, MainNotification mainNotif2) {
        // Create marker first then get the component to fix warning
        MainNotification CombinedMainNotif = new MainNotification();
        float x = (mainNotif1.MinimapLocation.x + mainNotif2.MinimapLocation.x) / 2;
        float y = (mainNotif1.MinimapLocation.y + mainNotif2.MinimapLocation.y) / 2;

        CombinedMainNotif.MinimapLocation = new Vector2(x, y);
        CombinedMainNotif.keyNote = mainNotif1.keyNote += mainNotif2.keyNote;

        foreach (Notification notif in mainNotif1.notifications)
            CombinedMainNotif.notifications.Add(notif);

        foreach (Notification notif in mainNotif2.notifications)
            CombinedMainNotif.notifications.Add(notif);

        CombinedMainNotif.timeLatestNotification = GetLatestTime(mainNotif1.timeLatestNotification, mainNotif2.timeLatestNotification);

        return CombinedMainNotif;
    }

    private void DeleteSpecifiqMarker(Vector2 minimapLocation) {
        foreach (Transform marker in markersContainer.GetComponentInChildren<Transform>()) {

            if (minimapLocation == (Vector2)marker.localPosition) {

                Destroy(marker.gameObject);
                break;
            }
        }
    }

    private void SetUpMainNotification(GameObject marker, Notification notif) {
        MainNotification mainNotif = marker.GetComponent<MainNotification>();
        mainNotif.MinimapLocation = notif.MinimapLocation;
        mainNotif.keyNote = GetKeyNotes(notif.Message);
        mainNotif.notifications.Add(notif);
    }

    private string GetKeyNotes(string message) {
        //ToDo Temporary
        string[] words = GetWords(message);
        string[] removeList = { "ik", "hij", "dat", "wat", "de", "het", "heb", "een", "op", "onder", "over", "met", "naast", "is", "werd", "van", "is", "hier", "daar"};
        string validMessage = "";

        foreach (string word in words) {
            var q = removeList.Any(w => word.ToLower().Equals(w));
            if (!q)
                validMessage += word.ToLower() + " ";
        }
        return validMessage;
    }

    private string[] GetWords(string input) {
        MatchCollection matches = Regex.Matches(input, @"\b[\w']*\b");

        var words = from m in matches.Cast<Match>()
                    where !string.IsNullOrEmpty(m.Value)
                    select TrimSuffix(m.Value);

        return words.ToArray();
    }

    private string TrimSuffix(string word) {
        int apostropheLocation = word.IndexOf('\'');
        if (apostropheLocation != -1) {
            word = word.Substring(0, apostropheLocation);
        }

        return word;
    }

    private DateTime GetLatestTime(DateTime t1, DateTime t2) {
        int i = DateTime.Compare(t1, t2);

        if (i > 0)
            return t1;
      
        return t2;
    }

    private void Setup() {
        locationSync = GetComponent<LocationSync>();
        notificationControl = FindObjectOfType<NotificationControl>();
        CalculateScale();
        CalculateBoundries();
    }

    private void CalculateScale() {
        float offsetX = 1.7f;
        float offsetY = 2f;

        Vector2 mapSize = minimap.GetComponent<RectTransform>().sizeDelta;
        Vector2 planeSize = plane.GetComponent<RectTransform>().sizeDelta;
        xScale = planeSize.x / mapSize.x + offsetX;
        yScale = planeSize.y / mapSize.y + offsetY;
    }

    private void CalculateBoundries() {
        float saveMargin = 100f;

        minRangeX = (minimap.GetComponent<RectTransform>().sizeDelta.x / 2 * -1) + saveMargin;
        maxRangeX = (minimap.GetComponent<RectTransform>().sizeDelta.x / 2) - saveMargin;
        minRangeY = (minimap.GetComponent<RectTransform>().sizeDelta.y / 2 * -1) + saveMargin;
        maxRangeY = (minimap.GetComponent<RectTransform>().sizeDelta.y / 2) - saveMargin;
    }

    // Not needed for now, perhaps in the future
    private void SetIrrelevantNotificationLocation(Notification notification) {
        float corX = Random.Range(minRangeX, maxRangeX);
        float corY = Random.Range(minRangeY, maxRangeY);
        notification.MinimapLocation = new Vector2(corX, corY);
    }
    #endregion
}