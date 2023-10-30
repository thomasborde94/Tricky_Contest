using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItems : MonoBehaviour
{
    public static InventoryItems instance;
    #region Show in inspector

    [SerializeField] public Image[] emptySlots;
    [SerializeField] public Sprite[] icons;
    [SerializeField] Text[] _amountText;
    [SerializeField] private Sprite emptyIcon;
    [SerializeField] private Sprite _bluePotionIcon;
    [SerializeField] private Text _bluePotionAmount;
    [SerializeField] private Sprite _redPotionIcon;
    [SerializeField] private Text _redPotionAmount;

    public IntVariable _mushroomAmount;
    public IntVariable _redFlowerAmount;
    public IntVariable _blueFlowerAmount;
    public IntVariable _bluePotion;
    public IntVariable _redPotion;

    public static int newIcon = 0;
    public static bool iconUpdate = false;
    public static int mushrooms = 0;
    public static int blueFlowers = 0;
    public static int redFlowers = 0;
    public static int bluePotion = 0;
    public static int redPotion = 0;

    public static bool key = true;
    public static int gold = 300;

    #endregion

    #region Unity lifecycle

    void Start()
    {
        instance = this;
        max = emptySlots.Length;
    }
    private void Update()
    {
        mushrooms = _mushroomAmount.Value;
        redFlowers = _redFlowerAmount.Value;
        blueFlowers = _blueFlowerAmount.Value;
        // Happens when I pick up an item
        if (iconUpdate == true)
        {
            for (int i = 0; i < max; i++)
            {
                // Checks if I already have or had the item
                if (emptySlots[i].sprite == icons[newIcon])
                {
                    break;
                }
                // If I don't have it, add the sprite to the inventory
                if (emptySlots[i].sprite == emptyIcon)
                {
                    max = i;
                    // Sets the right sprite depending on the pickup. newIcon is setup in the Pickups script
                    emptySlots[i].sprite = icons[newIcon];
                    emptySlots[i].transform.gameObject.GetComponent<PickupAmount>().objectType = newIcon;
                    // PlayerPrefs, so the game remembers newIcon no matter the scene.
                    PlayerPrefs.SetInt(emptySlots[i].gameObject.name + "ObjecType", newIcon);
                }
            }
            // Turns iconUpdate to false and max to emptyslots.length
            StartCoroutine(Reset());
        }
        // Deals with the tea icons
        TeaChecker();
    }

    #endregion

    // Displays correct icons of tea in the inventory
    private void TeaChecker()
    {
        if (_bluePotion.Value != 0)
        {
            emptySlots[6].sprite = _bluePotionIcon;
            _bluePotionAmount.gameObject.SetActive(true);
            _bluePotionAmount.text = _bluePotion.Value.ToString();
        }
        else
        {
            emptySlots[6].sprite = emptyIcon;
            _bluePotionAmount.gameObject.SetActive(false);
        }
        if (_redPotion.Value != 0)
        {
            emptySlots[7].sprite = _redPotionIcon;
            _redPotionAmount.gameObject.SetActive(true);
            _redPotionAmount.text = _redPotion.Value.ToString();
        }
        else
        {
            emptySlots[7].sprite = emptyIcon;
            _redPotionAmount.gameObject.SetActive(false);
        }
    }

        IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.1f);
        iconUpdate = false;
        max = emptySlots.Length;
    }

    #region Private

    private int max;

    #endregion
}
