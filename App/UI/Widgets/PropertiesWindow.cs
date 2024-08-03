using UnityEngine;

namespace App.UI.Widgets
{
    public class PropertiesWindow : BaseWidget
    {
        [SerializeField] private WindowHead head;
        
        [SerializeField] private Property positionX;
        [SerializeField] private Property positionY;
        [SerializeField] private Property positionZ;
        [SerializeField] private Property rotationX;
        [SerializeField] private Property rotationY;
        [SerializeField] private Property rotationZ;
        [SerializeField] private Property scaleX;
        [SerializeField] private Property scaleY;
        [SerializeField] private Property scaleZ;

        
        //이거는 setactive로해도되는건데 
        // public bool IsVisible { get; set; }
        public bool IsDraggable { get; set; }
        public bool IsResizable { get; set; }

        private void Awake()
        {
            // head.Body = GetComponent<RectTransform>();

            positionX.OnPress += (value) =>
            {

            };


        }



        public void SetPosition(Vector2 position)
        {
        }

        public void SetSize(Vector2 size)
        {
            
        }
    }
}