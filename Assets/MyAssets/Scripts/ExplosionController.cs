using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {
    [SerializeField] ParticleSystem ExplosionFireball;
    [SerializeField] ParticleSystem ShockWave;
    [SerializeField] ParticleSystem Sparks;
    [SerializeField] ParticleSystem Light;

    public void Explosion () {
        ExplosionFireball.Play ();
        ShockWave.Play ();
        Sparks.Play ();
        Light.Play ();
    }

}