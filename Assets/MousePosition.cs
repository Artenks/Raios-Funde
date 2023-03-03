using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public Camera MainCamera;
    private Transform _mouseTransform;

    private void Awake()
    {
        _mouseTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        var inputMouse = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        _mouseTransform.position = new Vector3(inputMouse.x, inputMouse.y, _mouseTransform.transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(this.transform.position, GetComponent<SphereCollider>().radius);
    }
}
