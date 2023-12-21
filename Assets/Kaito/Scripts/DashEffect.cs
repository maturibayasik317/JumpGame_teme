using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----ダッシュ時のエフェクト----
public class DashEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    [SerializeField] float duration; // パーティクルの持続時間

    Jump_Dash playerScript; // playerのダッシュ状態を取得用

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Jump_Dash>();
    }

    void Update()
    {
        StartCoroutine("DashParticle", duration);
    }

    // パーティクルの処理
    IEnumerator DashParticle(float duration)
    {
        if (playerScript.GetDash)
        {
            particle.Play();
            
            yield return new WaitForSeconds(duration);
            
            // duration秒後にエフェクトをストップ
            particle.Stop();
        }
    }
}
