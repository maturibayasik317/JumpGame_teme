using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump_Dash : MonoBehaviour
{   //プレイヤーにコンポーネントしてください
    private Vector2 velocity;
    [SerializeField] float Jump_Power;//ジャンプ力500ぐらいがオススメ
    [SerializeField] private int Jump_Count =1;//ジャンプ可能回数
    private int JumpCount = 0;//ジャンプ終了時の処理
    private bool jump = false;
    private bool isground;
    Rigidbody2D rigid2D;
    [SerializeField] ContactFilter2D filter2D;
    bool dash = false;

    public bool GetDash => dash;


    public GameObject text;

    void MoveDash()
    {
        dash = true;
        transform.position += new Vector3(3f, 0, 0);
        Debug.Log ("横移動");
    }


    void MoveNormal()
    {
        if( Input.GetKeyDown(KeyCode.Space))
            dash = true;
    }

    private void Update()
    {
        //スペースでジャンプ
        if (Input.GetKeyDown (KeyCode.Space))
        {
            if(isground == true) jump = true;
            else if(isground == false&& dash == false) MoveDash();
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //HitBoxに触れるとプレイヤーの破壊
        if(other.gameObject.tag == "HitBox")
        {
            //プレイヤーを消去
            Destroy(this.gameObject);
            //ゲームオーバーテキストの表示
            //IwasaのところにPrefabとしてゲームオーバーテキストお用意したのでInspecterに入れてください
            text.SetActive(true);
        }
        if (other.gameObject.tag == "Ground")
        {
            isground = true;
            dash = false;
        }
    }
}
