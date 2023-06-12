using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupAmount : MonoBehaviour
{
    [SerializeField] private GameObject _amountBox;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Text _amountText;
    [SerializeField] public int objectType;


    void Start()
    {
        _amountText.gameObject.SetActive(false);
    }


    void Update()
    {
        objectType = PlayerPrefs.GetInt(gameObject.name + "ObjecType", 0);
        if (objectType != 0)
            _amountText.gameObject.SetActive(true);
        MessageDisplay();
    }

    private void MessageDisplay()
    {
        // Se souvient de la sprite
        _iconImage.sprite = InventoryItems.instance.icons[objectType];

        if (objectType == 1)
        {
            _amountText.text = InventoryItems.mushrooms.ToString();
        }
        if (objectType == 2)
        {
            _amountText.text = InventoryItems.redFlowers.ToString();
        }
        if (objectType == 3)
        {
            _amountText.text = InventoryItems.blueFlowers.ToString();
        }
        if (objectType == 4)
        {
            _amountText.text = InventoryItems.bluePotion.ToString();
        }
        if (objectType == 5)
        {
            _amountText.text = InventoryItems.redPotion.ToString();
        }
    }
}
