using UnityEngine;

public class EndInfo : MonoBehaviour
{
    public bool DestroyObject;
    private Animator _animator;
    private DestroyWhenOver _destroyWhenOver;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _destroyWhenOver = GetComponent<DestroyWhenOver>();
    }

    private void Update()
    {
        if (DestroyObject)
        {
            _animator.SetTrigger("Desappear");
            _destroyWhenOver.enabled = true;
        }
    }
}
