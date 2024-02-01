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
    bool isClear = false;
    public bool GetIsClear => isClear;

    public AudioSource jumpAudioSource;//ジャンプの音
    public AudioSource dashAudioSource;//ダッシュの音

    Rigidbody2D rigid2D;
    [SerializeField] ContactFilter2D filter2D;

    private float dashElapsedTime = 0.0f;//ダッシュが始まってからの落下までの猶予
    private float fallElapsedTime = 0.0f; // 落下開始からの経過時間
    [SerializeField] float Dash_Distance;//ダッシュ距離10~20ぐらいがオススメ
    [SerializeField] float Jump_Power;//ジャンプ力500ぐらいがオススメ
    [SerializeField] private int Jump_Count = 1;//ジャンプ可能回数
    [SerializeField] float Fall_Duration = 2.0f; // 落下開始からの持続時間
    [SerializeField] float FallSpeed = 0.1f;//落下速度
    [SerializeField] float DefaultGravityScale = 10.0f;
    //動く床の速度加速用
    Vector2 jumpVec = Vector2.zero;
    Vector2 addVelocity = Vector2.zero;
    bool onMoveFloor = false;//動く床かどうか
    bool jumpDuring = false;//ジャンプ中かどうか
    MovingFloor floorScript = null;

    void MoveDash()//ジャンプ中にSpeace押した時のダッシュ処理
    {

        dash = true;
        dashElapsedTime = 0.0f;
        fallElapsedTime = 0.0f; // ダッシュが始まったので落下経過時間をリセット

        rigid2D.velocity = new Vector2(Dash_Distance / 0.5f, 0); // Dash_Distance / 0.5f はダッシュにかかる時間を示しています
        rigid2D.gravityScale = 0.0f;

        dashAudioSource.Play();
        Debug.Log ("横移動");
    }

    //落下処理
    void Fall()
    {
        rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
        //rigid2D.velocity = new Vector2(0, -FallSpeed);
        rigid2D.gravityScale = DefaultGravityScale;
        Debug.Log("落下");
    }

    void MoveNormal()
    {
        if( Input.GetKeyDown(KeyCode.Space))
            dash = true;
    }
    private void Awake()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        rigid2D.gravityScale = DefaultGravityScale;
    }
    private void Update()
    {
        dashElapsedTime += Time.deltaTime; // フレームごとに経過時間を更新
        //スペースでジャンプ
        if (Input.GetKeyDown (KeyCode.Space))
        {
            if(isground == true) jump = true;
            else if (!isground && !dash)
                MoveDash();
        }

        if (dash && dashElapsedTime > 0.5f)
        {
            dash = false;
            fall = true;
        }

        if (jump)
        { 
            jumpDuring = true;
            rigid2D.velocity = Vector2.zero;
            jumpVec =transform.up * Jump_Power;//引数を保存した変数に変更

            jumpAudioSource.Play();

            //ジャンプさせる
            this.rigid2D.AddForce(transform.up * Jump_Power);
            if(onMoveFloor)
            {
                rigid2D.AddForce(jumpVec + addVelocity);
            } 
            else
            {
                rigid2D.AddForce(jumpVec);
            }

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

        // ジャンプ中かつダッシュ中でない場合にのみ落下処理
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

        //動く床が参照されている時
        if(floorScript != null)
        {
            addVelocity = new Vector2(floorScript.GetVelocity.x, floorScript.GetVelocity.y);
            //動く床の上でジャンプしてないとき
            if(!jumpDuring)
            {
                rigid2D.velocity =new Vector2(0,0) + addVelocity;
            }
        }

        EndGame();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ClearLine"))
        {
            isClear = true;
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
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "MoveFloor")
        {
            isground = true;
            dash = false;
            fall = false;
            jumpDuring = false;
            // 地面に着地したら落下経過時間をリセット
            fallElapsedTime = 0.0f;
            //地面に着地したらダッシュを停止
            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);

            if(other.gameObject.tag == "MoveFloor") 
            {
                floorScript = other.gameObject.GetComponent<MovingFloor>();
                onMoveFloor = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("MoveFloor"))
        {
            Debug.Log("床から離れた");
            floorScript = null;
            onMoveFloor = false;
        }
    }

    private void EndGame()
    {
        if(Input.GetKey(KeyCode.Exclaim))
        {
            
        }
    }
}
