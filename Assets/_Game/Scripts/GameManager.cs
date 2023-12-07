using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
                //Debug.Log("Forrr");
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
            if (clickedObj.transform.childCount > 5)
            {
                FirstClickedObject = clickedObj;
                FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).transform.DOMove(FirstClickedObject.transform.GetChild(4).transform.position,0.3f).SetEase(Ease.OutBounce);
                //position = FirstClickedObject.transform.GetChild(4).transform.position;
                flag = true;
            }
            else
            {
                Debug.Log("No ball in the tube");
            }
        }
        else
        {
            //Debug.Log("e");
            if(clickedObj.transform.childCount >= 9)
            {
                //Debug.Log("Tube full");
                FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).transform.DOMove(FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 6).transform.position, 0.3f);
                
                flag = false;
            }
            else if(clickedObj.transform.childCount == 5)
            {
                //Debug.Log("Else ifff");
                SecondClickedObject = clickedObj;
                FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).transform.parent = SecondClickedObject.transform;
                SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 1).transform.DOMove(SecondClickedObject.transform.GetChild(4).transform.position,0.3f)
                    .OnComplete(() =>
                    {
                        SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 1).transform.DOMove(SecondClickedObject.transform.GetChild(0).transform.position, 0.3f);
                    }).SetEase(Ease.InOutExpo);
                //SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 1).transform.position = SecondClickedObject.transform.GetChild(0).transform.position;
                flag = false;
            }
            else
            {
                SecondClickedObject = clickedObj;
                if (SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 1).tag == FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).tag)
                {
                    FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).transform.parent = SecondClickedObject.transform;
                    //SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 1).transform.position = ;


                    SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 1).transform.DOMove(SecondClickedObject.transform.GetChild(4).transform.position, 0.3f)
                    .OnComplete(() =>
                    {
                        SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 1).transform.DOMove(SecondClickedObject.transform.GetChild(SecondClickedObject.transform.childCount - 6).transform.position, 0.3f);
                    }).SetEase(Ease.InOutExpo);
                    flag = false;
                }
                else
                {
                    FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 1).transform.position = FirstClickedObject.transform.GetChild(FirstClickedObject.transform.childCount - 6).transform.position;
                    flag = false;
                }
            }   
        }
        BallSameColorCheck();
    }
    bool isChanged;
    [SerializeField] List<GameObject> WinList;
    void BallSameColorCheck()
    {
        WinList.Clear();
        isChanged = false;
        foreach(GameObject tube in AllTubes)
        {
            if (tube.transform.childCount >= 9)
            {
                WinList.Add(tube);
            }
            else
            {
                isChanged = false;
            }
        }
        if(WinList.Count == 3)
        {
            foreach (GameObject selectedtube in WinList)
            {
                for (int i = 5; i < 9; i++)
                {
                    Debug.Log("Six index = "+ selectedtube.transform.GetChild(6).gameObject.name);
                    Debug.Log("next index = "+ selectedtube.transform.GetChild(i).gameObject.name);
                    if (selectedtube.transform.GetChild(5).gameObject.tag != selectedtube.transform.GetChild(i).gameObject.tag)
                    {
                        Debug.Log("if workss");
                        isChanged = true;
                        break;
                    }
                }
            }
        }
        else
        {
            isChanged = true;
        }
        if(isChanged)
        {
            Debug.Log("continue play");
        }
        else
        {
            Debug.Log("Game completed");
        }
    }
}