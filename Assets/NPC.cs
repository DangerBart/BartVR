﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour {

	private Animator animator;
	[SerializeField]
	private float npcMovementSpeed = 5f;
	private float lowerBodyLayer = 0f;
	// Use this for initialization
	private NavMeshAgent navMeshAgent;
	void Start () {
		animator = this.GetComponentInChildren<Animator>();
		navMeshAgent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		float speed = navMeshAgent.velocity.magnitude;
		animator.SetFloat("Speed", speed);
		//if(speed < Mathf.Abs(0.05f) && lowerBodyLayer != 1){
		//	lowerBodyLayer = Mathf.Lerp(lowerBodyLayer, 0, 2 * Time.deltaTime);
		//	//animator.SetLayerWeight(1, lowerBodyLayer);
		//}
		//else{
		//	lowerBodyLayer = Mathf.Lerp(lowerBodyLayer, 1, speed * Time.deltaTime);
		//	//animator.SetLayerWeight(1, lowerBodyLayer);
		//}
	}
}
