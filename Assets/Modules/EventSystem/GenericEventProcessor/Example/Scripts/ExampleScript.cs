using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Katakuri.Modules.Event.Test
{
    public class ExampleScript : MonoBehaviour
    {
        [SerializeField] private Button _clickButton;
        [SerializeField] private TMP_InputField _guessField;
        [SerializeField] private Button _guessButton;

        private void Start() 
        {
            _clickButton.onClick.AddListener(OnClickButton);
            _guessButton.onClick.AddListener(OnGuessNumber);
        }

        private void OnClickButton()
        {
            EventManager.TriggerEvent<ExampleEvent.ClickButtonEvent>();
        }

        private void OnGuessNumber()
        {
            if(int.TryParse(_guessField.text, out int num))
            {
                EventManager.TriggerEvent(new ExampleEvent.SubmitValueEvent(num));
            }
        }
    }
}

