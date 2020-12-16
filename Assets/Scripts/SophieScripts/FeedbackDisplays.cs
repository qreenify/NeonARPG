using UnityEngine;

public class FeedbackDisplays : MonoBehaviour
{
    Renderer[] myMaterials;
    public float fadeTime = 0.5f;
    public float fadeLevelMultiplier = 1;
    public float fadeDamageMultiplier = 1;
    [ColorUsage(true, true)]
    public Color levelColor = new Color(0, 200/255f, 0) * 32;
    [ColorUsage(true, true)]
    public Color damageColor = new Color(200/255f, 0, 0) * 32;


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
            material.material.SetColor("_PulseColor", damageColor);
            material.material.SetFloat("_DamageAmount", Amount);

        }
    }

    public void LevelUpFeedback()
    {
        Amount = fadeLevelMultiplier;

        foreach (var material in myMaterials)
        {
            material.material.SetColor("_PulseColor", levelColor);
            material.material.SetFloat("_DamageAmount", Amount);

        }
    }

}
