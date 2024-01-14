using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----ダッシュ時のエフェクト----
public class DashEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    // ダッシュエフェクトの持続時間
    [SerializeField] float dashDuration;

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
            // ダッシュエフェクト発生
            particle.Play();
            yield return new WaitForSeconds(dashDuration);
            // dashDuration秒後くらいにエフェクトをストップ
            particle.Stop();
        }
    }
}
