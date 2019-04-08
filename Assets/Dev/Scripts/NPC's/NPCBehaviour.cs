using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCBehaviour : MonoBehaviour {
    // GameObject references and serializable variables
    public GameObject checkpointContainer;
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;

    // Private logic variables
    public Node nextCheckpoint, currentCheckpoint, previousCheckpoint;
    private Node[] spawnList;
    private int randX, randZ;
    // Private variables not to be changed
    private readonly int radius = 4;
    // Logic variables that need referencing in other scripts (mostly Officer.cs)
    public bool questioned;
    public bool inQuestioning;
    public GameObject officerQuestioning;
    public NavMeshAgent agent = new NavMeshAgent();
    public bool relocating;

    void Start() {
        // initialize values
        int randomSpawnLocation = Random.Range(0, spawnList.Length);
        randX = Random.Range(0, radius);
        randZ = Random.Range(0, radius);
        agent = GetComponent<NavMeshAgent>();

        // Set NPC's position on a random node with a slight offset so NPC's don't spawn inside each other, Using Warp() rather than position
        // because transform.position unsyncs NPC from navmesh making it unable to walk across it
        this.agent.Warp(spawnList[randomSpawnLocation].GetTransformData().position + new Vector3(randX, 0, randZ));

        // Prevent NPC's from spawning on clutter heavy checkpoints
        currentCheckpoint = spawnList[randomSpawnLocation];

        // Randomize NPC's speed and set autoRepath to true so NPC's don't walk to invalid points on map
        agent.speed = Random.Range(minSpeed, maxSpeed);
        agent.autoRepath = true;

        //Find target to walk to
        FindNewCheckpoint();
    }

    public void SetSpawnList(Node[] spawnlist) {
        this.spawnList = spawnlist;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (inQuestioning == false) {
            agent.isStopped = false;
            // if timeout overflows OR NPC has reached destination find a new destination
            if (ReachedNode()) {
                // Set false so Officer script knows the officer finished relocating
                if (relocating)
                    relocating = false;
                if (Suspect.running)
                    Suspect.running = false;

                previousCheckpoint = currentCheckpoint;
                currentCheckpoint = nextCheckpoint;

                FindNewCheckpoint();
            }
        } else {
            // Stop for questioning
            if (officerQuestioning != null)
                FaceTarget(officerQuestioning.transform);
            agent.isStopped = true;
            questioned = true;
        }
    }

    private void FindNewCheckpoint() {
        // initialize values
        randX = Random.Range(0, radius);
        randZ = Random.Range(0, radius);

        // Set random option as destination
        nextCheckpoint = currentCheckpoint.GetOption(Random.Range(0, currentCheckpoint.GetLength()));

        // Confirm that nextCheckpoint is not equal to previousCheckpoint so NPC's don't walk back and forth between the same points
        while (nextCheckpoint == previousCheckpoint && currentCheckpoint.GetLength() != 1)
            nextCheckpoint = currentCheckpoint.GetOption(Random.Range(0, currentCheckpoint.GetLength()));

        agent.SetDestination(nextCheckpoint.GetTransformData().position + new Vector3(randX, 0, randZ));

        FaceTarget(nextCheckpoint.GetTransformData());
    }

    private bool ReachedNode() {
        if (this.transform.position.x >= nextCheckpoint.GetTransformData().position.x - radius &&
                this.transform.position.x <= nextCheckpoint.GetTransformData().position.x + radius
                && this.transform.position.z >= nextCheckpoint.GetTransformData().position.z - radius
                && this.transform.position.z <= nextCheckpoint.GetTransformData().position.z + radius)
            return true;
        return false;
    }

    public void MoveToTarget(GameObject target) {
        agent.SetDestination(target.transform.position);
    }

    /// Reset this NPC's currentCheckpoint to a new Node, Node is calculated with given coordinates by finding the nearest Node 
    /// <param name="coordinates">Coordinates on the map of the general location this NPC should move to</param>
    public void RelocateToTarget(Vector3 coordinates) {
        // initialize variables
        relocating = true;
        List<GameObject> distances = new List<GameObject>();
        GameObject closestNode = null;
        Node nodeTarget;
        float? minDistance = null;
        coordinates.y = 0.1f;


        //Find add all the nodes
        foreach (Transform node in GetNodes()) {
            Vector3 nodePos = new Vector3(node.gameObject.transform.position.x, 0.1f, node.gameObject.transform.position.z);

            if (Vector3.Distance(coordinates, nodePos) < minDistance || minDistance == null) {
                closestNode = node.gameObject;
                minDistance = Vector3.Distance(coordinates, nodePos);
            }
        }

        // Get index of checkpoint from it's name since the number in the name matches it's index number and set that as the target
        nodeTarget = NPCMaker.nodeList[int.Parse(Regex.Replace(closestNode.name, @"[^\d]", ""))];

        //Override all navigation variables to effectively relocate NPC
        currentCheckpoint = nodeTarget;
        previousCheckpoint = nodeTarget;
        nextCheckpoint = nodeTarget;

        agent.SetDestination(nodeTarget.GetTransformData().position);
    }

    public void FaceTarget(Transform target) {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public List<Transform> GetNodes() {
        List<Transform> list = new List<Transform>();
        foreach (Transform node in GameObject.Find("CheckpointContainer").transform)
            list.Add(node);
        return list;
    }
}
