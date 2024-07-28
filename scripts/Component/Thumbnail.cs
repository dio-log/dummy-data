

using App.Data;
using UnityEngine;
using UnityEngine.UI;

    //썸네일은 카테고리마다 토글그룹을 해줘야 알아서 동작하겠음 
    public class Thumbnail : MonoBehaviour
    {
        private Node _thumbnailNode;


        private string _url; //요청경로에 node.id를 넣어줘서 요청?


        private Toggle _toggle;
    

        private Outline _outline;

        private Button _intantiateBtn;

        void Awake()
        {
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }
        private void OnMouseClick()
        {
            //inputsystem에서 이벤트 전략은, 트랜스포메이션일때는 다른 동작을 버블링하지 않는 용도로???? 아니면 UI에 작용하는 건 별도로?
            //아 클릭이라는 특정 이벤트만을 활성화하겠다는 의미지, 클릭했을때의 동작을 정의하지는 않아야한다. 이건 컴포넌트에 정의하자 
            //아웃라인 활성화 
            //더블클릭시?
            // 1. 일치하는 프리팹 유무 확인
            // 1-1. 없으면 요청 후 로컬에 쓰기
            // 2. 프리팹 로드
            // 3. 임의의 포지션 설정 
            //---
            // 번들 웹요청 -> 프리팹 로드 -> 인스턴스화 
            //인스턴스화된 거에 대해서 이름등 설명이 필요할듯 
        }

        private void OnValueChanged(bool isOn)
        {
            if(isOn)
            {
                _outline.enabled = true;
                _intantiateBtn.gameObject.SetActive(true);
                //인스턴스화한다는 버튼이 떠야함 
            }   
            else
            {
                _outline.enabled = false;
                _intantiateBtn.gameObject.SetActive(false);
            }
        }




        private void OnInstantiateBtnClick()
        {
            //인스턴스화해서 임의의위치에 배치 . 일단은 프리팹이 갖는 임의의 포지션이 있을거고, 별도로 배치하려면 포지션 세팅해주면 됨 
            //지금 문제는 url요청시 제대로 주지않아서 로컬에서 로드 
        }


        

    }   
