using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NotificationPanel : MonoBehaviour, IPointerClickHandler
{
    // Image needs to be public to be able to clone
    public Sprite image;
    public GameObject panelImage;

    private DoublyLinkedList notification;
    public TabletDisplay tablet;
    private Text username;
    private Text message;
    private Image mediaPlaform;
    private GameObject imageButton;
    private Text Date;

    private Color panelColorWhite = new Color32(255, 255, 255, 255);
    private Color panelColorYellow = new Color32(255, 255, 0, 180);

    void Start() {
    }

    public void Setup(DoublyLinkedList notification, KindOfNotification kind) {
        notification.GetData().Kind = kind;

        SetGameObjects();
        SetComponents(kind);
        this.notification = notification;

        if (notification.HasPrevious())
            SetupPanelInformation(notification.GetData(), kind, notification.GetPrevious().GetData().Autor);
        else
            SetupPanelInformation(notification.GetData(), kind);
    }

    public void Setup(Notification notif, KindOfNotification kind = KindOfNotification.Relevant) {
        SetGameObjects();
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

    private void SetGameObjects() {
        imageButton = transform.Find("Show Button").gameObject;
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
        if(img != null) {
            image = img;
            imageButton.SetActive(true);
        }
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

    }

    public void PostButtonClicked() {
        GetComponentInParent<NotificationControl>().CreateRelevantMessagePanel(notification);
        GetComponentInParent<Board>().SetNotificationWaitingForPost(false, notification.GetNext().GetData().Id);
        DeletePanel();
    }

    public void TogglePanelColor() {
        Image panel = gameObject.GetComponent<Image>();

        if (!notification.GetData().IsSelected)
            panel.color = panelColorYellow;
        else
            panel.color = panelColorWhite;

        notification.GetData().IsSelected = !notification.GetData().IsSelected;
    }
}
