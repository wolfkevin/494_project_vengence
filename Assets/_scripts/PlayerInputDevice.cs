using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInputDevice : MonoBehaviour
{
    public int playerNumber;
    public InputDevice inputDevice;

    // Use this for initialization
    void Awake()
    {
        if (playerNumber < InputManager.Devices.Count) {
            inputDevice = InputManager.Devices[playerNumber];
        }

    }
  }
