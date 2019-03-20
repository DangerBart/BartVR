using UnityEngine;

class DLinkedList
{
    private Notification data;
    private DLinkedList next;
    private DLinkedList prev;

    public DLinkedList() {
        data = new Notification();
        next = null;
        prev = null;
    }

    public DLinkedList(Notification value) {
        data = value;
        next = null;
        prev = null;
    }

    public DLinkedList InsertNext(Notification notif) {
        DLinkedList node = new DLinkedList(notif);
        if (this.next == null) {
            // Easy to handle
            node.prev = this;
            node.next = null; // already set in constructor
            this.next = node;
        } else {
            // Insert in the middle
            DLinkedList temp = this.next;
            node.prev = this;
            node.next = temp;
            this.next = node;
            temp.prev = node;
            // temp.next does not have to be changed
        }
        return node;
    }

    public bool FindAndInsertByNotificationId(Notification notif) {
        return FindAndInsertByNotificationId(this, notif);
    }

    public bool FindAndInsertByNotificationId(DLinkedList node, Notification notif) {
        if (node == null)
            node = this;

        Debug.Log("Traversing in Forward Direction");

        while ((node != null) && (node.data.Id != notif.ReactionTo)) {
            Debug.Log(node.data.Id);
            node = node.next;
        }

        if (node != null) {
            Debug.Log("Found Notif");
            node.InsertNext(notif);
            return true;
        }

        return false;
    }

    public DLinkedList InsertPrev(Notification notif)
    {
        DLinkedList node = new DLinkedList(notif);

        if (this.prev == null) {
            node.prev = null; // already set on constructor
            node.next = this;
            this.prev = node;
        } else {
            // Insert in the middle
            DLinkedList temp = this.prev;
            node.prev = temp;
            node.next = this;
            this.prev = node;
            temp.next = node;
            // temp.prev does not have to be changed
        }

        return node;
    }

    public void TraverseFront() {
        TraverseFront(this);
    }

    public void TraverseFront(DLinkedList node) {
        if (node == null)
            node = this;

        string toPrint = "";

        while (node != null) {
            toPrint += node.data.Id + ", ";
            node = node.next;
        }
        Debug.Log(toPrint);
    }

    public void TraverseBack() {
        TraverseBack(this);
    }

    public void TraverseBack(DLinkedList node) {
        if (node == null)
            node = this;

        Debug.Log("Traversing in Backward Direction");

        while (node != null) {
            Debug.Log(node.data);
            node = node.prev;
        }
    }
}
