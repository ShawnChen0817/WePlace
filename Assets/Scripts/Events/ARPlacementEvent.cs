using System;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
    public class ARObjectPlacementEvent : UnityEvent<InputManager, GameObject>
    {
    }