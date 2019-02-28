using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class SuspectBehaviour : MonoBehaviour {
    //List of desired checkpoint nodes
    [SerializeField][Tooltip("Add the checkpoint nodes that the suspect can walk between here")]
    public List<GameObject> checkpoints = new List<GameObject>();

    //Logic variables
    private NavMeshAgent agent;
    private Vector3 target;
    private int randomIndex;
    private bool findNewDestination = false;
    
    // Use this for initialization
    void Start () {
        //Initialize suspect on checkpoint node 0 and move to node 1
        agent = this.GetComponent<NavMeshAgent>();
        agent.Warp(checkpoints[0].transform.position);
        agent.SetDestination(checkpoints[1].transform.position);
        target = agent.destination;
    }
	
	// Update is called once per frame
	void Update () {
        //Set random index as destination
        if (findNewDestination) {
            findNewDestination = false;
            randomIndex = Random.Range(0, checkpoints.Count);
            agent.SetDestination(checkpoints[randomIndex].transform.position);
            target = agent.destination;
        } 
        //When suspect reaches destination find a new one
        if (transform.position == target) {
            findNewDestination = true;
        }
	}
}
