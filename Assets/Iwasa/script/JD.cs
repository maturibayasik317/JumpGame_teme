using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JD : MonoBehaviour
{
    private int Jump_Count = 1;
    private int JumpCount = 0;
    private bool jump = false;
    private bool dash = false;
    private Rigidbody2D rigid2D;

    [SerializeField] float Jump_Power;
    [SerializeField] ContactFilter2D filter2D;

    public GameObject text;

    void MoveDash()
    {
        // スペースを押してもジャンプもダッシュも反応させる
        transform.position += new Vector3(5f, 0, 0);
        dash = false;
    }

    void MoveNormal()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dash = true;
        }
    }

    private void Update()
    {
        // スペースでジャンプ
        if (JumpCount < Jump_Count && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (jump)
        {
            this.rigid2D = GetComponent<Rigidbody2D>();
            rigid2D.velocity = Vector2.zero;

            // ジャンプさせる
            this.rigid2D.AddForce(transform.up * Jump_Power);
            JumpCount++;

            // jump フラグを true に設定するタイミングを変更
            // ジャンプ中にスペースを押すとダッシュも反応させる
            jump = Input.GetKey(KeyCode.Space);

            // スペースを押してもジャンプもダッシュも反応させる
            if (dash)
            {
                MoveDash();
            }
        }
        else // 通常状態
        {
            MoveNormal();
        }

        if (GetComponent<Rigidbody2D>().IsTouching(filter2D))
        {
            // 地面についたら終わったらもう一回できるようにする
            JumpCount = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "HitBox")
        {
            // プレイヤーを消去
            Destroy(this.gameObject);
            // ゲームオーバーテキストの表示
            text.SetActive(true);
        }
    }
}
