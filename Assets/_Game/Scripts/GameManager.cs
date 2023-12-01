using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] AllTubes;
    [SerializeField] Color[] AllColors;
    //[SerializeField] List<Color> SelectedColor;
    [SerializeField] List<GameObject> SelectedTubes, AllGeneratedBall;
    [SerializeField] GameObject BallPrefab;
    private void Start()
    {
        Debug.Log("Start");
        TubeSelection();
        BallGenerate();
    }
    private void Awake()
    {
        Debug.Log("Awakee");
    }
    private void Update()
    {

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
    //public void BallColorSet()
    //{
    //    List<Color> SelectedColor = new List<Color>(AllColors);

    //    for (int i = 0; i < AllGeneratedBall.Count; i++)
    //    {
    //        int BallIndex;
    //        do
    //        {
    //            BallIndex = Random.Range(0, AllColors.Length);
    //            Debug.Log("Generated index = " + BallIndex);
    //            if (AllColors != null && BallIndex >= 0 && BallIndex < AllColors.Length)
    //            {
    //                SelectedColor.Add(AllColors[BallIndex]);
    //            }
    //            else
    //            {
    //                Debug.Log("Out of range");
    //            }
    //        } while (SelectedColor.Contains(AllColors[BallIndex]));

    //        //SelectedColor.Add(AllColors[BallIndex]);

    //        for (int j = 0; j < 4; j++)
    //            {
    //                Debug.Log("Forrr");
    //                int val = Random.Range(0, AllGeneratedBall.Count);
    //                AllGeneratedBall[val].GetComponent<MeshRenderer>().material.color = AllColors[BallIndex];
    //                AllGeneratedBall.Remove(AllGeneratedBall[val]);
    //            }
    //        //}
    //    }
    //}
    public void BallColorSet()
    {
        List<Color> availableColors = new List<Color>(AllColors);

        for (int i = 0; i < AllGeneratedBall.Count; i++)
        {
            if (availableColors.Count == 0)
            {
                Debug.LogWarning("Not enough colors available.");
                break;
            }

            int randomColorIndex = Random.Range(0, availableColors.Count);
            Color selectedColor = availableColors[randomColorIndex];

            AllGeneratedBall[i].GetComponent<MeshRenderer>().material.color = selectedColor;

            if (i % 4 == 3)
            {
                // Remove the color from available colors when it has been used 4 times
                availableColors.RemoveAt(randomColorIndex);
            }
        }
    }

}
