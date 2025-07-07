using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescureTransition : Transition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NeedTransit = true;
    }
}
