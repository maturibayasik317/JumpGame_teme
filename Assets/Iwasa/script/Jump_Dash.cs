using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump_Dash : MonoBehaviour
{   //プレイヤーにコンポーネントしてください
    private Vector2 velocity;
    private int JumpCount = 0;//ジャンプ終了時の処理
    private bool jump = false;
    private bool isground;
    bool dash = false;
    bool fall = false;
    public bool GetDash => dash;
    public GameObject text;

    Rigidbody2D rigid2D;
    [SerializeField] ContactFilter2D filter2D;

    private float dashElapsedTime = 0.0f;//ダッシュが始まってからの落下までの猶予
    private float fallElapsedTime = 0.0f; // 落下開始からの経過時間
    [SerializeField] float Dash_Distance;//ダッシュ距離150~250ぐらいがオススメ
    [SerializeField] float Jump_Power;//ジャンプ力500ぐらいがオススメ
    [SerializeField] private int Jump_Count = 1;//ジャンプ可能回数
    [SerializeField] float Fall_Duration = 0.0f; // 落下開始からの持続時間
    [SerializeField] float FallSpeed = 0.0f;//落下速度

    void MoveDash()//ジャンプ中にSpeace押した時のダッシュ処理
    {
        dash = true;
        dashElapsedTime = 0.0f;
        fallElapsedTime = 0.0f; // ダッシュが始まったので落下経過時間をリセット

        rigid2D.velocity = new Vector2(Dash_Distance, rigid2D.velocity.y);

        Debug.Log ("横移動");
    }

    void Fall()
    {
        transform.Translate(Vector3.down * FallSpeed * Time.deltaTime);
    }

    void MoveNormal()
    {
        if( Input.GetKeyDown(KeyCode.Space))
            dash = true;
    }

    private void Update()
    {
        dashElapsedTime += Time.deltaTime; // フレームごとに経過時間を更新
        //スペースでジャンプ
        if (Input.GetKeyDown (KeyCode.Space))
        {
            if(isground == true) jump = true;
            else if(isground == false&& dash == false) 
            MoveDash();
        }

        if (dash && dashElapsedTime > 0.2f)
        {
            dash = false;
            fall = true;
        }

        if (jump)
        { 
            this.rigid2D = GetComponent<Rigidbody2D>();
            rigid2D.velocity = Vector2.zero;

            //ジャンプさせる
            this.rigid2D.AddForce(transform.up * Jump_Power);

            jump = false;
            isground = false;
            Debug.Log("ジャンプ");
        }

        if (GetComponent<Rigidbody2D>().IsTouching(filter2D))
        {
            //地面についたら終わったらもう一回できるようにする
            JumpCount = 1;
        }
        Jump_Count = Mathf.Max(0, Jump_Count);

        // 追加: ジャンプ中かつダッシュ中でない場合にのみ落下処理
        if (!dash && !isground && !fall)
        {
            fallElapsedTime += Time.deltaTime;
            if (fallElapsedTime > Fall_Duration)
            {
                fall = true;
            }
        }

        if (fall)
        {
            Fall() ; 
            Debug.Log("落下");
        }

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        //HitBoxもしくはEnemyに触れるとプレイヤーの破壊
        if(other.gameObject.tag == "HitBox" || other.gameObject.tag == "Enemy ")
        {
            //プレイヤーを消去
            Destroy(this.gameObject);
            //ゲームオーバーテキストの表示
            //IwasaのところにPrefabとしてゲームオーバーテキストお用意したのでInspecterに入れてください
            text.SetActive(true);
        }

        //ジャンプのオンオフについて使用
        if (other.gameObject.tag == "Ground")
        {
            isground = true;
            dash = false;
            fall = false;
            // 地面に着地したら落下経過時間をリセット
            fallElapsedTime = 0.0f; 
            //地面に着地したらダッシュを停止
             rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
        }
    }
}
