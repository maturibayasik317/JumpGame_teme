using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----セレクト画面のUI制御----
public class SelectSceneUI : MonoBehaviour
{
    CoinManager coinManagerScript;

    void Start()
    {
        coinManagerScript = GameObject.FindWithTag("UI").GetComponent<CoinManager>();
    }

    void Update()
    {
        
    }
}
