using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NotificationPanel : MonoBehaviour, IPointerClickHandler
{
    // Image needs to be public to be able to clone
    public Sprite image;
    public GameObject panelImage;
    public TabletDisplay tablet;

    private DoublyLinkedList notification;
    private KindOfNotification kindOfNotification;
    private Text username;
    private Text message;
    private Image mediaPlaform;
    private Text Date;

    private Color panelColorWhite = new Color32(255, 255, 255, 255);
    private Color panelColorPurple = new Color32(240, 230, 245, 255);

    public void Setup(DoublyLinkedList notification, KindOfNotification kind) {
        kindOfNotification = kind;
        SetComponents(kind);
        this.notification = notification;

        if (notification.HasPrevious())
            SetupPanelInformation(notification.GetData(), kind, notification.GetPrevious().GetData().Autor);
        else
            SetupPanelInformation(notification.GetData(), kind);
    }

    public void Setup(Notification notif, KindOfNotification kind = KindOfNotification.Relevant) {
        kindOfNotification = kind;
        SetComponents(kind);
        SetupPanelInformation(notif, KindOfNotification.Relevant);
    }

    // Getters
    public Vector2 GetMinimapLocation() {
        return notification.GetData().MinimapLocation;
    }

    public bool IsFavorite() {
        return notification.GetData().IsFavorite;
    }

    public bool IsSelected() {
        return notification.GetData().IsSelected;
    }

    public DoublyLinkedList GetNotification() {
        return notification;
    }

    // Setup functions
    private void SetComponents(KindOfNotification kind) {
        // General settings
        username = transform.Find("UserName").GetComponent<Text>();
        message = transform.Find("Message").GetComponent<Text>();
        mediaPlaform = transform.Find("MediaPlatform").GetComponent<Image>();

        // Specifiq to kind of panel
        if (kind != KindOfNotification.Postable){
            Date = this.transform.Find("Date").GetComponent<Text>();
        }
    }

    private void SetupPanelInformation(Notification notif, KindOfNotification kind, string reactionTo = "") {
        // General settings
        username.text = notif.Autor;
        if (reactionTo != string.Empty)
            username.text += " ↳ Reactie op " + reactionTo;

        message.text = notif.Message;
        mediaPlaform.sprite = notif.PlatformLogo;

        if (kind != KindOfNotification.Postable) {
            Date.text = notif.PostTime;
            SetImage(notif.Img);
        }
    }

    public void SetImage(Sprite img) {
        if(img != null)
            image = img;
    }

    // Event functions
    public void ShowImage() {
        panelImage.GetComponent<Image>().sprite = image;
    }

    public void SendImageToVRUser() {
        tablet.SetImage(panelImage.GetComponent<Image>().sprite);
    }

    public void DeletePanel() {
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData) {
        // Detected click on panel
        if(kindOfNotification == KindOfNotification.Postable) {
            GetComponentInParent<NotificationControl>().PostedNotificationPanelClicked(notification.GetData().Id);
            SetPanelColor(true);
        }
    }

    public void PostButtonClicked() {
        GetComponentInParent<NotificationControl>().CreateRelevantMessagePanel(notification);
        GetComponentInParent<Board>().SetNotificationWaitingForPost(false, notification.GetNext().GetData().Id);
        GetComponentInParent<NotificationControl>().PostedNotificationPanelClicked(notification.GetData().Id);
        transform.Find("PostedMessage").gameObject.SetActive(true);

        // Delete buttons
        Destroy(transform.Find("PostButton").gameObject);
        Destroy(transform.Find("NotPostButton").gameObject);
    }

    public void SetPanelColor(bool selected) {
        Image panel = gameObject.GetComponent<Image>();

        if (selected)
            panel.color = panelColorPurple;
        else
            panel.color = panelColorWhite;
    }
}
