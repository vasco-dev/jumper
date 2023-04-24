using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Ghost : Platform
{
    // interval of time the platform stays visible and invisible
    [SerializeField]
    private float _timeInterval = 2f;

    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private GameObject _ghost;
    [SerializeField]
    private MeshRenderer _renderer;

    private bool _isGhost = false;

    private bool _playerLanded = false;

    private void Awake()
    {
        if (_collider == null){
            _collider = GetComponent<Collider>();
        }
        if (_ghost == null){
            _ghost = GetComponentInChildren<Collider>().gameObject;
        }
    }

    private void Start()
    {
        StartCoroutine(GhostBehaviour());
    }
    protected override void OnPlayerLanded()
    {
        _playerLanded = true;
        _isGhost = true;
        _collider.enabled = _isGhost;
        _renderer.enabled = _isGhost;
        _ghost.SetActive(_isGhost);
    }

    private IEnumerator GhostBehaviour()
    {
        while (gameObject.activeSelf && _playerLanded == false)
        {
            _isGhost = !_isGhost;

            _collider.enabled = _isGhost;
            _ghost.SetActive(_isGhost);

            yield return new WaitForSeconds(_timeInterval);
        }
        yield return null;
    }

}