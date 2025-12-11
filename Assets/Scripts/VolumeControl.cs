using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        if (MusicManager.Instance != null)
        {
            // Inicializa el slider con el valor actual de MusicManager
            slider.value = MusicManager.Instance.volume;
        }

        // Suscribirse al cambio de valor
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetVolume(value);
        }
    }
}
