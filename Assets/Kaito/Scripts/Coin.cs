using System.Collections;
using UnityEngine;

//----コインの動き等----
public class Coin : MonoBehaviour
{
    [SerializeField] float angle; // 何度ずつ回転させるか
    [SerializeField] ParticleSystem particle;

    Vector3 axis = Vector3.up; // 回転軸

    // プレイヤーが取得したコイン数
    // リセットボタンを押したときのみ初期化される：削除予定
    static int getCoin_Stage1 = 0;
    static int getCoin_Stage2 = 0;
    static int getCoin_Stage3 = 0;
    static int getCoin_Stage4 = 0;

    static int[] playerCoinNums = new int[GlobalConst.MAX_STAGE_NUM];

    /// <summary>
    /// プレイヤーがステージごとに取得したコイン枚数を返す関数
    /// </summary>
    /// <param name="_stageNum">ステージ番号</param>
    /// <returns></returns>
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

    // プロパティ：削除予定
    public int PlayerCoin_Stage1
    {
        get { return getCoin_Stage1; }
        set { getCoin_Stage1 = value; }
    }
    public int PlayerCoin_Stage2
    {
        get { return getCoin_Stage2; }
        set { getCoin_Stage2 = value; }
    }
    public int PlayerCoin_Stage3
    {
        get { return getCoin_Stage3; }
        set { getCoin_Stage3 = value; }
    }
    public int PlayerCoin_Stage4
    {
        get { return getCoin_Stage4; }
        set { getCoin_Stage4 = value; }
    }

    // コイン取得時の効果音用
    AudioSource audioSource;

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

        #if false // 変更前
        // ステージ1
        if (coinManagerScript.gameSceneType == CoinManager.GameSceneType.STAGE_1)
        {
            getCoin_Stage1 += 1; // UIで使用
        }
        // ステージ2
        else if (coinManagerScript.gameSceneType == CoinManager.GameSceneType.STAGE_2)
        {
            getCoin_Stage2 += 1;
        }
        // ステージ3
        else if (coinManagerScript.gameSceneType == CoinManager.GameSceneType.STAGE_3)
        {
            getCoin_Stage3 += 1;
        }
        // ステージ４
        else if (coinManagerScript.gameSceneType == CoinManager.GameSceneType.STAGE_4)
        {
            getCoin_Stage4 += 1;
        }
        #endif
        Destroy(gameObject);
    }
}
