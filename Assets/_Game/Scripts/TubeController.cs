using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeController : MonoBehaviour
{
    private void OnMouseUp()
    {
        Debug.Log("Tube name = "+this.gameObject.name);
    }
}
