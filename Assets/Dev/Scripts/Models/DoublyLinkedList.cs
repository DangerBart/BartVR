public class DoublyLinkedList {
    private Notification data;
    private DoublyLinkedList next;
    private DoublyLinkedList prev;

    public DoublyLinkedList() {
        data = new Notification();
        next = null;
        prev = null;
    }

    public DoublyLinkedList(Notification value) {
        data = value;
        next = null;
        prev = null;
    }

    public bool FindAndInsertByNotificationId(Notification notif) {
        DoublyLinkedList node = this;

        while ((node != null) && (node.data.Id != notif.ReactionTo))
            node = node.next;

        if (node != null) {
            // Postable messages are made by professionals
            if (node.GetData().Postable)
                node.GetData().Autor = "Politie ✔";

            notif.ReactionOfPostableNotif = IsReactionToPostableNotification(node);

            if (notif.ReactionOfPostableNotif) {
                notif.WaitingForPost = true;

                // Make sure reactions are from the same platform
                notif.Platform = node.GetData().Platform;
                notif.PlatformLogo = node.GetData().PlatformLogo;
            }

            node.InsertNext(notif);
            return true;
        }

        return false;
    }

    public DoublyLinkedList InsertNext(Notification notif) {
        DoublyLinkedList node = new DoublyLinkedList(notif);
        if (this.next == null) {
            node.prev = this;
            node.next = null; // already set in constructor
            this.next = node;
        } else {
            // Insert in the middle
            DoublyLinkedList temp = this.next;
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

    public DoublyLinkedList GetNext() {
        return next;
    }

    public bool HasNext() {
        return next != null;
    }

    public DoublyLinkedList GetPrevious() {
        return prev;
    }

    public bool HasPrevious() {
        return prev != null;
    }

    public DoublyLinkedList InsertPrev(Notification notif) {
        DoublyLinkedList node = new DoublyLinkedList(notif);

        if (this.prev == null) {
            node.prev = null; // already set on constructor
            node.next = this;
            this.prev = node;
        } else {
            // Insert in the middle
            DoublyLinkedList temp = this.prev;
            node.prev = temp;
            node.next = this;
            this.prev = node;
            temp.next = node;
            // temp.prev does not have to be changed
        }

        return node;
    }

    private bool IsReactionToPostableNotification(DoublyLinkedList notif) {
        DoublyLinkedList looptrough = notif;
        while(looptrough != null) {
            if (looptrough.GetData().Postable)
                return true;

            looptrough = looptrough.GetPrevious();
        }

        return false;
    }
}
