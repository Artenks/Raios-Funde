using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer _lineRenderer;
    public float y;

    private List<Vector3> _points;

    public void UpdateLine(Vector3 position)
    {
        if (_points == null)
        {
            _points = new List<Vector3>();
            SetPoint(new Vector3(position.x, position.y, y));
            return;
        }

        SetPoint(new Vector3(position.x, position.y, y));

    }
    private void SetPoint(Vector3 point)
    {
        _points.Add(point);

        _lineRenderer.positionCount = _points.Count;
        _lineRenderer.SetPosition(_points.Count - 1, point);
    }

}
