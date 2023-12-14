using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    void Start()
    {
        particle.Play();
    }

    void Update()
    {

    }
}
