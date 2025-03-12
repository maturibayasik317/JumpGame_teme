using System.Collections;
using UnityEngine;

/// <summary>
/// ダッシュ時のエフェクトを発生させるクラス
/// </summary>
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

    /// <summary>
    /// パーティクルの処理を行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator DashParticle()
    {
        if (playerScript.GetDash)
        {
            // ダッシュエフェクト発生
            particle.Play();
            yield return new WaitForSeconds(dashDuration);
            particle.Stop();
        }
    }
}
