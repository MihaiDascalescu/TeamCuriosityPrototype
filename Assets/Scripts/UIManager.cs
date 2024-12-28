using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private EndScreen endScreen;
    void Start()
    {
        endScreen.gameObject.SetActive(false);
        
        OpenStartScreen();
        
        GameManager.Instance.IsGameFinished += OpenEndScreen;
        GameManager.Instance.OnGameRestarted += OpenStartScreen;
    }

    private void OnDestroy()
    {
        GameManager.Instance.IsGameFinished -= OpenEndScreen;
        GameManager.Instance.OnGameRestarted -= OpenStartScreen;
    }

    private void OpenStartScreen()
    {
        startScreen.gameObject.SetActive(true);
    }
    private void OpenStartScreen(object sender, EventArgs e)
    {
        endScreen.gameObject.SetActive(false);
        OpenStartScreen();
    }

    private void OpenEndScreen(object sender, EventArgs e)
    {
        endScreen.gameObject.SetActive(true);
    }
}
