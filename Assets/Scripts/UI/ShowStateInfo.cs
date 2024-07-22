using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowStateInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public StateData data;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StateUIDatabase.stateUIDB.stateInfoUI.transform.SetParent(transform);
        StateUIDatabase.stateUIDB.stateInfoUI.transform.localPosition = new Vector2(0, 0);
        StateUIDatabase.stateUIDB.stateInfoUI.SetActive(true);
        StateUIDatabase.stateUIDB.stateInfoUI.GetComponentInChildren<Text>().text = data.stateExplanation;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (StateUIDatabase.stateUIDB.stateInfoUI.transform.position != transform.position)
            return;
        StateUIDatabase.stateUIDB.stateInfoUI.transform.SetParent(StateUIDatabase.stateUIDB.transform);

    }
}
