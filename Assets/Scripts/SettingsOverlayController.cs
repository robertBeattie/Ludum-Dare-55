using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsOverlayController : MonoBehaviour
{

    public Button m_SettingsButton;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting initializing! " + m_SettingsButton.ToString());
        m_SettingsButton.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        Debug.Log("BUTTON CLICKED!");
    }
}
