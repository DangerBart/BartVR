using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    Transform data;
    public Node[] options = new Node[100];//Update to list

    public Node(Transform data) {
        this.data = data;
    }

    public Node(Transform data, Node[] options) {
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
        int i = 0;
        foreach (Node node in nodes) {
            SetOption(node, i);
            i++;
        }
    }
}
