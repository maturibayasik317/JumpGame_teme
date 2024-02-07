using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//----ÉZÉåÉNÉgâÊñ ÇÃUIêßå‰----
public class SelectSceneUI : MonoBehaviour
{
    CoinManager coinManagerScript;

    void Start()
    {
        coinManagerScript = GameObject.Find("CoinManager").GetComponent<CoinManager>();
    }

    void Update()
    {
        
    }
}
