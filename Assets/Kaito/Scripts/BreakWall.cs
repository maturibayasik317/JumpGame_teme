using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    PlayerTest playerScript;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerTest>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerScript.GetDash)
        {
            Destroy(gameObject);
        }
    }
}
