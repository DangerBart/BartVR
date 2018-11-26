using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationButton : MonoBehaviour {

    [SerializeField]
    private Text name;

    [SerializeField]
    private Text message;

    public void SetName(string name)
    {
        this.name.text = name;
    }
}
