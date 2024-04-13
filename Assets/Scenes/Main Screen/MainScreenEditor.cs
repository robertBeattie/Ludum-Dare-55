using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainScreen : MonoBehaviour
{

    [SerializeField]
    private VisualTreeAsset m_UXMLTree;

    public void OnEnable() {
        var visualTree = m_UXMLTree.CloneTree();
        var uiDocument = GetComponent<UIDocument>();

        Button playButton = uiDocument.rootVisualElement.Query<Button>("PlayButton");
        Button settingsButton = uiDocument.rootVisualElement.Query<Button>("SettingsButton");

        playButton.RegisterCallback<ClickEvent>(_ => LoadScene("SampleScene"));
        settingsButton.RegisterCallback<ClickEvent>(_ => LoadScene("settingsScene"));
    }

    private void LoadScene(string scene){
        Debug.Log("Loading Scene " + scene);
        SceneManager.LoadScene(scene);
    }

}
