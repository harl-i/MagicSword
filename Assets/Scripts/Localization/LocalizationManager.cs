using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        [SerializeField] DialogueLocalization _dialogueLocalization;

        private Dictionary<string, DialogueLocalization.LocalizedEntry> _localizationDictionary;

        private void Awake()
        {
            _localizationDictionary = _dialogueLocalization.Entries.ToDictionary(e => e.Key);
        }

        public string GetText(string key)
        {
            if (!_localizationDictionary.ContainsKey(key))
            {
                return key;
            }

            var entry = _localizationDictionary[key];
            return YG2.lang switch
            {
                "ru" => entry.Russian,
                "tr" => entry.Turkish,
                _ => entry.English
            };
        }
    }
}