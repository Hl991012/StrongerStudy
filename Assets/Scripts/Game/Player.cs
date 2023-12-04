using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Circle
{
    [SerializeField] private Transform line;
    
    private void Awake()
    {
        Init(new CircleModel(0.66f, Vector2.zero, 0, CircleModel.MoveType.None));
        transform.localScale = Vector3.one * CircleModel.diameter;

        InputManager.Instance.OnInputDirStateChanged += OnInputDirStateChanged;
        InputManager.Instance.OnClickSpitBtn += OnClickSpitBtn;
    }

    protected override void Move()
    {
        base.Move();

#if UNITY_EDITOR
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(h, v), ForceMode2D.Impulse);
        }
#endif

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnClickSpitBtn();
        }
    }

    private void OnInputDirStateChanged(bool active)
    {
        line.gameObject.SetActive(active);
        var angle = InputManager.Instance.dir.y >= 0
            ? Vector2.Angle(Vector2.right, InputManager.Instance.dir)
            : -Vector2.Angle(Vector2.right, InputManager.Instance.dir);
        line.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnClickSpitBtn()
    {
        CircleManager.Instance.CreatCircle(0.2f,
            transform.position + CircleModel.diameter * Vector3.Normalize(InputManager.Instance.dir));
        Debug.LogError(2222);
    }
}
