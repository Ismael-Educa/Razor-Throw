using UnityEngine;

public class FoamRegister : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.RegisterFoam(transform.childCount);
    }
}
