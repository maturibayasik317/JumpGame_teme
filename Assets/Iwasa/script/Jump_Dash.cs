using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump_Dash : MonoBehaviour
{   //�v���C���[�ɃR���|�[�l���g���Ă�������
    private Vector2 velocity;
    [SerializeField] float Jump_Power;//�W�����v��
    private int Jump_Count =1;
    private int JumpCount = 0;
    private bool jump = false;
    Rigidbody2D rigid2D;
    [SerializeField] ContactFilter2D filter2D;

    bool dash = false;


    public GameObject text;

    void MoveDash()
    {
        //������else�ɂ���if(�W�����v���ɃX�y�[�X�������Ƃ��Ċ����ɏ�������)
        if(Input.GetKey(KeyCode.Space)) 
        { 
            transform.position+=  new Vector3(0.1f,0,0);
        }
        dash = false;
    }

    void MoveNormal()
    {
        if( Input.GetKeyDown(KeyCode.Space))
            dash = true;
    }

    private void Update()
    {
        //�X�y�[�X�ŃW�����v
        if ( JumpCount < Jump_Count && Input.GetKeyDown (KeyCode.Space))
        {
            jump = true;
        }
        if (jump)
        { 
            this.rigid2D = GetComponent<Rigidbody2D>();
            rigid2D.velocity = Vector2.zero;

            //�W�����v������
            this.rigid2D.AddForce(transform.up * Jump_Power);
            JumpCount++;

            jump = false;
        }
        if (dash)//�_�b�V���ړ�
        {
            MoveDash();
        }
        else//�ʏ���
        {
            MoveNormal();
        }
        if (GetComponent<Rigidbody2D>().IsTouching(filter2D))
        {
            //�n�ʂɂ�����I�������������ł���悤�ɂ���
            JumpCount = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "HitBox")
        {
            //�v���C���[������
            Destroy(this.gameObject);
            //�Q�[���I�[�o�[�e�L�X�g�̕\��
            //Iwasa�̂Ƃ����Prefab�Ƃ��ăQ�[���I�[�o�[�e�L�X�g���p�ӂ����̂�Inspecter�ɓ���Ă�������
            text.SetActive(true);
        }
    }
}
