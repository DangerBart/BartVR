using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCBehaviour : MonoBehaviour {

    public GameObject checkpointContainer;

    private readonly float radius = 5.5f;

    private Node target;
    private Node prevCheckpoint;
    private Node[] list = new Node[59];
    private NavMeshAgent agent = new NavMeshAgent();
    private float randX, randZ;
    private int timeout;

    // Use this for initialization
    void Start() {
        #region Checkpoint node initialization
        /*
        List goes by checkpoint index number in CheckpointContainer parent, so the GameObject named Checkpoint (3) is equal to node3 and GetCheckpointChild(3)
        */

        //TODO Create three seperate functions
        Node node0 = new Node(GetCheckpointChild(0));
        Node node1 = new Node(GetCheckpointChild(1));
        Node node2 = new Node(GetCheckpointChild(2));
        Node node3 = new Node(GetCheckpointChild(3));
        Node node4 = new Node(GetCheckpointChild(4));
        Node node5 = new Node(GetCheckpointChild(5));
        Node node6 = new Node(GetCheckpointChild(6));
        Node node7 = new Node(GetCheckpointChild(7));
        Node node8 = new Node(GetCheckpointChild(8));
        Node node9 = new Node(GetCheckpointChild(9));
        Node node10 = new Node(GetCheckpointChild(10));
        Node node11 = new Node(GetCheckpointChild(11));
        Node node12 = new Node(GetCheckpointChild(12));
        Node node13 = new Node(GetCheckpointChild(13));
        Node node14 = new Node(GetCheckpointChild(14));
        Node node15 = new Node(GetCheckpointChild(15));
        Node node16 = new Node(GetCheckpointChild(16));
        Node node17 = new Node(GetCheckpointChild(17));
        Node node18 = new Node(GetCheckpointChild(18));
        Node node19 = new Node(GetCheckpointChild(19));
        Node node20 = new Node(GetCheckpointChild(20));
        Node node21 = new Node(GetCheckpointChild(21));
        Node node22 = new Node(GetCheckpointChild(22));
        Node node23 = new Node(GetCheckpointChild(23));
        Node node24 = new Node(GetCheckpointChild(24));
        Node node25 = new Node(GetCheckpointChild(25));
        Node node26 = new Node(GetCheckpointChild(26));
        Node node27 = new Node(GetCheckpointChild(27));
        Node node28 = new Node(GetCheckpointChild(28));
        Node node29 = new Node(GetCheckpointChild(29));
        Node node30 = new Node(GetCheckpointChild(30));
        Node node31 = new Node(GetCheckpointChild(31));
        Node node32 = new Node(GetCheckpointChild(32));
        Node node33 = new Node(GetCheckpointChild(33));
        Node node34 = new Node(GetCheckpointChild(34));
        Node node35 = new Node(GetCheckpointChild(35));
        Node node36 = new Node(GetCheckpointChild(36));
        Node node37 = new Node(GetCheckpointChild(37));
        Node node38 = new Node(GetCheckpointChild(38));
        Node node39 = new Node(GetCheckpointChild(39));
        Node node40 = new Node(GetCheckpointChild(40));
        Node node41 = new Node(GetCheckpointChild(41));
        Node node42 = new Node(GetCheckpointChild(42));
        Node node43 = new Node(GetCheckpointChild(43));
        Node node44 = new Node(GetCheckpointChild(44));
        Node node45 = new Node(GetCheckpointChild(45));
        Node node46 = new Node(GetCheckpointChild(46));
        Node node47 = new Node(GetCheckpointChild(47));
        Node node48 = new Node(GetCheckpointChild(48));
        Node node49 = new Node(GetCheckpointChild(49));
        Node node50 = new Node(GetCheckpointChild(50));
        Node node51 = new Node(GetCheckpointChild(51));
        Node node52 = new Node(GetCheckpointChild(52));
        Node node53 = new Node(GetCheckpointChild(53));
        Node node54 = new Node(GetCheckpointChild(54));
        Node node55 = new Node(GetCheckpointChild(55));
        Node node56 = new Node(GetCheckpointChild(56));
        Node node57 = new Node(GetCheckpointChild(57));
        Node node58 = new Node(GetCheckpointChild(58));

        node0.SetOptions(new Node[] { node1, node58 });
        node1.SetOptions(new Node[] { node0, node2, node52, node54, node55 });
        node2.SetOptions(new Node[] { node1, node3 });
        node3.SetOptions(new Node[] { node2, node4, node5 });
        node4.SetOptions(new Node[] { node3, node6 });
        node5.SetOptions(new Node[] { node3, node54, node20, node22 });
        node6.SetOptions(new Node[] { node4, node7, node16 });
        node7.SetOptions(new Node[] { node8, node6, node10 });
        node8.SetOptions(new Node[] { node7, node9 });
        node9.SetOptions(new Node[] { node8 });
        node10.SetOptions(new Node[] { node7, node11, node12 });
        node11.SetOptions(new Node[] { node10, node24 });
        node12.SetOptions(new Node[] { node10, node13, node15 });
        node13.SetOptions(new Node[] { node12 });
        node14.SetOptions(new Node[] { node15, node21, node20 });
        node15.SetOptions(new Node[] { node12, node14, node16 });
        node16.SetOptions(new Node[] { node15, node17, node19 });
        node17.SetOptions(new Node[] { node16, node18 });
        node18.SetOptions(new Node[] { node17, node19, node20 });
        node19.SetOptions(new Node[] { node16, node18 });
        node20.SetOptions(new Node[] { node14, node18, node22, node5, node54 });
        node21.SetOptions(new Node[] { node14, node22, node23, node24 });
        node22.SetOptions(new Node[] { node20, node21, node5 });
        node23.SetOptions(new Node[] { node21, node29, node27 });
        node24.SetOptions(new Node[] { node11, node21, node25 });
        node25.SetOptions(new Node[] { node24, node26, node27 });
        node26.SetOptions(new Node[] { node25, node30, node31 });
        node27.SetOptions(new Node[] { node23, node25, node28 });
        node28.SetOptions(new Node[] { node27, node29, node50 });
        node29.SetOptions(new Node[] { node23, node28 });
        node30.SetOptions(new Node[] { node26, node40, node38 });
        node31.SetOptions(new Node[] { node26, node35, node32 });
        node32.SetOptions(new Node[] { node33, node31 });
        node33.SetOptions(new Node[] { node32, node34 });
        node34.SetOptions(new Node[] { node33 });
        node35.SetOptions(new Node[] { node31, node36 });
        node36.SetOptions(new Node[] { node35, node37 });
        node37.SetOptions(new Node[] { node36, node38, node44 });
        node38.SetOptions(new Node[] { node30, node37, node39 });
        node39.SetOptions(new Node[] { node38, node40, node43 });
        node40.SetOptions(new Node[] { node30, node39, node41 });
        node41.SetOptions(new Node[] { node40, node42, node48 });
        node42.SetOptions(new Node[] { node41, node43, node45 });
        node43.SetOptions(new Node[] { node39, node42, node44 });
        node44.SetOptions(new Node[] { node37, node43, node45 });
        node45.SetOptions(new Node[] { node44, node42, node46 });
        node46.SetOptions(new Node[] { node45, node47, node48 });
        node47.SetOptions(new Node[] { node46, node51, node57 });
        node48.SetOptions(new Node[] { node41, node46, node49 });
        node49.SetOptions(new Node[] { node48, node50, node51 });
        node50.SetOptions(new Node[] { node28, node54, node53 });
        node51.SetOptions(new Node[] { node49, node47, node52 });
        node52.SetOptions(new Node[] { node53, node1 });
        node53.SetOptions(new Node[] { node52, node54, node50 });
        node54.SetOptions(new Node[] { node53, node50, node5, node1 });
        node55.SetOptions(new Node[] { node56 });
        node56.SetOptions(new Node[] { node57 });
        node57.SetOptions(new Node[] { node47 });
        node58.SetOptions(new Node[] { node0, node1, node2 });

        list[0] = node0;
        list[1] = node1;
        list[2] = node2;
        list[3] = node3;
        list[4] = node4;
        list[5] = node5;
        list[6] = node6;
        list[7] = node7;
        list[8] = node8;
        list[9] = node9;
        list[10] = node10;
        list[11] = node11;
        list[12] = node12;
        list[13] = node13;
        list[14] = node14;
        list[15] = node15;
        list[16] = node16;
        list[17] = node17;
        list[18] = node18;
        list[19] = node19;
        list[20] = node20;
        list[21] = node21;
        list[22] = node22;
        list[23] = node23;
        list[24] = node24;
        list[25] = node25;
        list[26] = node26;
        list[27] = node27;
        list[28] = node28;
        list[29] = node29;
        list[30] = node30;
        list[31] = node31;
        list[32] = node32;
        list[33] = node33;
        list[34] = node34;
        list[35] = node35;
        list[36] = node36;
        list[37] = node37;
        list[38] = node38;
        list[39] = node39;
        list[40] = node40;
        list[41] = node41;
        list[42] = node42;
        list[42] = node42;
        list[43] = node43;
        list[44] = node44;
        list[45] = node45;
        list[46] = node46;
        list[47] = node47;
        list[48] = node48;
        list[49] = node49;
        list[50] = node50;
        list[51] = node51;
        list[52] = node52;
        list[53] = node53;
        list[54] = node54;
        list[55] = node55;
        list[56] = node56;
        list[57] = node57;
        list[58] = node58;
        #endregion

        int random = Random.Range(0, list.Length);
        agent = GetComponent<NavMeshAgent>();

        this.agent.Warp(list[random].GetTransformData().position);
        prevCheckpoint = list[random];

        FindNewTarget();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (timeout > 500 || this.transform.position.x == (target.GetTransformData().position.x + randX)
            && this.transform.position.z == (target.GetTransformData().position.z + randZ)) {
            prevCheckpoint = target;
            
            FindNewTarget();
            timeout = 0;
        }
        timeout++;
    }

    private void FindNewTarget() {
        int rand;

        randX = Random.Range(0, radius);
        randZ = Random.Range(0, radius);
        rand = Random.Range(0, prevCheckpoint.GetLength());

        target = prevCheckpoint.GetOption(rand);
        agent.SetDestination(target.GetTransformData().position + new Vector3(randX, 0, randZ));

        FaceTarget();
    }

    private Transform GetCheckpointChild(int index) {
        return checkpointContainer.gameObject.transform.GetChild(index);
    }

    //TODO explain
    private void FaceTarget() {
        Vector3 direction = (target.GetTransformData().position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
