﻿using UnityEngine;

public class ChargedBall : MonoBehaviour {
    // components
    GameObject lastHitBy;
    Rigidbody rb;
    ParticleSystem ps;

    // multiplier for dash hit velocity
    float DEFAULT_CHARGE = 1f;
    float CHARGE_BONUS = .1f;
    float MAX_CHARGE = 2f;
    float charge;

    // ball scale
    static Vector3 baseScale;

    // Use this for initialization
    void Start () {
        ResetCharge();
        rb = GetComponent<Rigidbody>();
        baseScale = transform.localScale;
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update () {
        // Debug.Log(charge);
        transform.localScale = baseScale * charge;
        UpdateColor();
    }

    void OnCollisionEnter(Collision collision) {
        GameObject other = collision.gameObject;

        if (other.CompareTag("player")) {
            // someone hit the ball
            if (lastHitBy == null) {
                // first hit (serve)
                lastHitBy = other;
                AddCharge();
            } else {
                // grab PlayerMovement script
                PlayerMovement pm = other.GetComponent<PlayerMovement>();

                if (pm.IsDashing()) {
                    // dashes consume charge regardless of team
                    ApplyCharge();
                    ResetCharge();
                } else {
                    // only do something if a different player hits the ball
                    if (other != lastHitBy) {
                        AddCharge();
                        lastHitBy = other;
                    }
                }
            }
        }

        if (other.CompareTag("ground")) {
            ResetCharge();
        }
    }

    void AddCharge() {
        Debug.Log("adding charge");
        charge = Mathf.Min(MAX_CHARGE, charge + CHARGE_BONUS);
    }

    void ResetCharge() {
        Debug.Log("resetting charge");
        // reset charge
        charge = DEFAULT_CHARGE;
        // reset lastHitBy
        lastHitBy = null;
    }

    void ApplyCharge() {
        Debug.Log("applying charge: " + charge);
        // apply charge to velocity
        if (rb != null) {
            rb.velocity  = rb.velocity * charge;
        }
    }

    // https://docs.unity3d.com/ScriptReference/ParticleSystem-colorOverLifetime.html
    void UpdateColor() {
        ParticleSystemRenderer r = GetComponent<ParticleSystemRenderer>();

        if (lastHitBy == null) {
            r.material.color = Color.white;
        } else
        {
            r.material = lastHitBy.GetComponentInChildren<MeshRenderer>().material;
        }
    }
}