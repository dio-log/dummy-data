using System;
using App.Entities;
using App.Library;
using UnityEngine;

namespace App.Models
{
    public class Facility : BaseModel
    {
        public ModelEntity Entity { get; set; }

        [SerializeField] private QuickOutline outline;

        private Transform cachedTransform;

        public void Awake()
        {
            cachedTransform = transform;

            OnSelected += () => { outline.enabled = true; };

            OnDeselected += () => { outline.enabled = false; };
        }

        private void Update()
        {
            UpdateEntity();
        }

        private void UpdateEntity()
        {
            if (!HasChanged()) return;
            
            Entity.Position = cachedTransform.position;
            Entity.Rotation = cachedTransform.rotation.eulerAngles;
            Entity.Scale = cachedTransform.localScale;
        }

        private bool HasChanged()
        {
            if (Entity == null) return false;

            return Entity.Position != cachedTransform.position ||
                   Entity.Rotation != cachedTransform.rotation.eulerAngles ||
                   Entity.Scale != cachedTransform.localScale;

        }
        
    }
}