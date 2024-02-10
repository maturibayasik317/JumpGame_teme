using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----ギミックのテスト用----
public class PlayerTest : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid2D;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    float xSpeed;
    float ySpeed;

    // 出来るかどうか
    bool dash = false;
    bool jump = false;

    bool isClear = false;

// 動く床の速度加算用の変数

    // rb.velocity用
    Vector2 jumpVec = Vector2.zero;
    // 加算する移動速度
    Vector2 addVelocity = Vector2.zero;

    bool onMoveFloor = false; // 動く床の上かどうか
    bool jumpDuring = false; // ジャンプ中かどうか

    MovingFloor floorSprict = null; // 動く床のスクリプト

//

    bool isground = false;

    // 読み取り専用プロパティ
    public bool GetDash => dash;
    public bool GetIsClear => isClear;

    void Update()
    {
        if (isground)
        {
            jump = true;
        }
        // ジャンプ処理
        if (jump && Input.GetKeyDown(KeyCode.Space))
        {
// ジャンプ処理部分に追記（変更）

            jumpDuring = true;
            // (AddForceの中身)を変数「jumpVec」として保存
            jumpVec = transform.up * jumpPower;
            // 引数を保存した変数に変更
            if (onMoveFloor)
            {
                rigid2D.AddForce(jumpVec + addVelocity);
            }
            else
            {
                rigid2D.AddForce(jumpVec);
            }
//
            jump = false;
            isground = false;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            dash = true;
        }
        else
        {
            dash = false;
        }
        
// Update()の最後に追記

        // 動く床が参照されているとき
        if (floorSprict != null)
        {
            // x成分とy成分をそれぞれ取得
            addVelocity = new Vector2(floorSprict.GetVelocity.x, floorSprict.GetVelocity.y);
            
            // 動く床の上でジャンプ入力していないとき
            if (!jumpDuring)
            {
                rigid2D.velocity = new Vector2(0, 0) + addVelocity;
            }
        }
//
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //// 動く床に乗っているとき
        //if (collision.gameObject.CompareTag("MoveFloor"))
        //{
        //    floorSprict = collision.gameObject.GetComponent<MovingFloor>();
        //}

        //// ジャンプできる
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    jump = true;
        //}

// ジャンプ判定部分に追記

        //ジャンプのオンオフについて使用
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MoveFloor")
        {
            isground = true;
            dash = false;
            //fall = false;

            // 追記
            jumpDuring = false;
            
            // 地面に着地したら落下経過時間をリセット
            //fallElapsedTime = 0.0f;
            //地面に着地したらダッシュを停止
            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);

            if (collision.gameObject.tag == "MoveFloor")
            {
                Debug.Log("床に乗った");
                // 動く床のスクリプト取得
                floorSprict = collision.gameObject.GetComponent<MovingFloor>();
                onMoveFloor = true;
            }
        }
        //

        if (collision.gameObject.CompareTag("HitBox") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("ゲームオーバー");
        }
    }


//
    void OnCollisionExit2D(Collision2D collision)
    {
        // 動く床に乗っていないとき
        if (collision.gameObject.CompareTag("MoveFloor"))
        {
            Debug.Log("床から離れた");
            floorSprict = null;
            onMoveFloor = false;
        }
    }
//

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HitBox"))
        {
            Destroy(gameObject);
            Debug.Log("ゲームオーバー");
        }
        if (collision.gameObject.CompareTag("ClearLine"))
        {
            isClear = true;
        }
    }
}
