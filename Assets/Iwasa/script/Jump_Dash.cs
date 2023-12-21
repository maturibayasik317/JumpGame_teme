using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump_Dash : MonoBehaviour
{   //�v���C���[�ɃR���|�[�l���g���Ă�������
    private Vector2 velocity;
    [SerializeField] float Jump_Power;//�W�����v��500���炢���I�X�X��
    [SerializeField] private int Jump_Count =1;//�W�����v�\��
    private int JumpCount = 0;//�W�����v�I�����̏���
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
        Debug.Log ("���ړ�");
    }


    void MoveNormal()
    {
        if( Input.GetKeyDown(KeyCode.Space))
            dash = true;
    }

    private void Update()
    {
        //�X�y�[�X�ŃW�����v
        if (Input.GetKeyDown (KeyCode.Space))
        {
            if(isground == true) jump = true;
            else if(isground == false&& dash == false) MoveDash();
        }

        if (jump)
        { 
            this.rigid2D = GetComponent<Rigidbody2D>();
            rigid2D.velocity = Vector2.zero;

            //�W�����v������
            this.rigid2D.AddForce(transform.up * Jump_Power);

            jump = false;
            isground = false;
            Debug.Log("�W�����v");
        }

        if (GetComponent<Rigidbody2D>().IsTouching(filter2D))
        {
            //�n�ʂɂ�����I�������������ł���悤�ɂ���
            JumpCount = 1;
        }
        Jump_Count = Mathf.Max(0, Jump_Count);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //HitBox�ɐG���ƃv���C���[�̔j��
        if(other.gameObject.tag == "HitBox")
        {
            //�v���C���[������
            Destroy(this.gameObject);
            //�Q�[���I�[�o�[�e�L�X�g�̕\��
            //Iwasa�̂Ƃ����Prefab�Ƃ��ăQ�[���I�[�o�[�e�L�X�g���p�ӂ����̂�Inspecter�ɓ���Ă�������
            text.SetActive(true);
        }
        if (other.gameObject.tag == "Ground")
        {
            isground = true;
            dash = false;
        }
    }
}
