using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PickupDisplay : MonoBehaviour
{
    public GameObject item;
    public float spawntime = 1f;
    public bool itemDisplayed;
    public Vector3 offset = new Vector3(0, 2, 0);


    GameObject spawnedItem;
    float time = 0f;


    private void Update()
    {
        if (itemDisplayed)
        {
            time += Time.deltaTime;
            spawnedItem.transform.eulerAngles = new Vector3(0, -90, 0);

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
        if (spawnedItem != null)
            Destroy(spawnedItem);
        var newItem = Instantiate(item, transform);
        newItem.transform.position = offset + transform.position;
        newItem.transform.eulerAngles = new Vector3(0, -90, 0);
        spawnedItem = newItem;
        itemDisplayed = true;
    }
}
