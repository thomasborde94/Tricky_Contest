using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Public functions

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Time.timeScale == 1)
        {
            PlayerMovement.canMove = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Time.timeScale == 1)
        {
            PlayerMovement.canMove = true;
        }
    }

    #endregion
}
