
using UnityEngine;

namespace App.Entities
{
    public class ModelEntity : IEntity 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BundleName { get; set; }
        public string AssetName { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
    }
}