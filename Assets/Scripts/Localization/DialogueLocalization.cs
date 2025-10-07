using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    [CreateAssetMenu(menuName = "Localization/DialogueLocalization")]
    public class DialogueLocalization : ScriptableObject
    {
        [System.Serializable]
        public class LocalizedEntry
        {
            public string Key;
            public string Russian;
            public string English;
            public string Turkish;
        }

        public List<LocalizedEntry> Entries = new List<LocalizedEntry>();
    }
}