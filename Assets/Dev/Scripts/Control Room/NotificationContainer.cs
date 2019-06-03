using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

[XmlRoot("NotificationCollection")]
public class NotificationContainer
{
    [XmlArray("Notifications")]
    [XmlArrayItem("Notification")]
    public List<Notification> notifications = new List<Notification>();

    // Suspect info arrays
    private static string[] genders = { "man", "vrouw" };
    private static string[] colors = { "wit", "zwart", "grijs", "geel", "groen", "roze", "paars", "blauw", "bruin", "rood" };

    public static NotificationContainer Load(string path) {
        TextAsset _xml = Resources.Load<TextAsset>(path);
        XmlSerializer serializer = new XmlSerializer(typeof(NotificationContainer));
        StringReader reader = new StringReader(_xml.text);
        NotificationContainer notifications = serializer.Deserialize(reader) as NotificationContainer;
        reader.Close();
        notifications = ParseVariables(notifications);
        return notifications;
    }

    private static NotificationContainer ParseVariables(NotificationContainer notifications) {
        string match;
        foreach (Notification notif in notifications.notifications) {
            Match m = Regex.Match(notif.Message, @"\[(.*?)\]");
            match = m.Value;
            switch (match) {
                case "[geslacht]":
                    notif.Message = notif.Message.Replace(match, genders[(int)NPCMaker.suspectGender - 1]);
                    break;
                case "[bovenstuk]":
                    notif.Message = notif.Message.Replace(match, colors[(int)NPCMaker.suspectTopPiece - 1]);
                    break;
                case "[onderstuk]":
                    notif.Message = notif.Message.Replace(match, colors[(int)NPCMaker.suspectBottomPiece - 1]);
                    break;
            }
        }
        return notifications;
    }
}
