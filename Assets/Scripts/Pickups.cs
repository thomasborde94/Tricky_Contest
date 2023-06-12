using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickups : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] private bool isCoin;
    [SerializeField] private bool isGold;
    [SerializeField] private bool isGlasses;
    [SerializeField] private bool isItem;
    [SerializeField] private bool isMushroom;
    [SerializeField] private bool isRedFlower;
    [SerializeField] private bool isBlueFlower;
    [SerializeField] private float _waitToBeCollected;

    [SerializeField] private int _arrayNumber;

    public IntVariable _mushroomAmount;
    public IntVariable _redFlowerAmount;
    public IntVariable _blueFlowerAmount;

    #endregion

    #region Unity Lifecycle


    void Update()
    {
        if (_waitToBeCollected > 0)
            _waitToBeCollected -= Time.deltaTime;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerPickup") && _waitToBeCollected <= 0)
        {
            if (isCoin)
            {
                LevelManaging.instance.GetCoin();
                Destroy(gameObject);
                SFXManager.instance.PlaySFX(3);
            }
            if (isGold)
            {
                LevelManaging.instance.GetGold();
                Destroy(gameObject);
                SFXManager.instance.PlaySFX(3);
            }
            if (isItem)
            {
                SFXManager.instance.PlaySFX(4);
                if (isMushroom)
                {
                    if (InventoryItems.mushrooms == 0)
                    {
                        DisplayIcons();
                    }
                    _mushroomAmount.Value++;
                }
                else if (isRedFlower)
                {
                    if (InventoryItems.redFlowers == 0)
                    {
                        DisplayIcons();
                    }
                    _redFlowerAmount.Value++;
                }
                else if (isBlueFlower)
                {
                    if (InventoryItems.blueFlowers == 0)
                    {
                        DisplayIcons();
                    }
                    _blueFlowerAmount.Value++;
                }
                else
                    DisplayIcons();

                Destroy(gameObject);
            }
        }
    }

    
    private void DisplayIcons()
    {
        InventoryItems.newIcon = _arrayNumber;
        InventoryItems.iconUpdate = true;
    }

}
