using System.Collections.Generic;
using UnityEngine;

public class Node {
    private Transform data;
    private List<Node> options = new List<Node>();

    public Node(Transform data) {
        this.data = data;
    }

    public Node(Transform data, List<Node> options) {
        this.data = data;
        this.options = options;
    }

    public Transform GetTransformData() {
        return data;
    }

    public Node GetOption(int index) {
        return options[index];
    }

    public int GetLength() {
        int length = 0;
        foreach (Node n in options) {
            length++;
        }
        return length;
    }

    public void SetOption(Node node, int index) {
        options[index] = node;
    }

    public void SetOptions(Node[] nodes) {
        foreach (Node node in nodes) {
            options.Add(node);
        }
    }
}
