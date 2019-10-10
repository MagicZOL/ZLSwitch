using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GYSwitch : MonoBehaviour
{
    [SerializeField] RectTransform handleRectTransform;

    public MenuCtrl menuCtrl;

    static bool isOn; //Turn : on, False: off

    private void Start()
    {
        isOn = false;
        handleRectTransform.anchoredPosition = new Vector2(-30, 0);
    }
    //스위치 0n/off 동작
    public void OnClickSwitch()
    {
        if (isOn)
        {
            handleRectTransform.anchoredPosition = new Vector2(-30, 0);
            isOn = false;
        }
        else
        {
            handleRectTransform.anchoredPosition = new Vector2(30, 0);
            isOn = true;
        }
        menuCtrl.SwitchIsOn(isOn);
    }
}
