using UnityEngine;

public class FeedbackDisplays : MonoBehaviour
{
    Renderer[] myMaterials;
    public float fadeTime = 0.5f;
    public float fadeLevelMultiplier = 1;
    public float fadeDamageMultiplier = 1;
    [ColorUsage(true, true)]
    public Color levelUpColor = new Color(0, 1, 0) * 256;
    [ColorUsage(true, true)]
    public Color damageFeedback = new Color(1, 0, 0) * 256;


    float Amount = 0f;
  
    private void Awake()
    {
        
        myMaterials = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        if (Amount > 0)
        {
            Amount -= Time.deltaTime / fadeTime;

            foreach (var material in myMaterials)
            {
                material.material.SetFloat("_DamageAmount", Amount);

            }
        }
    }
    public void DamageFeedback()
    {
        Amount = fadeDamageMultiplier;

        foreach (var material in myMaterials)
        {
            material.material.SetColor("_PulseColor", damageFeedback);
            material.material.SetFloat("_DamageAmount", Amount);

        }
    }

    public void LevelUpFeedback()
    {
        Amount = fadeLevelMultiplier;

        foreach (var material in myMaterials)
        {
            material.material.SetColor("_PulseColor", levelUpColor);
            material.material.SetFloat("_DamageAmount", Amount);

        }
    }

}
