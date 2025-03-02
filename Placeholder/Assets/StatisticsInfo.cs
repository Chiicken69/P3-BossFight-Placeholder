using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatisticsInfo : MonoBehaviour
{
    private int _resolutionWidth = 1920;
    private int _resolutionHeight = 1080;
    private bool _isDebugOn;

    private Canvas _canvas;
    private GameObject _canvasObject;

    private CanvasScaler _canvasScaler;
    private TMP_Text _fpsText; // Assign a UI Text element in the Inspector
    private float _deltaTime = 0.0f;

    private void Awake()
    {
        CreateDebugCanvas();
        CreateDebugText();
    }

    void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        float fps = 1.0f / _deltaTime;
        _fpsText.text = "FPS: " + Mathf.Ceil(fps);
        
    }

    public void CreateDebugCanvas()
    {
        _canvasObject = new GameObject("DebugCanvas");

        _canvas = _canvasObject.AddComponent<Canvas>();

        // add scaler
        _canvasScaler = _canvasObject.AddComponent<CanvasScaler>();
        _canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        // change settings

        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _canvasScaler.referenceResolution = new Vector2(_resolutionWidth, _resolutionHeight);

    }

    public void CreateDebugText()
    {
        // create the object
        GameObject textObject = new GameObject("FPSText");
        textObject.transform.SetParent(_canvasObject.transform);
        
        // add component
        _fpsText = textObject.AddComponent<TextMeshProUGUI>();
        
        // set text options and styling stuff
        
        _fpsText.fontSize = 36;
        _fpsText.color = Color.white;
        _fpsText.alignment = TextAlignmentOptions.Center;
        
        // adjust pos
        
        RectTransform rectTransform = _fpsText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 200);
        rectTransform.sizeDelta = new Vector2(300, 100);


    }
}
