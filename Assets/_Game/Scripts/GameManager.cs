using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] AllTubes;
    [SerializeField] List<Color> AllColors;
    [SerializeField] List<Color> RandomlySelectedColor = new List<Color>();
    [SerializeField] List<int> RandomlyColorIntIndex;
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
            int RandomColorIndex;
            do
            {
                RandomColorIndex = Random.Range(0,AllColors.Count);

            } while (RandomlySelectedColor.Contains(AllColors[RandomColorIndex]));
            RandomlySelectedColor.Add(AllColors[RandomColorIndex]);
            RandomlyColorIntIndex.Add(RandomColorIndex);
            for (int j = 0; j < 4; j++)
            {
                Debug.Log("Forrr");
                int val = Random.Range(0, AllGeneratedBall.Count);
                AllGeneratedBall[val].GetComponent<MeshRenderer>().material.color = RandomlySelectedColor[i];
                AllGeneratedBall[val].GetComponent<MeshRenderer>().tag = AllTags[RandomlyColorIntIndex[i]];
                AllGeneratedBall.RemoveAt(val);  // Use RemoveAt to remove by index
            }
        }
    }
    bool flag;
    GameObject FirstClickedObject, SecondClickedObject;
    public void ClickedBallUp(GameObject clickedObj)
    {
        if (!flag)
        {
            Debug.Log("i");
            if (clickedObj.transform.childCount > 5)
            {
                Debug.Log("IF");
                FirstClickedObject = clickedObj;
                FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).transform.position = FirstClickedObject.transform.GetChild(4).transform.position;
                flag = true;
            }
            else
            {
                Debug.Log("No ball in the tube");
            }
        }
        else
        {
            Debug.Log("e");
            if(clickedObj.transform.childCount >= 9)
            {
                Debug.Log("Tube full");
                FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).transform.position = FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 6).transform.position;
                flag = false;
            }
            else if(clickedObj.transform.childCount == 5)
            {
                Debug.Log("Else ifff");
                SecondClickedObject = clickedObj;
                FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).transform.parent = SecondClickedObject.transform;
                SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 1).transform.position = SecondClickedObject.transform.GetChild(0).transform.position;
                flag = false;
            }
            else
            {
                SecondClickedObject = clickedObj;
                if (SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 1).tag == FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).tag)
                {
                    FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).transform.parent = SecondClickedObject.transform;
                    SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 1).transform.position = SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 6).transform.position;
                    flag = false;
                    Debug.Log("Else of else");
                }
                else
                {
                    //SecondClickedObject = clickedObj;
                    FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).transform.position = FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 6).transform.position;
                    flag = false;
                    Debug.Log("eeeeeeeeeeeeeeeeeee");
                }
            }
            
        }
    }
}
