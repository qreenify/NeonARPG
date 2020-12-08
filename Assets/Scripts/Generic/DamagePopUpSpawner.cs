using UnityEngine;

public class DamagePopUpSpawner : MonoBehaviour
{
    public static void Create(Transform transform, float damageAmount)
    {
        var damagePopupTransform = Instantiate(GameAssets.damagePopup, transform.position, Camera.main.transform.rotation);
        //damagePopupTransform.transform.Rotate(0, 180, 0);
        
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        bool isCriticalHit = Random.Range(0, 100) < 30;
        damagePopup.Setup(transform.position, damageAmount, isCriticalHit);

        //return damagePopup;
    }
}
