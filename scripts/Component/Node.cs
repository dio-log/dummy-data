

using UnityEngine.UI;

namespace App.Data
{
    public interface IData
    {

    }

    public class Node : IData
    {
        //실제 맵핑될 오브젝트가 있는지 필드로 하나 가지고 있으면 되나?
    }
    public class Transformation : IData
    {}

    public class HierarchyButton
    {
        public Node Node{
            get; set;
        }
        public Toggle Toggle
        {
            get; set;
        }

    }

    public class Facility : IData
    {
        Node node;
        Transformation transformation;
    }

}
    