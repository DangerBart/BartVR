using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NotificationPanel : MonoBehaviour, IPointerClickHandler
{
    //image needs to be public to be able to clone
    public Sprite image;
    public GameObject panelImage;
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

    public void Setup(Notification notification, KindOfNotification kind) {
        SetGameObjects();
        SetComponents(kind);
        this.notification = notification;
        SetupPanelInformation(notification, kind);
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
        imageButton = transform.Find("Show Button").gameObject;
    }

    private void SetComponents(KindOfNotification kind) {
        // General settings
        username = transform.Find("UserName").GetComponent<Text>();
        message = transform.Find("Message").GetComponent<Text>();
        mediaPlaform = transform.Find("MediaPlatform").GetComponent<Image>();

        // Specifiq to kind of panel
        if (kind == KindOfNotification.Relevant || kind == KindOfNotification.Irrelevant)
        {
            favoriteButton = this.transform.Find("Favorite Button").GetComponent<Image>();
            Date = this.transform.Find("Date").GetComponent<Text>();
        }
    }

    private void SetupPanelInformation(Notification notification, KindOfNotification kind) {
        // General settings
        this.notification = notification;
        username.text = notification.Autor;
        message.text = notification.Message;
        mediaPlaform.sprite = notification.PlatformLogo;

        if (kind == KindOfNotification.Relevant || kind == KindOfNotification.Irrelevant)
        {
            SetImage(notification.Img);
            SetTime();
        } else {
            // Postable notification
            username.text = "Politie ✔";
            notification.Autor = "Politie ✔"; ;
        }
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
        DeletePanel();
    }

    public void SendImageToVRUser() {
        tablet.SetImage(panelImage.GetComponent<Image>().sprite);
    }

    private void DeletePanel() {
        Destroy(gameObject);
    }

    public void DeleteButtonClicked() {
        DeletePanel();
    }

    public void OnPointerClick(PointerEventData eventData) {
        // Detected click on panel
        GetComponentInParent<NotificationControl>().NotificationSelected(gameObject);
    }

    public void PostButtonClicked()
    {
        Debug.Log("Post button was clicked");
        GetComponentInParent<Board>().SetNotificationWaitingForPost(false, notification.Id);
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
