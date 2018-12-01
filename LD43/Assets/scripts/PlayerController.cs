using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float baseDamage;
    public float baseProtection;
    public float baseSpeed;

    public float speedMultiplier;
    public float strengthMultipler;
    public float defenseMultipler;

    public float speedDelta;
    public float strengthDelta;
    public float defenseDelta;

    public GameObject statsPrefab;
    public Transform canvasParent;

    public float punchDuration;

    Rigidbody rb;
    Animator anim;
    HealthObject health;

    float lastPunchTime;
    bool hasTarget;

	void Start () {
        rb = GetComponent<Rigidbody>();
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
	}

    void Update() {
        if (Input.GetMouseButtonDown(0)) {            
            Punch(true);
        } else if (Input.GetMouseButtonDown(1)) {
            Punch(false);
        }
    }

    void Punch(bool punchLeft) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);
        Transform closestEnemy = transform;
        float closestDistance = float.MaxValue;

        foreach(Collider col in colliders) {
            if (col.CompareTag("Enemy")) {
                float distance = Vector3.Distance(col.transform.position, transform.position);

                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestEnemy = col.transform;
                }
            }
        }

        if (closestEnemy != transform) {
            closestEnemy.GetComponent<HealthObject>().LoseHealth(baseDamage * strengthMultipler);
            transform.rotation = Quaternion.LookRotation(closestEnemy.position - transform.position);
            hasTarget = true;
        } else {
            hasTarget = false;
        }

        if (punchLeft) {
            anim.SetTrigger("leftPunch");
        } else {
            anim.SetTrigger("rightPunch");
        }

        lastPunchTime = Time.time;
    }
	
	void FixedUpdate () {
        float horizontal = -Input.GetAxis("Horizontal");
        float vertical = -Input.GetAxis("Vertical");

        Vector3 forward = Vector3.forward * vertical * baseSpeed * speedMultiplier;
        Vector3 right = Vector3.right * horizontal * baseSpeed * speedMultiplier;

        rb.AddForce(forward);
        rb.AddForce(right);

        if (Time.time - lastPunchTime < punchDuration && hasTarget) return;

        if (forward + right != Vector3.zero) transform.rotation = Quaternion.LookRotation(forward + right);
        else {
            rb.angularVelocity = Vector3.zero;
        }
	}

    public void LoseStrength()
    {
        strengthMultipler -= strengthDelta;
        health.strengthMultiplier = strengthMultipler;
        health.strength = baseDamage * strengthMultipler;
    }

    public void LoseSpeed()
    {
        speedMultiplier -= speedDelta;
        health.speedMultiplier = speedMultiplier;
        health.speed = baseSpeed * speedMultiplier;
    }

    public void LoseDefense()
    {
        defenseMultipler -= defenseDelta;
        health.defenseMultiplier = defenseMultipler;
        health.defense = baseProtection * defenseMultipler;
    }
}
