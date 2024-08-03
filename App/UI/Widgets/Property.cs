using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace App.UI.Widgets
{
    public class Property : BaseWidget
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button applyButton;
        public UnityAction<string> OnPress { get; set; }
        
        private void Awake()
        {
            inputField.onValueChanged.AddListener((value) =>
            {
                applyButton.onClick.Invoke();
            });
            
            applyButton.onClick.AddListener(() =>
            {
                OnPress.Invoke(inputField.text);
            });
        }

        public void SetValue(string value)
        {
            inputField.text = value;
        }

        public void SetValue(float value)
        {
            //변환
        }

    }
}