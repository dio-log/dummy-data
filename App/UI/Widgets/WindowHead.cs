using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.UI.Widgets
{
    public class WindowHead : BaseWidget, IDragHandler
    {
        [SerializeField] private TMP_Text title;
        
        [SerializeField] private Toggle toggle;
        
        [SerializeField] private RectTransform body;

        private void Start()
        {
            // toggle.onValueChanged.AddListener((isOn) =>
            // {
            //     body?.gameObject.SetActive(isOn);
            // });
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log((eventData.position));
            // body.position = eventData.position;
        }

    }
}