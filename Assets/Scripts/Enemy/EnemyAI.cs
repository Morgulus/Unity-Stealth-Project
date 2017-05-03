using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public float patrolSpeed = 2f;
	public float chaseSpeed = 5f;
	public float patrolWaitTime = 1f;
	public float chaseWaitTime = 5f;
	public Transform[] patrolWayPoints;

	private EnemySight enemySight;
	private UnityEngine.AI.NavMeshAgent nav;
	private Transform playerTr;
	private PlayerHealth playerHealth;
	private LastPlayerSighting lastPlayerSighting;
	private float chaseTimer;
	private float patrolTimer;
	private int wayPointIndex;

	void Awake(){
		enemySight = GetComponent<EnemySight> ();
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		playerTr = GameObject.FindGameObjectWithTag (Tags.player).transform;
		playerHealth = playerTr.GetComponent<PlayerHealth> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();

	}

	void Update(){
		if (enemySight.playerInSight && playerHealth.health > 0f) {
			Shooting ();
		} else if (enemySight.personalLastPlayerSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0f) {
			Chasing ();
		} 
		else {
			Patroling ();
		}
	}

	void Shooting(){
		nav.Stop ();
	}

	void Chasing(){
		nav.Resume ();
		Vector3 sightDeltaPos = enemySight.personalLastPlayerSighting - transform.position;
		if (sightDeltaPos.sqrMagnitude > 4f)
			nav.destination = enemySight.personalLastPlayerSighting;

		nav.speed = chaseSpeed;

		if (nav.remainingDistance < nav.stoppingDistance) {
			chaseTimer += Time.deltaTime;

			if (chaseTimer >= chaseWaitTime) {
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				enemySight.personalLastPlayerSighting = lastPlayerSighting.resetPosition;
				chaseTimer = 0f;
			}
		} 
		else
			chaseTimer = 0f;
	}

	void Patroling(){
		nav.Resume ();
		nav.speed = patrolSpeed;

		if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance) {
			patrolTimer += Time.deltaTime;

			if (patrolTimer >= patrolWaitTime) {
				if (wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;
				patrolTimer = 0f;
			}
		} 
		else
			patrolTimer = 0f;

		nav.destination = patrolWayPoints [wayPointIndex].position;
	}

}
