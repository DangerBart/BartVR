using UnityEngine;

public class MultiLinkedList : MonoBehaviour {
	Node head = null;

	public void Add(int index, Transform data, Node[] options) {
		if (head == null || index == 0) {
			head = new Node(data, options);
		} else {
			int i = 1;
			Node current = head;
			while(i < index) {
				//find index, place node and update options of nodes
			}
		}
	}

	public class Node {
		Transform data;
		Node[] options;

		public Node(Transform data, Node[] options) {
			this.data = data;
			this.options = options;
		}

		public void SetOption(int index, Node node) {
			this.options[index] = node;
		}

		public Node GetOption(int index) {
			return this.options[index];
		}
	}
}
