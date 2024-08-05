using App.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Widgets
{
    //프리팹에 버튼이 있어야함  
    public class Thumbnail : BaseWidget
    {
        [SerializeField] private Button button;
        
        public ThumbnailEntity Entity { get; set; }
        
        //버튼이 클릭되면 프리팹로드 -> 인스턴스화 이벤트전략 변경
        //월드 포지션 

        private void Awake()
        {
            // button.onClick.AddListener(() =>
            // {
            //     InstantiateModel();
            // });
        }

        private void Start()
        {
            // InstantiateModel();
            
        }

        private GameObject InstantiateModel()
        {
            GameObject container = GameObject.Find("GizmoTarget");
            GameObject target = GameObject.Find("Target");
            GameObject obj = Resources.Load<GameObject>("Prefabs/Cube");
            GameObject instance = Instantiate(obj);
            // instance.transform.position = target.transform.position;
            // instance.transform.rotation = target.transform.rotation;
            // instance.transform.localScale = target.transform.localScale;
            
            Bounds bounds = target.GetComponent<Collider>().bounds;
            Vector3 center = bounds.center;
            Vector3 size = bounds.size;
            Vector3 floorCenter = new Vector3(center.x, center.y - (size.y / 2), center.z);
            Transform cubeTransform = instance.transform;
            cubeTransform.position = center;
            cubeTransform.rotation = target.transform.rotation;
            cubeTransform.localScale = size;
            
            target.transform.SetParent(cubeTransform);
            
            container.transform.position = floorCenter;
                            
            cubeTransform.SetParent(container.transform);
            
            
            // GameObject instance = Instantiate(DataUtil.LoadPrefab(), container.transform);
            // Facility facility = instance.AddComponent<Facility>();
            // facility.ModelEntity = new()
            // {
            //     Id = GUID.Generate().ToString(),
            //     Name = Entity.BundleName,
            //     BundleName = Entity.BundleName,
            //     AssetName = Entity.AssetName,
            // };
            
            return container;
        }
    }
}