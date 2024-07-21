using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using Unity.VisualScripting;


public class AlphaTest : MonoBehaviour
{

    private Renderer _renderer;
    
    float last = 0f;


    void Start()
    {
        StartCoroutine(printFixedUpdate());
    }
    void UpdateAlpha(){
        Debug.Log(_renderer == null);
        Color color;
        color = _renderer.material.color;
        Debug.Log(color);
        color.a = 0;
        _renderer.material.color = color;
        int delay = 100;
        while(true){
            color = _renderer.material.color;
            last += delay;
            
            
            Thread.Sleep(delay);
            color.a = last/2000f;
            _renderer.material.color = color;


            Debug.Log(color.a);
            if(last >= 2000) break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //   Color color;
        // color = _renderer.material.color;
        // Debug.Log(color);
        // color.a = 0;
        // _renderer.material.color = color;
        // Debug.Log(Time.deltaTime);

        // for(int i=0; i<5000000; i++){
        //     if(i==5000000){
        //         Debug.Log(i);
        //     }
           
        // }
        Thread.Sleep(2000);
    }

    // WaitForFixedUpdate wait = 
    IEnumerator printFixedUpdate(){
        int i = 0;
        int prevCount = 0;
        while(true){
            if(prevCount !=Time.frameCount){
                Debug.Log($"count: {i}");
                i = 0; 
            } 
            prevCount = Time.frameCount;
            Debug.Log($"frame: {prevCount} / fixedTime: {Time.fixedTime} / runtime: {Time.realtimeSinceStartup}");
            i++;

            // yield return null;
            yield return new WaitForFixedUpdate();
        }

    }
}
