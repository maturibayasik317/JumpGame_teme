using System.Collections;
using UnityEngine;

/// <summary>
/// コインの動きや取得した数をカウントしておくクラス
/// </summary>
public class Coin : MonoBehaviour
{
    [SerializeField] float angle; // 何度ずつ回転させるか
    [SerializeField] ParticleSystem particle;

    Vector3 axis = Vector3.up; // 回転軸

    // プレイヤーが取得したコインの数
    static int[] playerCoinNums = new int[GlobalConst.MAX_STAGE_NUM];

    /// <summary>
    /// プレイヤーがステージごとに取得したコイン枚数を返す関数
    /// </summary>
    /// <param name="_stageNum">ステージ番号</param>
    /// <returns>引数で指定したステージのコイン取得枚数</returns>
    public int GetPlayerCoinNums(int _stageNum) { return playerCoinNums[_stageNum - 1];}
   
    /// <summary>
    /// プレイヤーが取得したコイン枚数をリセットする関数
    /// </summary>
    public void ResetPlayerCoinNums()
    {
        for (int num = 0; num < playerCoinNums.Length; num++)
        {
            playerCoinNums[num] = 0;
        }
    }

    AudioSource audioSource; // コイン取得時の効果音用
    CoinManager coinManagerScript;

    void Start()
    {
        particle.Play();
        audioSource = GetComponent<AudioSource>();
        coinManagerScript = GameObject.Find("CoinManager").GetComponent<CoinManager>();
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

    /// <summary>
    /// コインに動きを付ける関数
    /// </summary>
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

    /// <summary>
    /// コイン取得時の処理を行う関数
    /// </summary>
    /// <returns></returns>
    IEnumerator CoinDestroy()
    {
        audioSource.PlayOneShot(audioSource.clip); // soundを1回鳴らす
        particle.Stop();

        yield return new WaitForSeconds(0.8f);
        
        // コイン枚数が最大値を超えていたら呼ばないようにする
        if (playerCoinNums[coinManagerScript.GetStageNum() - 1] < GlobalConst.MAX_COIN_NUM)
        {
            playerCoinNums[coinManagerScript.GetStageNum() - 1]++;
        }
        Destroy(gameObject);
    }
}
