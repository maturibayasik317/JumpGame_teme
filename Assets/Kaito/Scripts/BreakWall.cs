using System.Collections;
using UnityEngine;

/// <summary>
/// ダッシュで壊せる壁のアニメーションなどを行うクラス
/// </summary>
public class BreakWall : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] Collider2D _collider;
    Jump_Dash playerScript;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Jump_Dash>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // ダッシュ状態のプレイヤーが当たったら
        if (collision.gameObject.CompareTag("Player") && playerScript.GetDash)
        {
            _collider.isTrigger = true; // 通り抜けられるようにする
            StartCoroutine(WallDestroy());
        }
    }

    /// <summary>
    /// 壁が壊れる処理を行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator WallDestroy()
    {
        particle.Play();

        // エフェクト終了後に壁が壊れる
        yield return new WaitForSeconds(particle.main.duration);
        Destroy(gameObject);
    }
}
