using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour {

    [SerializeField]
    private Text name;

    [SerializeField]
    private Text message;

    [SerializeField]
    private Image mediaPlaform;

    [SerializeField]
    private Sprite image;
    public GameObject panelImage;
    public GameObject imageButton;
    public GameObject favoriteButton;
    public GameObject NotificationMenu;
    public bool isFavorite;
    
    public void SetName(string name) {
        this.name.text = name;
    }

    public void SetMessage(string message) {
        this.message.text = message;
    }

    public void SetMediaPlatform(Sprite image) {
        this.mediaPlaform.sprite = image;
    }

    public void SetImage(Sprite img) {
        if(img != null) {
            this.image = img;
            imageButton.SetActive(true);

        }
        else {
            imageButton.SetActive(false);
        }
    }
    public void ShowImage() {
        panelImage.GetComponent<Image>().sprite = image;
    }
    public void ToggleFavoriteButton(){
        if(isFavorite){
            isFavorite = false;
            favoriteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/EmptyStar");
        }
        else{
            isFavorite = true;
            favoriteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/FilledStar");
        }
        NotificationMenu.GetComponent<NotificationControl>().ToggleFavoritePanel(gameObject, isFavorite);
        DeletePanel();
    }
    public void DeletePanel(){
        Destroy(gameObject);
    }
}
