using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject SettingPanel;

    public void OnSettingPanelOpen()
    {
        SettingPanel.SetActive(true);
    }
    public void OnSettingPanelClose()
    {
        SettingPanel.SetActive(false);
    }
    public void ApplicationQuit()
    {
        Application.Quit();
    }
    public void OnPlayBtnClicked()
    {
        SceneManager.LoadScene(1);
    }
}
