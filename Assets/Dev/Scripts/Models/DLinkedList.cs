public class DLinkedList
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

    public bool FindAndInsertByNotificationId(Notification notif) {
        return FindAndInsertByNotificationId(this, notif);
    }

    public bool FindAndInsertByNotificationId(DLinkedList node, Notification notif) {
        if (node == null)
            node = this;

        while ((node != null) && (node.data.Id != notif.ReactionTo))
            node = node.next;

        if (node != null) {
            // Postable messages are made by professionals
            if (node.GetData().Postable)
                node.GetData().Autor = "Politie ✔";

            node.InsertNext(notif);
            return true;
        }

        return false;
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

    public Notification GetData() {
        return data;
    }

    public DLinkedList GetNext() {
        return next;
    }

    public bool HasNext() {
        return next != null;
    }

    public DLinkedList InsertPrev(Notification notif) {
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
}
