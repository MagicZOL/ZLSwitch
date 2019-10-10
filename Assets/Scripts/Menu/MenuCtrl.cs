using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCtrl : MonoBehaviour
{
    public GameObject settingsPanel;

    AudioSource audioSource;
    private void Start()
    {
        settingsPanel.SetActive(false);
        //audioSource.Play();
        //audioSource.Stop();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
        //설정창 열기
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        //설정창 닫기
        settingsPanel.SetActive(false);
    }

    //Switch를 위한 함수
    public void SwitchIsOn(bool isOn)
    {
        if (isOn == true)
            audioSource.Play();
        else
            audioSource.Pause();
    }
}
