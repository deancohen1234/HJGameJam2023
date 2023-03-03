using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundOnCollision : MonoBehaviour
{
    private AudioSource source;

    private void Start() {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {

        if (source == null) {
            source = GetComponent<AudioSource>();
        }

        if (collision.impulse.sqrMagnitude >= 0.1f) {
            float pitch = Random.Range(0.75f, 1.25f);
            source.pitch = pitch;
            source.Play();
        }
        
    }
}
