using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerContorller : MonoBehaviour
{
    //カメラにaddしてください
    //addしたらプレイヤーの下に入れてください
    float y = 0.0f;
    [SerializeField] GameObject player;
    Jump_Dash playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Jump_Dash>();
        y = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerScript.GetIsDead) return;
        float x = player.transform.position.x;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

    }
}
