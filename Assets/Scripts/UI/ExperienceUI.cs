using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ExperienceUI : MonoBehaviour
{
    public TMP_Text levelText;
    public Image image;
    public PlayerLevel exp;

    private void Start()
    {
        var player = PlayerController.playerController;
        if(player == null)
        {
            Destroy(this);
            return;
        }
        exp = player.GetComponent<PlayerLevel>();
        if(exp == null)
        {
            Destroy(this);
            return;
        }
        exp.onExpChanged.AddListener(UpdateAmount);
        UpdateAmount(exp.Experience);
        exp.onLevelChanged.AddListener(UpdateText);
        UpdateText(exp.level);
    }

    public void UpdateAmount(float experience)
    {
        image.fillAmount = Mathf.InverseLerp(0, this.exp.RequiredExp, experience);
    }

    public void UpdateText(int level)
    {
        levelText.SetText($"Player Level {level}");
    }
}
