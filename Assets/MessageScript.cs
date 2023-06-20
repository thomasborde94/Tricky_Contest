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


    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = messageOn;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = messageOff;
    }
}
