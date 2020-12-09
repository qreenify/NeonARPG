using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackModeUI : MonoBehaviour
{
    TextMeshProUGUI tmp;
    PlayerController player;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        player = PlayerController.playerController;
    }
    private void FixedUpdate()
    {
        UpdateAttackModeText();
    }
    public void UpdateAttackModeText()
    {
        if (player.ranged)
        {
            tmp.text = $"Attack Mode:\nRanged";
        }
        else
        {
            tmp.text = $"Attack Mode:\nMelee";
        }
    }
}
