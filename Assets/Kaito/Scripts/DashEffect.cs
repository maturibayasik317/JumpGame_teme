using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----ダッシュ時のみ起こる風のエフェクト----
public class DashEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject player;

    PlayerTest playerScript;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerTest>();
    }

    void Update()
    {
        StartCoroutine("DashParticle");
    }

    IEnumerator DashParticle()
    {
        if (playerScript.GetIsDash)
        {
            particle.Play();
            Debug.Log("particle_Start");
        }
        else
        {
            particle.Stop();
        }
        
        yield return null;
    }
}
