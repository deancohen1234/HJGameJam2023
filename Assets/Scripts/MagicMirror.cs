using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMirror : MonoBehaviour
{
    public GameObject[] allOmens;

    public Vector2 minMaxWaitDuration = new Vector2(0.5f, 2f);
    public Vector2 minMaxHoldDuration = new Vector2(1.5f, 4.0f);

    private GameObject spawnedOmen;
    private float omenDespawnTime;
    private float omenSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedOmen == null && Time.time >= omenSpawnTime) {
            SpawnOmen();
        }

        if (spawnedOmen != null && Time.time >= omenDespawnTime) {
            DespawnOmen();
        }
    }

    private void SpawnOmen() {
        spawnedOmen = Instantiate(allOmens[Random.Range(0, allOmens.Length)]);
        spawnedOmen.transform.position = transform.position;
        spawnedOmen.transform.LookAt(Camera.main.transform);

        omenDespawnTime = Time.time + Random.Range(minMaxHoldDuration.x, minMaxHoldDuration.y);
    }

    private void DespawnOmen() {
        Destroy(spawnedOmen);

        omenSpawnTime = Time.time + Random.Range(minMaxWaitDuration.x, minMaxWaitDuration.y);
    }
}
