using UnityEngine;
using UnityEngine.UI;

public class FrameManager : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private float deltaTime = 0.0f;

    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        text.text = $"{0}{1.0f / deltaTime}";
    }
}
