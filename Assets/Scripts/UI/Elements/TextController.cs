using System;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpText;

        private void Reset()
        {
            _tmpText = GetComponent<TMP_Text>();
        }

        public void Increment()
        {
            if (_tmpText.text.Any(char.IsLetter)) return;
            
            var number = Convert.ToInt16(_tmpText.text);
            ++number;
            _tmpText.text = number.ToString();
        }

        public void Decrement()
        {
            if (_tmpText.text.Any(char.IsLetter)) return;
            
            var number = Convert.ToInt16(_tmpText.text);
            --number;
            _tmpText.text = number.ToString();
        }        

        public void SetText(string value)
        {
            _tmpText.text = value;
        }

        public void SetNumber(float value)
        {
            _tmpText.text = value.ToString("G4");
        }
    }
}
