using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----ギミックのテスト用----
public class PlayerTest : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    float xSpeed;
    float ySpeed;

    // 出来るかどうか
    bool dash = false;
    bool jump = false;
    bool fall = false;

    // rb.velocity用
    Vector2 jumpForce;
    Vector2 fallForce;

    bool jumpDuring = false; // ジャンプ中かどうか
    bool fallDuring = false;

    MovingFloor floorSprict = null; // 動く床のスクリプト

    // 読み取り専用プロパティ
    public bool GetDash => dash;

    void Start()
    {
        
    }

    private void Update()
    {
        if (jump && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
//
            jumpDuring = true;
            // 保存
            jumpForce = Vector2.up * jumpPower;
            rb.AddForce(jumpForce);
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

        Vector2 addVelocity = Vector2.zero; // 加算する移動速度
        

        // 動く床が参照されているとき
        if (floorSprict != null)
        {
            // x成分とy成分をそれぞれ取得
            addVelocity = new Vector2(floorSprict.GetVelocity.x, floorSprict.GetVelocity.y);
            
            // ジャンプ、落下状態をそれぞれ参照

            if (jumpDuring) // ジャンプ中はジャンプ力も参照させる
            {
                rb.velocity = new Vector2(0, jumpForce.y) + addVelocity;
            }
/*            else if (fallDuring) // 落下時
            {
                rb.velocity = new Vector2(0, fallForce.y) + addVelocity;
            }
*/
            else // 通常時
            {
                rb.velocity = new Vector2(0, 0) + addVelocity;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 動く床に乗っているとき
        if (collision.gameObject.CompareTag("MoveFloor"))
        {
            floorSprict = collision.gameObject.GetComponent<MovingFloor>();
        }

        // ジャンプできる
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MoveFloor"))
        {
           jump = true;
        }
        else
        {
            jump = false;
        }

        if (collision.gameObject.CompareTag("HitBox"))
        {
            Destroy(gameObject);
            Debug.Log("ゲームオーバー");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 動く床に乗っていないとき
        if (collision.gameObject.CompareTag("MoveFloor"))
        {
            floorSprict = null;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HitBox"))
        {
            Destroy(gameObject);
            Debug.Log("ゲームオーバー");
        }
    }
}
