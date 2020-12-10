using UnityEngine;

public class DamagePopUpSpawner : MonoBehaviour
{
    public static void Create(Transform transform, float damageAmount)
    {
        if (GameAssets.Instance == null)
        {
            Debug.LogError("Please Add GameAssets Prefab To Scene For DamagePopUp To Work");
            return;
        }
        var damagePopupTransform = Instantiate(GameAssets.Instance.damagePopup, transform.position, Camera.main.transform.rotation);
        //damagePopupTransform.transform.Rotate(0, 180, 0);
        
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        bool isCriticalHit = Random.Range(0, 100) < 0;
        damagePopup.Setup(transform.position, damageAmount, isCriticalHit);

        //return damagePopup;
    }
}
