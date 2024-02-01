using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----コインの動き等----
public class Coin : MonoBehaviour
{
    [SerializeField] float angle; // 何度ずつ回転させるか
    [SerializeField] ParticleSystem particle;
    
    Vector3 axis = Vector3.up; // 回転軸

    // プレイヤーが取得したコイン数
    static int getCoin = 0;
    public int GetPlayerCoin => getCoin;

    // コイン取得時の効果音
    [SerializeField] AudioClip sound;
    AudioSource audioSource;

    void Start()
    {
        particle.Play();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CoinMove();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CoinDestroy());
        }
    }

    // コインの動き
    void CoinMove()
    {
        // axis軸に、毎秒angle度回転させるQuaternionを作成
        Quaternion rot = Quaternion.AngleAxis(angle, axis);
        // 現在の自身の回転の情報を取得
        Quaternion q = transform.rotation;
        // 合成し、自身に設定（q * rot → 和）
        transform.rotation = q * rot;

        // 子オブジェクトは回転しないように上書き
        // 引数には、パーティクルのRotationを代入
        particle.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // コイン取得時の処理
    IEnumerator CoinDestroy()
    {
        audioSource.PlayOneShot(sound); // soundを1回鳴らす
        particle.Stop();

        yield return new WaitForSeconds(1.0f);

        getCoin += 1; // UIで使用
        Destroy(gameObject);
    }
}
