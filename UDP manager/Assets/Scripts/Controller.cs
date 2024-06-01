using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] Slider send_value_slider;
    Vector2 axis = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current == null)
        {
            return;
        }

        axis = Gamepad.current.leftStick.ReadValue();
        send_value_slider.value = axis.x;

    }
}
