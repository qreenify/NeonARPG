using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDisplay : MonoBehaviour
{
    public GameObject item;
    public float spawntime = 1f;
    public bool itemDisplayed;


    GameObject spawnedItem;
    float time = 0f;


    private void Update()
    {
        if (itemDisplayed)
        {
            time += Time.deltaTime;

            if (time > spawntime)
            {
                Destroy(spawnedItem);
                time -= spawntime;
                itemDisplayed = false;
            }

        }
    }
    public void PickupConfirmation()
    {
        var newItem = Instantiate(item, transform.position, transform.rotation);
        newItem.transform.SetParent(transform);
        spawnedItem = newItem;
        itemDisplayed = true;

    }
}
