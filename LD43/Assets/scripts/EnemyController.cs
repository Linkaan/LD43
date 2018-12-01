using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public float baseDamage;
    public float baseProtection;
    public float baseSpeed;

    public float speedMultiplier;
    public float strengthMultipler;
    public float defenseMultipler;

    public Transform playerTarget;

    public GameObject statsPrefab;
    public Transform canvasParent;

    public float cooldownTime;

    NavMeshAgent agent;
    Animator anim;
    HealthObject health;

    float lastPunchTime;
    bool punchedLeft;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        health = GetComponent<HealthObject>();

        Billboard stats = Instantiate(statsPrefab, transform.position, Quaternion.identity).GetComponent<Billboard>();
        health.healthSlider = stats.GetComponent<Slider>();
        health.speedText = stats.transform.Find("Speed").GetComponent<Text>();
        health.defenseText = stats.transform.Find("Defense").GetComponent<Text>();
        health.strengthText = stats.transform.Find("Strength").GetComponent<Text>();
        stats.target = transform;
        stats.transform.SetParent(canvasParent, false);

        health.toDestroy.Add(stats.gameObject);

        health.strengthMultiplier = strengthMultipler;
        health.speedMultiplier = speedMultiplier;
        health.defenseMultiplier = defenseMultipler;
        health.strength = baseDamage * strengthMultipler;
        health.speed = baseSpeed * speedMultiplier;
        health.defense = baseProtection * defenseMultipler;

        agent.speed = health.speed;
    }

    void FixedUpdate() {
        float distance = Vector3.Distance(playerTarget.position, transform.position);
        agent.destination = playerTarget.position;

        if (distance <= agent.stoppingDistance) {
            transform.rotation = Quaternion.LookRotation(playerTarget.position - transform.position);
            Punch();
        }
    }

    void Punch() {
        if (Time.time - lastPunchTime > cooldownTime) {
            lastPunchTime = Time.time;

            if (!punchedLeft) {
                punchedLeft = true;
                anim.SetTrigger("leftPunch");
            } else {
                punchedLeft = false;
                anim.SetTrigger("rightPunch");
            }

            playerTarget.GetComponent<HealthObject>().LoseHealth(baseDamage * strengthMultipler);
        }
    }
	
}
