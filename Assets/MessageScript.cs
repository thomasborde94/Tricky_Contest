using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static string _messageScript;

    [SerializeField] public Text buttonText;
    [SerializeField] public Color32 messageOff;
    [SerializeField] public Color32 messageOn;

    // Changes the color of the text if player mouseovers it
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = messageOn;
    }
    // Gives default value to the text when players stops overring it
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = messageOff;
    }
}
