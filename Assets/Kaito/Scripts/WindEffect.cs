using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----プレイヤーが存在する間常に吹いている風のエフェクト----
public class WindEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject player;

    void Start()
    {
        particle.Play();
    }

    void Update()
    {
        if (player == null)
        {
            particle.Stop();
        }
    }
}
