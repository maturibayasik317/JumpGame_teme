using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    //�w�i�Ƃ��̓������������̂ɓ���Ă�������
    [SerializeField] private float speed;

    Vector3  Startpos;

    private void Start()
    {
        Startpos = transform.position;
    }


    void Update()
    {
        //�w�i�������̓O���E���h�ɃR���|�[�l���g���Ƃ��āASerialize�Őݒ肵������X�s�[�h�̐ݒ���Y�ꂸ��
        transform.position -= new Vector3(Time.deltaTime * speed, Startpos.y);
        if(transform.position.x <= -8.9f);

    }
}
