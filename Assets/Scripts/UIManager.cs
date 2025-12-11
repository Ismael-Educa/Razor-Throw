using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI foamText;       // % de espuma restante
    public TextMeshProUGUI attemptsText;   // intentos restantes
    public GameObject endLevelPanel;       // panel de fin de nivel

    [Header("Opcional")]
    public string nextLevelScene;           // nombre de la escena siguiente
    public string menuScene;                // nombre de la escena del menú

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (endLevelPanel != null)
            endLevelPanel.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance == null) return;

        // Actualiza porcentaje de espuma
        if (foamText != null && GameManager.Instance.foamTotal > 0)
        {
            float percent = (float)GameManager.Instance.foamRemaining / GameManager.Instance.foamTotal * 100f;
            foamText.text = $"Espuma: {percent:0}%";
        }

        // Actualiza intentos restantes
        if (attemptsText != null)
        {
            attemptsText.text = $"Intentos: {GameManager.Instance.attempts}";
        }
    }

    /// <summary>
    /// Muestra el panel de fin de nivel
    /// </summary>
    public void ShowEndLevelPanel()
    {
        if (endLevelPanel != null)
            endLevelPanel.SetActive(true);
    }

    // Botón Siguiente Nivel
    public void NextLevel()
    {
        GameManager.Instance.ResetGame();
        if (!string.IsNullOrEmpty(nextLevelScene))
            SceneManager.LoadScene(nextLevelScene);
    }

    // Botón Menú
    public void BackToMenu()
    {
        GameManager.Instance.ResetGame();
        if (!string.IsNullOrEmpty(menuScene))
            SceneManager.LoadScene(menuScene);
    }

    // Botón Reiniciar Nivel
    public void RestartLevel()
    {
        GameManager.Instance.ResetGame();   // Opcional, si quieres resetear variables
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
