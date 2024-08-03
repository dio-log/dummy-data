// using System.Collections.Generic;
// using App.Entities;
// using App.UI.Elements;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
//
// namespace App.UI.Elements
// {
//     public class CompositeElement : MonoBehaviour
//     { 
//         public BaseElement<ElementEntity> Entity { get; set; } 
//         
//         private readonly Dictionary<string, UIBehaviour> elements  = new ();
//         // public UIElementEntity { get; set; }
//         
//         
//         public void AddElement(string key, UIBehaviour element)
//         {
//             if (element != null)
//             {
//                 elements[key] = element;
//             }
//         }
//
//         public T GetElement<T>(string key) where T : UIBehaviour
//         {
//             if (elements.TryGetValue(key, out var element) && element is T typedElement)
//             {
//                 return typedElement;
//             }
//             return null;
//         }
//
//         public void RemoveElement(string key)
//         {
//             elements.Remove(key);
//         }
//
//         public void SetActiveAll(bool active)
//         {
//             foreach (var element in elements.Values)
//             {
//                 element.gameObject.SetActive(active);
//             }
//         }
//
//         public void SetInteractableAll(bool interactable)
//         {
//             foreach (var element in elements.Values)
//             {
//                 if (element is Selectable selectable)
//                 {
//                     selectable.interactable = interactable;
//                 }
//             }
//         }
//     }
// }