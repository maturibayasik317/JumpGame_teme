using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----ダッシュ時のみ起こる風のエフェクト----
public class DashEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    PlayerTest playerScript;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerTest>();
    }

    void Update()
    {
        if (playerScript.isDash)
        {
            particle.Play();
            Debug.Log("particle_Start");
        }
        else
        {
            particle.Stop();
        }
    }
}
