// using System;
// using System.Collections.Generic;
// using App.UI.Element;
// using App.UI.Section;
// using UnityEngine;
//
// namespace App.UI
// {
//     public class UIManager :MonoBehaviour
//     {
//         public static UIManager Instance { get; private set; }
//         
//         private Dictionary<string, UISection> sections = new();
//         
//
//         private void Awake()
//         {
//             if (Instance)
//             {   
//                 DestroyImmediate(gameObject);
//                 return;
//             }
//
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//
//         public void RegisterUIElement(IUIElement element)
//         {
//             
//         }
//     }
// }