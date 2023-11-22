using UnityEngine;

public class PokiSDKInitializer : MonoBehaviour
{
    private void Awake()
    {
        PokiUnitySDK.Instance.init();
    }

    private void Start()
    {
        PokiUnitySDK.Instance.gameplayStart();
    }
}