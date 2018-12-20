using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NotificationPanel : MonoBehaviour, IPointerClickHandler
{
    //image needs to be public to be able to clone
    public Sprite image;
    public GameObject panelImage;
    public GameObject notificationMenu;
    public GameObject Timestamp;

    private Notification notification;
    public TabletDisplay tablet;
    public bool isFavorite;
    private Text username;
    private Text message;
    private Image mediaPlaform;
    private Sprite imageForVR;
    private GameObject imageButton;
    private Image favoriteButton;
    private Text Date;

    private Color panelColorWhite = new Color32(255, 255, 255, 255);
    private Color panelColorYellow = new Color32(255, 255, 0, 180);

    public void Setup(Notification notification) {
        SetGameObjects();
        SetComponents();
        this.notification = notification;
        SetupPanelInformation(notification);
    }

    public Vector2 GetMinimapLocation() {
        return notification.MinimapLocation;
    }

    public bool IsFavorite() {
        return notification.IsFavorite;
    }

    public bool IsSelected() {
        return notification.IsSelected;
    }

    private void SetGameObjects() {
        imageButton = this.transform.Find("Show Button").gameObject;
    }

    private void SetComponents() {
        username = this.transform.Find("UserName").GetComponent<Text>();
        message = this.transform.Find("Message").GetComponent<Text>();
        mediaPlaform = this.transform.Find("MediaPlatform").GetComponent<Image>();
        favoriteButton = this.transform.Find("Favorite Button").GetComponent<Image>();
        Date = this.transform.Find("Date").GetComponent<Text>();
    }

    private void SetupPanelInformation(Notification notification) {
        username.text = notification.Name;
        message.text = notification.Message;
        mediaPlaform.sprite = notification.PlatformLogo;
        SetImage(notification.Img);
        SetTime();

        this.notification = notification;
    }

    public void SetImage(Sprite img) {
        if(img != null) {
            this.image = img;
            imageButton.SetActive(true);
        }
    }

    public void SetTime() {
        Text date = Timestamp.transform.Find("Date").GetComponent<Text>();
        Text time = Timestamp.transform.Find("Time").GetComponent<Text>();
        Date.text = string.Format("{0} {1}", time.text, date.text);
    }

    public void ShowImage() {
        panelImage.GetComponent<Image>().sprite = image;
    }

    public void ToggleFavoriteButton() {
        if (notification.IsFavorite) {
            notification.IsFavorite = false;
            favoriteButton.sprite = Resources.Load<Sprite>("Notification/EmptyStar");
        } else {
            notification.IsFavorite = true;
            favoriteButton.sprite = Resources.Load<Sprite>("Notification/FilledStar");
        }

        notificationMenu.GetComponent<NotificationControl>().ToggleFavoritePanel(gameObject, notification);
        DeletePanel();
    }
    public void SendImageToVRUser(){
        tablet.SetImage(panelImage.GetComponent<Image>().sprite);
    }

    private void DeletePanel() {
        Destroy(gameObject);
    }

    public void DeleteButtonClicked() {
        if (notification.IsFavorite || notification.IsSelected) {
            // Make sure the marker gets deleted as well
            notificationMenu.GetComponent<NotificationControl>().NotificationPanelRemoved(notification.MinimapLocation);
        }

        DeletePanel();
    }

    public void OnPointerClick(PointerEventData eventData) {
        // Detected click on panel
        notificationMenu.GetComponent<NotificationControl>().NotificationSelected(gameObject);
    }

    public void TogglePanelColor() {
        Image panel = gameObject.GetComponent<Image>();

        if (!notification.IsSelected) {
            panel.color = panelColorYellow;
        } else {
            panel.color = panelColorWhite;
        }

        // Set IsSelected value
        notification.IsSelected = !notification.IsSelected;
    }
}
