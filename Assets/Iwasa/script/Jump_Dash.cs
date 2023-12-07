using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Dash : MonoBehaviour
{
    private Vector2 velocity;
    [SerializeField] float Jump_Power;//ジャンプ力
    private int Jump_Count =1;
    private int JumpCount = 0;
    private bool jump = false;
    Rigidbody2D rigid2D;
    [SerializeField] ContactFilter2D filter2D;

    private void Update()
    {
        if ( JumpCount < Jump_Count && Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }
        if (jump)
        { 
            this.rigid2D = GetComponent<Rigidbody2D>();
            rigid2D.velocity = Vector2.zero;

            //ジャンプさせる
            this.rigid2D.AddForce(transform.up * Jump_Power);
            JumpCount++;

            jump = false;
        }
        if (GetComponent<Rigidbody2D>().IsTouching(filter2D))
        {
            //地面についたら終わったらもう一回できるようにする
            JumpCount = 0;
        }
    }
}
