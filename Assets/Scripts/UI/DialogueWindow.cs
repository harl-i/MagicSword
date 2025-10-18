using System;
using UnityEngine;

namespace UI
{
    public class DialogueWindow : MonoBehaviour
    {
        public Action WindowShown;

        public void OnWindowShowAnimationFinished()
        {
            WindowShown?.Invoke();
        }
    }
}