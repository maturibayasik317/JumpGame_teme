using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----ダッシュで壊せる壁のスクリプト----
// パーティクルシステムのインスペクター上の「duration」から
// 壁が壊れるまでの時間を設定
public class BreakWall : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] Collider2D _collider;
    Jump_Dash playerScript;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Jump_Dash>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ダッシュ状態のプレイヤーが当たったら
        if (collision.gameObject.CompareTag("Player") && playerScript.GetDash)
        {
            _collider.isTrigger = true; // 通り抜けられるようにする
            StartCoroutine(WallDestroy());
        }
    }

    // 壁が壊れる処理
    IEnumerator WallDestroy()
    {
        particle.Play();

        // エフェクト終了後に壁が壊れる
        yield return new WaitForSeconds(particle.main.duration);
        Destroy(gameObject);
    }
}
