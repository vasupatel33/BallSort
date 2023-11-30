using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] AllTubes;
    [SerializeField] Color[] AllColors;
    [SerializeField] List<GameObject> SelectedTubes;
    [SerializeField] List<Color> SelectedColors;
    [SerializeField] GameObject BallPrefab;
    private void Start()
    {
        Debug.Log("1");
        TubeSelection();
        Debug.Log("2");
        TubeColorSet();
        Debug.Log("3");
    }
    private void Update()
    {
        Debug.Log("-");
    }
    public void TubeSelection()
    {
        int RandomTubeIndex;
        Debug.Log("Func");
        for(int i = 0 ; i < 3 ; i++)
        {
            Debug.Log("Forr");
            do
            {
                Debug.Log("While");
                RandomTubeIndex = Random.Range(0, AllTubes.Length);
            } while (SelectedTubes.Contains(AllTubes[RandomTubeIndex]));
            SelectedTubes.Add(AllTubes[RandomTubeIndex]);
            Debug.Log("Cube added");
        }
    }
    public void TubeColorSet()
    {
        int RandomBallColor;
        for(int i = 0;i < SelectedTubes.Count;i++)
        {
            for(int j = 0 ; j < 4 ; j++)
            {
                GameObject Ball = Instantiate(BallPrefab, SelectedTubes[i].transform.GetChild(j).position, Quaternion.identity, SelectedTubes[i].transform);
                Debug.Log("Ball generated");
                do
                {
                    RandomBallColor = Random.Range(0, AllColors.Length);//2
                    Debug.Log("While called");
                } while (SelectedColors.Contains(AllColors[RandomBallColor]));//2
                Debug.Log("Index = "+RandomBallColor);
                SelectedColors.Add(AllColors[RandomBallColor]);//2
                Ball.GetComponent<MeshRenderer>().material.color = AllColors[RandomBallColor];
                Debug.Log("Ball name = "+Ball.gameObject.name);
            }
        }
    }
}
