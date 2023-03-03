using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class MagicMirror : Location
{
    public GameObject[] allOmens;

    public Material omenMaterial;
    public AnimationCurve alphaCurve;
    public AudioMixer audioMixer;

    public Vector2 minMaxWaitDuration = new Vector2(0.5f, 2f);
    public Vector2 minMaxHoldDuration = new Vector2(1.5f, 4.0f);

    public float omenMaxMoveSpeed = 0.5f;
    public float omenTorqueSpeed = 5f;
    public float omenGravity = 0.1f;

    public float volumeFadeDuration = 0.75f;

    private GameObject spawnedOmen;
    private Rigidbody omenRB;
    private AudioSource audioSource;

    private float omenDespawnTime;
    private float omenSpawnTime;

    private const float LOW_PASS_THRESHOLD_DEFAULT = 5000;
    private const float LOW_PASS_THRESHOLD_ON = 350;

    private void Start() {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = 0;

        audioMixer.DOSetFloat("LowPassCutoff", LOW_PASS_THRESHOLD_DEFAULT, volumeFadeDuration);
    }

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

        StartAudio();
    }

    public override void Exit() {

        DespawnOmen();

        base.Exit();

        StopAudio();
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

    private void StartAudio() {
        //audioSource.Play();

        audioMixer.DOSetFloat("LowPassCutoff", LOW_PASS_THRESHOLD_ON, volumeFadeDuration);

        audioSource.DOFade(1.0f, volumeFadeDuration);

    }

    private void StopAudio() {

        //audioSource.Stop();
        audioMixer.DOSetFloat("LowPassCutoff", LOW_PASS_THRESHOLD_DEFAULT, volumeFadeDuration);

        audioSource.DOFade(0, volumeFadeDuration);
    }
}
