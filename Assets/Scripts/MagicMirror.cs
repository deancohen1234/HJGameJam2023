using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMirror : Location
{
    public GameObject[] allOmens;

    public Material omenMaterial;
    public AnimationCurve alphaCurve;

    public Vector2 minMaxWaitDuration = new Vector2(0.5f, 2f);
    public Vector2 minMaxHoldDuration = new Vector2(1.5f, 4.0f);

    public float omenMaxMoveSpeed = 0.5f;
    public float omenTorqueSpeed = 5f;
    public float omenGravity = 0.1f;

    private GameObject spawnedOmen;
    private Rigidbody omenRB;

    private float omenDespawnTime;
    private float omenSpawnTime;

    // Update is called once per frame
    void Update()
    {
        if (!IsActive()) {
            return;
        }

        if (spawnedOmen == null && Time.time >= omenSpawnTime && OmenManager.instance.HasActiveOmen()) {
            SpawnOmen();
        }

        if (spawnedOmen != null && Time.time >= omenDespawnTime) {
            DespawnOmen();
        }

        //update omen alpha
        if (spawnedOmen != null) {
            UpdateOmenAlpha();
        }
    }

    private void FixedUpdate() {
        if (spawnedOmen != null) {

            ApplyGravityToOmen();
        }
    }

    private void OnDestroy() {
        //reset material properties
        omenMaterial.SetFloat("_Alpha", 1);

    }

    public override void Enter() {
        base.Enter();

        omenSpawnTime = Time.time + Random.Range(minMaxWaitDuration.x, minMaxWaitDuration.y);
    }

    public override void Exit() {

        DespawnOmen();

        base.Exit();
    }

    private void SpawnOmen() {

        spawnedOmen = Instantiate(OmenManager.instance.GetCurrentOmen());
        spawnedOmen.transform.position = transform.position;
        spawnedOmen.transform.LookAt(Camera.main.transform);
        omenRB = spawnedOmen.GetComponent<Rigidbody>();

        omenDespawnTime = Time.time + Random.Range(minMaxHoldDuration.x, minMaxHoldDuration.y);

        omenMaterial.SetFloat("_Alpha", 0);

        omenRB.velocity = Random.insideUnitSphere * omenMaxMoveSpeed;
        omenRB.AddTorque(Random.insideUnitSphere * omenTorqueSpeed);
        omenRB.detectCollisions = false;
    }

    private void DespawnOmen() {

        if (spawnedOmen != null) {

            Destroy(spawnedOmen);
        }

        omenSpawnTime = Time.time + Random.Range(minMaxWaitDuration.x, minMaxWaitDuration.y);

        omenMaterial.SetFloat("_Alpha", 0);
    }

    private void UpdateOmenAlpha() {

        float normalizedTime = (Time.time - omenSpawnTime) / (omenDespawnTime - omenSpawnTime);

        float alpha = alphaCurve.Evaluate(normalizedTime);

        omenMaterial.SetFloat("_Alpha", alpha);
    }

    private void ApplyGravityToOmen() {

        Vector3 directionToCenter = transform.position - omenRB.position;
        omenRB.AddForce(directionToCenter.normalized * directionToCenter.magnitude * directionToCenter.magnitude * omenGravity, ForceMode.Acceleration);
    }
}
