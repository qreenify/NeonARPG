using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    Renderer[] myMaterials;
    public float FadeTime = 2f;

    float DamageAmount = 0;
     
    private void Awake()
    {
        
        myMaterials = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        if (DamageAmount > 0)
        {
            DamageAmount -= Time.deltaTime / FadeTime;

            foreach (var material in myMaterials)
            {
                material.material.SetFloat("_DamageAmount", DamageAmount);

            }
        }
    }
    public void Feedback()
    {
        DamageAmount = 1;

        foreach (var material in myMaterials)
        {
            material.material.SetFloat("_DamageAmount", DamageAmount);

        }

    }

}
