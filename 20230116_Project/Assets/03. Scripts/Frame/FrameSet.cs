using UnityEngine;

public class FrameSet : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
