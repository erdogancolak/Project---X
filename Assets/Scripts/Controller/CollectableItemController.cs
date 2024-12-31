using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableItemController : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private GameObject item;

    [SerializeField] private GameObject infoText;

    [Space]

    [Header("Settings")]

    [SerializeField] private string PlayerTag;

    private bool isCharacterInside;

    private void Update()
    {
        if(isCharacterInside && Input.GetKeyDown(KeyCode.U)) 
        {
            item.GetComponent<Image>().color = Color.white;
            item.GetComponent<Button>().enabled = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(PlayerTag))
        {
            isCharacterInside = true;
            infoText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(PlayerTag))
        {
            isCharacterInside = false;
            infoText.SetActive(false);
        }
    }

}
