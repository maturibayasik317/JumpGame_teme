using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//----コインの動き等----
public class Coin : MonoBehaviour
{
    [SerializeField] float angle; // 何度ずつ回転させるか
    [SerializeField] ParticleSystem particle;

    Vector3 axis = Vector3.up; // 回転軸

    // プレイヤーが取得したコイン数
    // リセットボタンを押したときのみ初期化される
    static int getCoin_Stage1 = 0;
    static int getCoin_Stage2 = 0;
    static int getCoin_Stage3 = 0;

    // プロパティ
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

    // コイン取得時の効果音
    [SerializeField] AudioClip sound;
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

        yield return new WaitForSeconds(0.8f);

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
        
        Destroy(gameObject);
    }


    //-------static変数の初期化用関数-------
    //static Coin()
    //{
    //    SceneManager.sceneLoaded += Init;
    //}

    //// シーンが読み込まれたとき、コインの取得数を初期化
    //static void Init(Scene loadingScene, LoadSceneMode loadSceneMode)
    //{
    //    getCoin = 0;
    //}
    //---------------------------------------

    //// ステージセレクト画面で表示されるコイン取得数
    //static int stageCoinNum;
    //public int GetStageCoinNum => stageCoinNum;

    //// どのシーンのコインか
    //enum GameSceneType
    //{
    //    STAGE_SELECT,
    //    STAGE_1,
    //    STAGE_2,
    //    STAGE_3
    //}
    //[SerializeField] GameSceneType gameSceneType;

    //void CoinState()
    //{
    //// ステージごとにコイン数を取得
    //switch (gameSceneType)
    //{
    //    case GameSceneType.STAGE_SELECT:
    //        // 初期化処理
    //        //StageSelectCoin();
    //        break;
    //    case GameSceneType.STAGE_1:
    //        //Stage_1Coin();
    //        break;
    //    case GameSceneType.STAGE_2:
    //        //Stage_2Coin();
    //        break;
    //    case GameSceneType.STAGE_3:
    //        //Stage_3Coin();
    //        break;
    //}
    //}
}
