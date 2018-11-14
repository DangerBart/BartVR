using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCBehaviour : MonoBehaviour {

    public GameObject checkpointContainer;

    private Node target = null;
    private Node prevCheckpoint;
    private Node[] list = new Node[8];
    private NavMeshAgent agent = new NavMeshAgent();
    private int randX, randZ, timeout;

    // Use this for initialization
    void Start() {
        #region Checkpoint node initialization
        /*
        List goes by checkpoint index number in CheckpointContainer parent, so the GameObject named Checkpoint (3) is equal to node3 and GetCheckpointChild(3)
        */
        Node node0 = new Node(GetCheckpointChild(0));
        Node node1 = new Node(GetCheckpointChild(1));
        Node node2 = new Node(GetCheckpointChild(2));
        Node node3 = new Node(GetCheckpointChild(3));
        Node node4 = new Node(GetCheckpointChild(4));
        Node node5 = new Node(GetCheckpointChild(5));
        Node node6 = new Node(GetCheckpointChild(6));
        Node node7 = new Node(GetCheckpointChild(7));

        node0.SetOptions(new Node[] { node1, node3, node6 });
        node1.SetOptions(new Node[] { node0, node2, node7 });
        node2.SetOptions(new Node[] { node1, node3 });
        node3.SetOptions(new Node[] { node0, node2, node4 });
        node4.SetOptions(new Node[] { node3, node5 });
        node5.SetOptions(new Node[] { node4 });
        node6.SetOptions(new Node[] { node0, node7 });
        node7.SetOptions(new Node[] { node1, node6 });

        list[0] = node0;
        list[1] = node1;
        list[2] = node2;
        list[3] = node3;
        list[4] = node4;
        list[5] = node5;
        list[6] = node6;
        list[7] = node7;
        #endregion
        int random = Random.Range(0, list.Length - 1);
        agent = GetComponent<NavMeshAgent>();

        // Test
        PrintDitto(list);

        this.agent.Warp(list[random].GetTransformData().position);
        prevCheckpoint = list[random];
    }

    // Update is called once per frame
    void Update() {
        int rand;
        if (target == null) {
            randX = (int)Random.Range(0, 2.5f);
            randZ = (int)Random.Range(0, 2.5f);
            rand = Random.Range(0, prevCheckpoint.GetLength());
            target = prevCheckpoint.GetOption(rand);
            agent.SetDestination(target.GetTransformData().position + new Vector3(randX, 0, randZ));
            FaceTarget();
        }
        if (this.transform.position.x == (target.GetTransformData().position.x + randX) && this.transform.position.z == (target.GetTransformData().position.z + randZ)) {
            prevCheckpoint = target;
            target = null;
            timeout = 0;
        }
        timeout++;
        if(timeout > 750) {
            prevCheckpoint = target;
            target = null;
            timeout = 0;
        }
    }

    private Transform GetCheckpointChild(int index) {
        return checkpointContainer.gameObject.transform.GetChild(index);
    }

    public void PrintDitto(Node[] nodes) {
        foreach (Node node in nodes) {
            Debug.Log(node.GetTransformData().gameObject.name + " is connected to " + node.GetLength());
        }
    }

    void FaceTarget() {
        Vector3 direction = (target.GetTransformData().position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
