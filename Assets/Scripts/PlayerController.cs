using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _playerLayers;

    private bool _mousePressed;
    private bool _hasPlayer;
    private bool _endLevel;

    private void Awake()
    {
        ApplicationController.Instance.Managers.EventManager.OnLevelTimerIsUp += OnEndLevel;
    }

    private void OnEndLevel(object sender, bool e)
    {
        ApplicationController.Instance.Managers.EventManager.OnLevelTimerIsUp -= OnEndLevel;
        StopAllCoroutines();
        _endLevel = true;
    }
    void Update()
    {
        if(_endLevel)
        {
            return;
        }
        _mousePressed = Input.GetMouseButton(0) || Input.touchCount > 0;
        if(!_mousePressed)
        {
            if(_hasPlayer)
            {
                _hasPlayer = false;
            }
            return;
        }
        if(_hasPlayer)
        {
            return;
        }
        if (IsClickOnPlayer(out var player))
        {
            StartCoroutine(MovePlayer(player));
        }
    }

    private bool IsClickOnPlayer(out Rigidbody2D playerPart)
    {
        _hasPlayer = false;
        playerPart = null;
        Vector3 controlPosition = Vector3.zero;
#if UNITY_EDITOR
        controlPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID
        controlPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
#endif
        var hit = Physics2D.Raycast(controlPosition, Vector3.forward, 100f, _playerLayers);
        _hasPlayer = hit.collider != null;
        if(_hasPlayer)
        {
            playerPart = hit.collider.attachedRigidbody;
        }
        return _hasPlayer;
    }

    private IEnumerator MovePlayer(Rigidbody2D player)
    {
        while(_mousePressed && _hasPlayer)
        {
            Vector3 controlPosition = Vector3.zero;
#if UNITY_EDITOR
            controlPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID
        controlPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
#endif
            player.MovePosition(new Vector3(controlPosition.x, controlPosition.y));
            yield return null;
        }
    }
}
