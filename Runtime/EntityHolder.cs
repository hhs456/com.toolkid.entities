using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHolder : MonoBehaviour
{
   [SerializeField] EnabledComponent enabledComponent;
}
[Flags]
public enum EnabledComponent {
    Transform, Collider, Rigidbody
}