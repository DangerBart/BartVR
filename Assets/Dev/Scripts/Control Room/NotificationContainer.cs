using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("NotificationCollection")]
public class NotificationContainer {
    [XmlArray("Notifications")]
    [XmlArrayItem("Notification")]
    public List<Notification> notifications = new List<Notification>();

    public static NotificationContainer Load(string path) {
        TextAsset _xml = Resources.Load<TextAsset>(path);
        XmlSerializer serializer = new XmlSerializer(typeof(NotificationContainer));
        StringReader reader = new StringReader(_xml.text);
        NotificationContainer notifications = serializer.Deserialize(reader) as NotificationContainer;
        reader.Close();
        return notifications;
    }
}
