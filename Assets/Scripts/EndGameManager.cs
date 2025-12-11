using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        if (GameManager.Instance.foamRemaining <= 0)
        {
            text.text = "¡Nivel completado!";
        }
        else
        {
            text.text = "No eliminaste toda la espuma...\nInténtalo de nuevo";
        }
    }

    public void PlayAgain()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("Game");
    }
}
