using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----ダッシュ時のエフェクト----
// パーティクルシステムのインスペクター上の「duration」から
// 持続時間を設定
public class DashEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    Jump_Dash playerScript; // playerのダッシュ状態を取得用

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Jump_Dash>();
    }

    void Update()
    {
        StartCoroutine(DashParticle());
    }

    // パーティクルの処理
    IEnumerator DashParticle()
    {
        if (playerScript.GetDash)
        {
            particle.Play();
            
            yield return new WaitForSeconds(particle.main.duration);
            
            // duration秒後くらいにエフェクトをストップ
            particle.Stop();
        }
    }
}
