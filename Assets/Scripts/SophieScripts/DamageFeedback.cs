using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    Renderer[] myMaterials;
    public float fadeTime = 2f;
    public float fadeLevelMultiplier = 1;
    public float fadeDamageMultiplier = 1;
    [ColorUsage(true, true)]
    public Color levelColor;
    [ColorUsage(true, true)]
    public Color damageColor;


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
    public void DamagedFeedback()
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
