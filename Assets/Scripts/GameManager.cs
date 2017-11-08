using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {


    //private Transform startPoint;

    private Transform spawnPoint;
    private Pin currentPin;

    public GameObject PinPrefab;
    private int score = 0;

    public Text scoreText;

    private bool isGameOver = false;

    private Camera mainCamera;

    public float speed = 3f; //动画的速度

	// Use this for initialization
	void Start () {
        //startPoint = GameObject.Find("StartPoint").transform;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        mainCamera = Camera.main;
        SpawnPin();
    }
	
	// Update is called once per frame
	void Update () {

        if (isGameOver)
            return;

		if(Input.GetMouseButtonDown(0))
        {
            score++;
            scoreText.text = score.ToString();
            currentPin.StartFly();  //针开始飞行
            SpawnPin();   //重新实例化一个新的针
        }
	}

    void SpawnPin()
    {
        currentPin=  GameObject.Instantiate(PinPrefab, spawnPoint.position, PinPrefab.transform.rotation).GetComponent<Pin>();  //实例化针
    }

    public void GameOver()
    {
        if (isGameOver)
            return;
        GameObject.Find("Circle").GetComponent<RotateSelf>().enabled = false;  //让球不再旋转，就是停止运行
        StartCoroutine(GameOverAnimation());
        isGameOver = true;
    }

    IEnumerator GameOverAnimation()
    {
        while (true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, speed * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, speed * Time.deltaTime);
            if(Mathf.Abs(mainCamera.orthographicSize-4)<0.01f)
            {
                break;
            }
            yield return 0;
        }
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
