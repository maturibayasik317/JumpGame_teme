using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTest : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    PlayerTest playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerTest>();
        _particleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.isDash)
        {

        }
        else
        {
            
        }
    }
}
