using App.Data;
using UnityEngine;


//오브젝트 데이터 받을때 퍼실러티를만들어야겠지 
//어떤 하이라키버튼과 연결되어있는지 알아야함, 노드는 데이터고, 하이라키 버튼을 가지고있고 거기서 노드의 정보를 참조해야겠음 
//즉, 하이라키 - 3d모델 - 정보창은 서로의 존재가 연결되어야 있어야 함 
    public class FacilityComponent: MonoBehaviour, IFacilityComponent
    {
        public HierarchyButton HierarchyButton
        {
            get;set;
        }

        private Node _node;

        private Transformation _transformation;
        //업데이트 칠경우 데이터 구조체만 보내야하니까 별도로 관리해야겠음 

        
        void Awake()
        {
            _node = this.HierarchyButton.Node;
        }

        public void OnMouseClick()
        {
            //하이어라키 표시 
            //우측에 트랜스포메이션이나 이름, 등 데이터를 표시하는걸 갈아껴야함 
            
            this.HierarchyButton.Toggle.isOn = true;

        }



    }

