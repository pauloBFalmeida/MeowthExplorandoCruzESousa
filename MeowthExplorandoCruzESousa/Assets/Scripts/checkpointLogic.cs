using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointLogic : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnEnter;

    public void OnTriggerEnter()
    {
        if (OnEnter != null)
            OnEnter();
    }

}
