using UnityEngine;
using UnityEngine.UI;

//-----ステージセレクト画面のコイン-----
public class StageSelectCoinUI : MonoBehaviour
{
    [SerializeField] Image[] coinImages;
    [SerializeField] Sprite coinSprite;
    private void OnEnable()
    {
        // シングルトンを活用（参照などを勝手にしてくれる）
        var coinManager = CoinManager.Instance;

        // ステージ1
        for (int i = 0; i < coinManager.IsPlayerCoin_Stage1.Length; i++)
        {
            if (coinManager.IsPlayerCoin_Stage1[i]) coinImages[i].sprite = coinSprite;
        }
        // ステージ2
        for (int i = 0; i < coinManager.IsPlayerCoin_Stage2.Length; i++)
        {
            if (coinManager.IsPlayerCoin_Stage2[i]) coinImages[i + 3].sprite = coinSprite;
        }
        // ステージ3
        for (int i = 0; i < coinManager.IsPlayerCoin_Stage3.Length; i++)
        {
            if (coinManager.IsPlayerCoin_Stage3[i]) coinImages[i + 6].sprite = coinSprite;
        }
        // ステージ4
        for (int i = 0; i < coinManager.IsPlayerCoin_Stage4.Length; i++)
        {
            if (coinManager.IsPlayerCoin_Stage4[i]) coinImages[i + 9].sprite = coinSprite;
        }
    }
}
