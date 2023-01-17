using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMove : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private PlayerMove player = null;

    [SerializeField]
    private RectTransform leverRect = null;

    [SerializeField]
    private float clampedSize = 160;

    private RectTransform rect = null;

    private Vector2 leverVec = Vector2.zero;

    private Vector2 input = Vector2.zero;

    private bool isInput = false;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isInput)
        {
            player.Move(input.x, input.y);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isInput = false;
        leverVec = Vector2.zero;
        leverRect.anchoredPosition = leverVec;
        player.StopMove();
    }

    private void ControlLever(PointerEventData eventData)
    {
        leverVec = eventData.position - rect.anchoredPosition;
        if (leverVec.magnitude > clampedSize)
        {
            leverVec = leverVec.normalized * clampedSize;
        }

        leverRect.anchoredPosition = leverVec;

        input = leverVec / clampedSize;
    }
}
