using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageListContext : MonoBehaviour {


    [SerializeField]
    private GameObject buttonTemplate;

    void Start() 
    {

        for (int i = 1; i < 20; i ++) 
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<MessageListButton>().SetText("Button nr." + i);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

}
