using System;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public event Action WrittedEventHandler;

    public PaperArea PaperArea;

    public Camera NowCamera;
    public GameObject _linePrefab;

    public Line ActiveLines;

    void Update()
    {
        if (PaperArea.PaperAreaOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newLine = Instantiate(_linePrefab, this.gameObject.transform);
                ActiveLines = newLine.GetComponent<Line>();

                WrittedEventHandler?.Invoke();
            }

            if (ActiveLines != null)
            {
                Vector2 mousePosition = NowCamera.ScreenToWorldPoint(Input.mousePosition);
                ActiveLines.UpdateLine(mousePosition);
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            ActiveLines = null;
        }

    }

}
