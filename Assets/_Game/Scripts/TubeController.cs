using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeController : MonoBehaviour
{
    public static TubeController instance;
    private void Awake()
    {
        instance = this;
    }
    private void OnMouseUp()
    {
        Debug.Log("Tube name = "+this.gameObject.name);
        GameManager.Instance.ClickedBallUp(this.gameObject);
    }
    public void TubeFillCheck()
    {
        if(this.gameObject.transform.childCount == 9)
        {
            for(int i = 1 ; i <= 4 ; i++)
            {
                Debug.Log("Name = "+ this.gameObject.transform.GetChild(this.gameObject.transform.childCount - i).tag);
                Debug.Log("Second Name = "+ this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 1).tag);
                if(this.gameObject.transform.GetChild(this.gameObject.transform.childCount - i).tag == this.gameObject.transform.GetChild(this.gameObject.transform.childCount - i+1).tag)
                {
                    Debug.Log("Tube color matches");
                    if (this.gameObject.transform.GetChild(this.gameObject.transform.childCount - i + 2).tag == this.gameObject.transform.GetChild(this.gameObject.transform.childCount - i + 3).tag)
                    {
                        Debug.Log("if of the if color matches");
                    }
                    else
                    {
                        Debug.Log("Else of else color matches");
                    }
                }
                else
                {
                    Debug.Log("Color is not matchess");
                }
            }
        }
    }
}
