using System;
using UnityEngine;

namespace App.Models
{
    public abstract class BaseModel : MonoBehaviour, IModel
    {
        public bool IsSelectable { get; protected set; }
        public bool IsClickable { get; protected set; }

        public Action OnClick { get; set; }
        public Action OnSelected { protected get; set; }
        public Action OnDeselected { protected get; set; }


        public void Click()
        {
            if(IsClickable) OnClick.Invoke();
        }

        public void Selected()
        {
            if(IsSelectable) OnSelected.Invoke();
        }

        public void Deselected()
        {
            if(IsSelectable) OnDeselected.Invoke();
        }
        
    }
}