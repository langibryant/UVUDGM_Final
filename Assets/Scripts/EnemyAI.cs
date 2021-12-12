using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour
{
    public float health = 100.0f;

    public float speed = 10.0f;

    // public Vector3[] targets;

    public GameObject currentTarget;

    public int targetIndex = 0;

    public List<GameObject> targets;

    public GameObject checkpointParent;

    public float healthLoss = 10.0f;

    void Start() {
        health += WaveManager.instance.enemyHealthBonus;
        checkpointParent = GameObject.FindGameObjectsWithTag("checkpointParent")[0];
        targets.Add(checkpointParent.transform.GetChild(0).gameObject);
        targets.Add(checkpointParent.transform.GetChild(1).gameObject);
        targets.Add(checkpointParent.transform.GetChild(2).gameObject);
        targets.Add(checkpointParent.transform.GetChild(3).gameObject);
        targets.Add(checkpointParent.transform.GetChild(4).gameObject);

        if (targets.Count != 0) {
            currentTarget = targets[targetIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == currentTarget.transform.position) {
            targetIndex ++;
            if(targetIndex == targets.Count){
                GameManager.instance.UpdateHealth(healthLoss * -1.0f);
                Destroy(gameObject);
            }
            currentTarget = targets[targetIndex];

        }
        MoveTowardsPoint(currentTarget.transform.position);
    }

    void MoveTowardsPoint(Vector3 nextTarget) {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nextTarget, step);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Projectile")) {
            health = health - 10.0f;
            if(health <= 0) {
                WaveManager.instance.IncrementEnemiesKilled();
                Destroy(gameObject);
            }
        }
    }
}
