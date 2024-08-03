using System;
using App.Entities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace App.Models
{
    public interface IModel
    {
        bool IsSelectable { get; }
        bool IsClickable { get; }
        
        public Action OnClick { set; }
        public Action OnSelected { set; }
        public Action OnDeselected { set; }
    }
    
}