using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(CloseScreen);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveAllListeners();
    }
    
    private void OnEnable()
    {
        startButton.onClick.AddListener(CloseScreen);
    }

    private void CloseScreen()
    {
        GameManager.Instance.GameStates = GameManager.GameState.Playing;
        gameObject.SetActive(false);
    }
}
