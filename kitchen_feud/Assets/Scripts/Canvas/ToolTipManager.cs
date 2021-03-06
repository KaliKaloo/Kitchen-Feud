using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager Instance;
    public Canvas parentCanvas;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform, Input.mousePosition,
            parentCanvas.worldCamera,
            out pos);
    }

    void Update()
    {
        Vector2 movePos = Input.mousePosition;
        Vector3 tooltipOffset = new Vector3(7,12,5);
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        transform.position = new Vector2(movePos.x + (screenSize.x / tooltipOffset.x), movePos.y + (screenSize.y / tooltipOffset.y));
    }
}
