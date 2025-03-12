using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerContorller : MonoBehaviour
{
    //カメラにaddしてください
    //addしたらプレイヤーの下に入れてください
    Vector3 offset = new Vector3(7.5f, 0, 0);
    float y = 0;
    float z = 0;
    [SerializeField] GameObject player;
    Jump_Dash playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Jump_Dash>();
        y = transform.position.y;
        z = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerScript.GetIsDead) return;
        float x = player.transform.position.x;
        transform.position = offset + new Vector3(x, y, z);

    }
}
