using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Nivel")]
    public int foamTotal;
    public int foamRemaining;
    public int attempts = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Registrar cuánta espuma hay al inicio del nivel
    /// </summary>
    public void RegisterFoam(int amount)
    {
        foamTotal = amount;
        foamRemaining = amount;
    }

    /// <summary>
    /// Llamado por Foam.cs cuando cada sprite de espuma es destruido
    /// </summary>
    public void FoamDestroyed()
    {
        if (foamRemaining <= 0) return;

        foamRemaining--;

        // Si no queda espuma, mostrar panel final
        if (foamRemaining <= 0)
        {
            if (UIManager.Instance != null)
                UIManager.Instance.ShowEndLevelPanel();
        }
    }

    /// <summary>
    /// Llamado por LaunchManager cuando termina un intento
    /// </summary>
    public void LaunchEnded()
    {
        attempts--;

        // Si no quedan intentos, mostrar panel final
        if (attempts <= 0)
        {
            if (UIManager.Instance != null)
                UIManager.Instance.ShowEndLevelPanel();
        }
    }

    /// <summary>
    /// Resetea los valores del juego
    /// </summary>
    public void ResetGame()
    {
        foamTotal = 0;
        foamRemaining = 0;
        attempts = 3;
    }
}
