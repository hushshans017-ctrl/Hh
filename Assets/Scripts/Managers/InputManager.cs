using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public enum Platform
    {
        Android,
        iOS,
        Editor
    }

    private Platform currentPlatform;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        DetectPlatform();
    }

    private void DetectPlatform()
    {
        #if UNITY_ANDROID
            currentPlatform = Platform.Android;
        #elif UNITY_IOS
            currentPlatform = Platform.iOS;
        #else
            currentPlatform = Platform.Editor;
        #endif

        Debug.Log("Running on platform: " + currentPlatform);
    }

    public Platform GetCurrentPlatform()
    {
        return currentPlatform;
    }

    public bool IsMobileDevice()
    {
        return currentPlatform == Platform.Android || currentPlatform == Platform.iOS;
    }

    public Vector2 GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        return Vector2.zero;
    }
}
