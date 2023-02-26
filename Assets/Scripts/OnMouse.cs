using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Texto da interface"), Space(2)]
    [Multiline]
    public string TextOnMouseHover;
    [Header("Texto da interface desativada"), Space(2)]
    [Multiline]
    public string TextDisabledOnHover;

    public GameObject PrefabObject;

    public enum ObjectStatus
    {
        active,
        disabled,
        both,
    }
    [Header("Reagir em que estado"), Space(2)]
    public ObjectStatus ObjectState;

    private TMP_Text _infoText;
    private GameObject _infoObject;
    private TimeForUpdate _timeForUpdate;

    private bool _callCount = false;
    private bool _showInfo = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var rightState = ObjectInRightState();

        if (!rightState)
            return;

        _infoObject = Instantiate(PrefabObject, this.gameObject.transform);

        _timeForUpdate = _infoObject.GetComponent<TimeForUpdate>();
        _callCount = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _callCount = false;
        _showInfo = false;

        if (_infoObject == null)
            return;

        DisableAndDestroyObjects();
    }


    private void Update()
    {

        if (_callCount)
        {
            if (!_showInfo)
                _showInfo = _timeForUpdate.StartTheCount(0.5f);
            else
            {
                UpdateCoponentsStats();

                _callCount = false;
                _showInfo = false;
            }
        }

    }

    private bool ObjectInRightState()
    {
        switch (ObjectState)
        {
            case ObjectStatus.active:
                if (this.gameObject.GetComponent<Button>().interactable)
                    return true;
                break;
            case ObjectStatus.disabled:
                if (!this.gameObject.GetComponent<Button>().interactable)
                    return true;
                break;
            case ObjectStatus.both:
                return true;
        }
        return false;
    }
    private void DisableAndDestroyObjects()
    {
        _infoObject.GetComponent<Animator>().SetTrigger("Desappear");
    }

    private void UpdateCoponentsStats()
    {
        if (_infoObject == null)
            return;

        foreach (var item in _infoObject.GetComponentsInChildren<RectTransform>(true))
        {
            item.gameObject.SetActive(true);
        }

        var prefabPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        _infoObject.transform.position = prefabPosition;

        var rects = _infoObject.GetComponentsInChildren<RectTransform>();
        var thisRect = this.GetComponent<RectTransform>();

        foreach (var item in rects)
        {
            item.sizeDelta = thisRect.sizeDelta;
        }

        _infoText = _infoObject.GetComponentInChildren<TMP_Text>();

        switch (ObjectState)
        {
            case ObjectStatus.both:
            case ObjectStatus.disabled:
                if (this.gameObject.GetComponent<Button>().interactable)
                    _infoText.text = TextOnMouseHover;
                else
                    _infoText.text = TextDisabledOnHover;
                break;

            default:
                _infoText.text = TextOnMouseHover;
                break;
        }

        _infoObject.GetComponent<Animator>().SetTrigger("Appear");
    }

    private void OnDisable()
    {
        _callCount = false;
        _showInfo = false;

        if (_infoObject == null)
            return;

        DisableAndDestroyObjects();
    }

}
