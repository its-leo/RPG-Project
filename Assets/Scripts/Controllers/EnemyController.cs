using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    // Use this for initialization
    void Start() {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();

    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius) {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance) {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null) {
                    combat.Attack(targetStats);
                }

                FaceTarget();
            }
        }
    }

    public void FaceTarget() {
        //Get direction towards the target
        Vector3 direction = (target.position - transform.position).normalized;
        //How to turn to look towards the target, Avoid looking up (0f)
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        //Smooth the interpolate towards target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
}
