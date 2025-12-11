using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Botón Jugar
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    // Botón Salir
    public void QuitGame()
    {
        Application.Quit();
        // Nota: en el editor no hace nada, solo en build
    }
}