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
    [SerializeField] float Dash_Distance;//�_�b�V������150~250���炢���I�X�X��
    private bool jump = false;
    private bool isground;
    Rigidbody2D rigid2D;
    [SerializeField] ContactFilter2D filter2D;
    bool dash = false;
  
    public bool GetDash => dash;
    public GameObject text;
    
    
    void MoveDash()//�W�����v����Speace���������̃_�b�V������
    {
        dash = true;
        /*transform.position += new Vector3(3f, 0, 0);*/
        Rigidbody2D rd = GetComponent<Rigidbody2D>();
        rd.bodyType = RigidbodyType2D.Kinematic;
        this.rigid2D.AddForce(transform.right * Dash_Distance);
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
        //HitBox��������Enemy�ɐG���ƃv���C���[�̔j��
        if(other.gameObject.tag == "HitBox" || other.gameObject.tag == "Enemy ")
        {
            //�v���C���[������
            Destroy(this.gameObject);
            //�Q�[���I�[�o�[�e�L�X�g�̕\��
            //Iwasa�̂Ƃ����Prefab�Ƃ��ăQ�[���I�[�o�[�e�L�X�g���p�ӂ����̂�Inspecter�ɓ���Ă�������
            text.SetActive(true);
        }

        //�W�����v�̃I���I�t�ɂ��Ďg�p
        if (other.gameObject.tag == "Ground")
        {
            isground = true;
            dash = false;
        }
    }
}
