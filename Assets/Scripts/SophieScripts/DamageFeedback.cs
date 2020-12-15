using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    Renderer myMaterial;
    public float FadeTime = 2f;

    float DamageAmount = 0;
     
    private void Awake()
    {
        myMaterial = GetComponent<Renderer>();
    }

    private void Update()
    {
        if(DamageAmount > 0)
        {
            DamageAmount -= Time.deltaTime/ FadeTime;
            myMaterial.material.SetFloat("_DamageAmount", DamageAmount);
        }
    }
    public void Feedback()
    {
        DamageAmount = 1;
        myMaterial.material.SetFloat("_DamageAmount", DamageAmount);
        //myMaterial.material.SetColor("_Color", damageColor);
     
    }

}
