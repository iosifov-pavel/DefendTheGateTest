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
        _mousePressed = Input.GetMouseButton(0);
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
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(mousePosition, Vector3.forward, 100f, _playerLayers);
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
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            player.MovePosition(new Vector3(mousePosition.x, mousePosition.y));
            yield return null;
        }
    }
}
