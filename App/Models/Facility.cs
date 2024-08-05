using System;
using App.Entities;
using App.Library;
using UnityEngine;

namespace App.Models
{
    public class Facility : BaseModel
    {
        private event Action<GameObject> _collisionOther;
        public event Action<GameObject> Collision
        {
            add => _collisionOther += value;
            remove => _collisionOther -= value;
        }
        
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

        public void OnCollisionOther()
        {
            _collisionOther?.Invoke(this.gameObject);
        }
    }
    
   
}