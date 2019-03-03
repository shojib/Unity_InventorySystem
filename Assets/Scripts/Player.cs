using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{

    public GameObject inventorySlotsContainer;
    public Camera fpsCam;
    public float range = 4f;
    public GameObject inventoryItemsContainer;
    public GameObject inventorySlots;

    private Button[] slots;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        inventorySlotsContainer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool activeState = inventorySlotsContainer.gameObject.activeSelf;
            inventorySlotsContainer.gameObject.SetActive(!activeState);
            Cursor.lockState = activeState ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !activeState;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = activeState;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }
    }

    public void PickupItem()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.transform.CompareTag("Item"))
            {
                hit.transform.SetParent(inventoryItemsContainer.transform);
                AddtoInventory(hit.transform);
                Debug.Log("Hitting: " + hit.transform.name);
            }
        }
    }

    public void AddtoInventory(Transform item)
    {
        Debug.Log("Adding to inventory...");
        slots = inventorySlots.GetComponentsInChildren<Button>();

        for (int i = 0; i < slots.Length; i++)
        {
            RawImage rImg = slots[i].GetComponentInChildren<RawImage>();
            if (rImg.texture == null)
            {
                rImg.texture = item.GetComponent<RawImage>().texture;
                return;
            }
        }
    }
}
