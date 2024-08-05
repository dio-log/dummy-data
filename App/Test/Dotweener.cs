using System;
using DG.Tweening;
using UnityEngine;

namespace App.Test
{
    public class Dotweener : MonoBehaviour
    {
        private void Update()
        {
            
            // Debug.Log("worldPos : "+transform.position);
            // Debug.Log("localPos : "+transform.localPosition);
            // Debug.Log("worldRot : "+transform.rotation);
            // Debug.Log("localRot : "+transform.localRotation);
            // Debug.Log("worldScale : "+transform.lossyScale);
            // Debug.Log("localScale : "+transform.localScale);
        }

        public void DoTween()
        {
            Debug.Log("--------------");
            Debug.Log("worldPos : "+transform.position);
            // Debug.Log("localPos : "+transform.localPosition);
            Debug.Log("worldRot : "+transform.rotation.eulerAngles);
            // Debug.Log("localRot : "+transform.localRotation);
            Debug.Log("worldScale : "+transform.lossyScale);
            
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation += new Vector3(0, 0, 45);
            // transform.DORotate(rotation, 0);
            // transform.parent.DORotate(rotation, 0).OnComplete(() =>
            // {
            //     Debug.Log("worldPos : "+transform.position);
            //     // Debug.Log("localPos : "+transform.localPosition);
            //     Debug.Log("worldRot : "+transform.rotation.eulerAngles);
            //     // Debug.Log("localRot : "+transform.localRotation);
            //     Debug.Log("worldScale : "+transform.lossyScale);
            //     // Debug.Log("localScale : "+transform.localScale);
            // });
            float diff = transform.parent.lossyScale.x + 0.1f;
            transform.parent.DOScaleX(diff, 0).OnComplete(() =>
            {
                Debug.Log("worldPos : " + transform.position);
                // Debug.Log("localPos : "+transform.localPosition);
                Debug.Log("worldRot : " + transform.rotation.eulerAngles);
                // Debug.Log("localRot : "+transform.localRotation);
                Debug.Log("worldScale : " + transform.lossyScale);
                // Debug.Log("localScale : "+transform.localScale);
            });


        }
        
    }
}