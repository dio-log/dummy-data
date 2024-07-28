

using System.Collections.Generic;
using System.Reflection;
using App.Data;
using UnityEngine;
using UnityEngine.UI;

namespace App.GUI.Window
{
    //이창에서 사용될 정보들은 일반적으로 정해져있다. 데이터만 바꾼다. 다시 그리지 말고 
    public class PropertiesWindow : MonoBehaviour
    {
        private Facility _facility;

        private InputField _name;
       
        private InputField _posX;
        private InputField _posY;
        private InputField _posZ;
        private InputField _rotX;
        private InputField _rotY;
        private InputField _rotZ;
        private InputField _scaleX;
        private InputField _scaleY;
        private InputField _scaleZ;

        private Dictionary<InputField, string> _fieldMappings;

        
        //프리팹은 각 프롭까지만 


        void Awake()
        {
            _fieldMappings = new Dictionary<InputField, string>
            {
                {_name, "name"},
                { _posX, "posX" },
                { _posY, "posY" },
                { _posZ, "posZ" },
                { _rotX, "rotX" },
                { _rotY, "rotY" },
                { _rotZ, "rotZ" },
                { _scaleX, "scaleX" },
                { _scaleY, "scaleY" },
                { _scaleZ, "scaleZ" }
            };

            foreach (var field in _fieldMappings)
            {
                field.Key.onValueChanged.AddListener(value => OnInputFieldValueChanged(field.Key, value));
            }
        }

    //string도되는지 체크해야함
        private void OnInputFieldValueChanged(InputField inputField, string value)
        {
            if (_fieldMappings.TryGetValue(inputField, out string propertyName))
            {
                if (float.TryParse(value, out float parsedValue))
                {
                    // Reflection을 사용하여 Facility 객체의 속성을 업데이트합니다.
                    PropertyInfo propertyInfo = typeof(Facility).GetProperty(propertyName);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(_facility, parsedValue);
                    }
                }
            }
        }
    }

    public class FacilitiesHierarchyWindow
    {

    }

    public class ThumnailsHierarchyWindow
    {

    }
    public class ThumnailsWindow
    {

    }

}