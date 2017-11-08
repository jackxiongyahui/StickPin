using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float speed = 5f;   //针前进的速度
    private bool isFly = false;     //是否进行飞行

    private bool isReach = false;   //是否到达startPoint

    private Vector3 targetCirclePos;  //目标位置

    private Transform startPoint;
    private Transform circle;

	// Use this for initialization
	void Start () {
        startPoint = GameObject.Find("StartPoint").transform;
        circle = GameObject.FindGameObjectWithTag("Circle").transform;
        targetCirclePos = circle.position; //目标点的位置
        targetCirclePos.y = 0.43f;    //针围绕球旋转的y的位置
    }

	// Update is called once per frame
	void Update () {
		if(isFly==false)
        {
            if(isReach==false)
            {
                //朝着startPoint运动
               transform.position =  Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime); 
               if(Vector3.Distance(transform.position,startPoint.position)<0.05f)
                {
                    isReach = true;  //当到达startPoint后，针就停止运动
                }
            }
        }else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetCirclePos, speed * Time.deltaTime);  //朝向圆运动
            if(Vector3.Distance(transform.position,targetCirclePos)<0.05f)
            {
                transform.position = targetCirclePos;
                transform.parent = circle;
                isFly = false;   //当到达球后，就停止飞行
            }
        }
	}

    public void StartFly()
    {
        isFly = true;
        isReach = true;
    }

}
