using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    Renderer myMaterial;
    public float tintFadeSpeed = 6f;
    Color damageColor = Color.black;
    private void Awake()
    {
        myMaterial = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (myMaterial.material.color == damageColor)
        {
            if (damageColor.a > 0)
            {
                damageColor.a -= tintFadeSpeed * Time.deltaTime;
            }
        }
        Debug.Log(damageColor.a);
    }
    public void Feedback()
    {
        myMaterial.material.SetColor("_Color", damageColor);
     
    }

}
