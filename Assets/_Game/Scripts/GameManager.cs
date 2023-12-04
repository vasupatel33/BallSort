using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] AllTubes;
    [SerializeField] List<Color> AllColors;
    //[SerializeField] List<Color> SelectedColors = new List<Color>();
    [SerializeField] List<GameObject> SelectedTubes, AllGeneratedBall;
    [SerializeField] GameObject BallPrefab;
    [SerializeField] string[] AllTags;

    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        TubeSelection();
        BallGenerate();
    }
    public void TubeSelection()
    {
        int RandomTubeIndex;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                RandomTubeIndex = Random.Range(0, AllTubes.Length);
            } while (SelectedTubes.Contains(AllTubes[RandomTubeIndex]));
            SelectedTubes.Add(AllTubes[RandomTubeIndex]);
        }
    }
    public void BallGenerate()
    {
        for (int i = 0; i < SelectedTubes.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject Ball = Instantiate(BallPrefab, SelectedTubes[i].transform.GetChild(j).position, Quaternion.identity, SelectedTubes[i].transform);
                AllGeneratedBall.Add(Ball);
            }
        }
        BallColorSet();
    }
    public void BallColorSet()
    {
        for (int i = 0; i < AllGeneratedBall.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Debug.Log("Forrr");
                int val = Random.Range(0, AllGeneratedBall.Count);
                AllGeneratedBall[val].GetComponent<MeshRenderer>().material.color = AllColors[i];  // Use SelectedColors[i] instead of AllColors[BallIndex]
                AllGeneratedBall[val].GetComponent<MeshRenderer>().tag = AllTags[i];
                AllGeneratedBall.RemoveAt(val);  // Use RemoveAt to remove by index
            }
        }
    }
    GameObject FirstClickedObject;
    public void ClickedBallUp(GameObject clickedObj)
    {
        FirstClickedObject = clickedObj;
        if(clickedObj.transform.childCount > 5)
        {
            clickedObj.transform.GetChild(clickedObj.transform.childCount - 1).transform.position = clickedObj.transform.GetChild(4).transform.position;
        }
        else
        {
            Debug.Log("No ball in the tube");
        }
        

    }
}
