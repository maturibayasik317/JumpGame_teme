using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�_�b�V�����̂݋N���镗�̃G�t�F�N�g----
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
