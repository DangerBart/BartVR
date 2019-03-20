using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCBehaviour : MonoBehaviour {

    public GameObject checkpointContainer;
    
    private readonly int radius = 4;
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;
    private Node nextCheckpoint, currentCheckpoint, previousCheckpoint;
   
    private Node[] spawnList;
    public NavMeshAgent agent = new NavMeshAgent();
    private int randX, randZ;
    private int timeout;
    private readonly int overflow = 1200;

    // Use this for initialization
    void Start() {

        //initialize values
        int randomSpawnLocation = Random.Range(0, spawnList.Length);
        randX = Random.Range(0, radius);
        randZ = Random.Range(0, radius);
        agent = GetComponent<NavMeshAgent>();

        //Set NPC's position on a random node with a slight offset so NPC's don't spawn inside each other, Using Warp() rather than position
        //because transform.position unsyncs NPC from navmesh making it unable to walk across it
        this.agent.Warp(spawnList[randomSpawnLocation].GetTransformData().position + new Vector3(randX, 0, randZ));

        //Prevent NPC's from spawning on clutter heavy checkpoints
        currentCheckpoint = spawnList[randomSpawnLocation];

        //Randomize NPC's speed and set autoRepath to true so NPC's don't walk to invalid points on map
        agent.speed = Random.Range(minSpeed, maxSpeed);
        agent.autoRepath = true;

        //Find target to walk to
        FindNewTarget();
    }

    public void SetSpawnList(Node[] spawnlist) {   
        this.spawnList = spawnlist;
    }

    // Update is called once per frame
    void FixedUpdate() {
        //if timeout overflows OR NPC has reached destination find a new destination
        if (timeout > overflow || ((this.transform.position.x >= nextCheckpoint.GetTransformData().position.x - radius &&
            this.transform.position.x <= nextCheckpoint.GetTransformData().position.x + radius) && (this.transform.position.z
            >= nextCheckpoint.GetTransformData().position.z - radius && this.transform.position.z <= nextCheckpoint.GetTransformData().position.z + radius))) {
            previousCheckpoint = currentCheckpoint;
            currentCheckpoint = nextCheckpoint;
            FindNewTarget();
            timeout = 0;
        }
        timeout++;
    }

    private void FindNewTarget() {
        //initialize values
        bool foundValidCheckpoint = false;
        randX = Random.Range(0, radius);
        randZ = Random.Range(0, radius);
        int randNextCheckpoint = Random.Range(0, currentCheckpoint.GetLength());

        //Set random option as destination
        nextCheckpoint = currentCheckpoint.GetOption(randNextCheckpoint);

        //Confirm that nextCheckpoint is not equal to previousCheckpoint so NPC's don't walk back and forth between the same points
        if (currentCheckpoint.GetLength() != 1) {
            while (!foundValidCheckpoint) {
                if (nextCheckpoint == previousCheckpoint) {
                    randNextCheckpoint = Random.Range(0, currentCheckpoint.GetLength());
                    nextCheckpoint = currentCheckpoint.GetOption(randNextCheckpoint);
                } else {
                    foundValidCheckpoint = true;
                }
            }
        }
        agent.SetDestination(nextCheckpoint.GetTransformData().position + new Vector3(randX, 0, randZ));

        //Face the destination
        FaceTarget();
    }

    public void MoveToTarget(GameObject target) {
        // TODO implement moving to target here
    }

    //Function extracted from Brackey's tutorial on making an RPG in Unity, NPC sets it's rotation to look towards the target it is walking towards
    private void FaceTarget() {
        Vector3 direction = (nextCheckpoint.GetTransformData().position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
