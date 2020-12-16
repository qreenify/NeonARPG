using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackModeUI : MonoBehaviour
{
    TextMeshProUGUI tmp;
    PlayerController player;
    public GameObject meleeModeOn;
    public GameObject meleeModeOff;
    public GameObject rangedModeOn;
    public GameObject rangedModeOff;


    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        player = PlayerController.playerController;
    }
    private void FixedUpdate()
    {
        UpdateAttackModeText();
    }
    void UpdateAttackModeText()
    {
        if(player == null)
        {
            return;
        }
        if (player.ranged)
        {
            //tmp.text = $"Attack Mode:\nRanged";
            meleeModeOn.SetActive(false);
            meleeModeOff.SetActive(true);
            rangedModeOff.SetActive(false);
            rangedModeOn.SetActive(true);
        }
        else
        {
            //tmp.text = $"Attack Mode:\nMelee";
            meleeModeOn.SetActive(true);
            meleeModeOff.SetActive(false);
            rangedModeOff.SetActive(true);
            rangedModeOn.SetActive(false);
        }
    }
}
