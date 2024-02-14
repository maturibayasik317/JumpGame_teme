using UnityEngine;
using UnityEngine.UI;

//-----�X�e�[�W�Z���N�g��ʂ̃R�C��-----
public class StageSelectCoinUI : MonoBehaviour
{
    [SerializeField] Image[] coinImages;
    [SerializeField] Sprite coinSprite;
    private void OnEnable()
    {
        // �V���O���g�������p�i�Q�ƂȂǂ�����ɂ��Ă����j
        var coinManager = CoinManager.Instance;

        // �X�e�[�W1
        for (int i = 0; i < coinManager.IsPlayerCoin_Stage1.Length; i++)
        {
            if (coinManager.IsPlayerCoin_Stage1[i]) coinImages[i].sprite = coinSprite;
        }
        // �X�e�[�W2
        for (int i = 0; i < coinManager.IsPlayerCoin_Stage2.Length; i++)
        {
            if (coinManager.IsPlayerCoin_Stage2[i]) coinImages[i + 3].sprite = coinSprite;
        }
        // �X�e�[�W3
        for (int i = 0; i < coinManager.IsPlayerCoin_Stage3.Length; i++)
        {
            if (coinManager.IsPlayerCoin_Stage3[i]) coinImages[i + 6].sprite = coinSprite;
        }
        // �X�e�[�W4
        for (int i = 0; i < coinManager.IsPlayerCoin_Stage4.Length; i++)
        {
            if (coinManager.IsPlayerCoin_Stage4[i]) coinImages[i + 9].sprite = coinSprite;
        }
    }
}
