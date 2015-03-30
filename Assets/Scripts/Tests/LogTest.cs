using UnityEngine;
using System.Collections;

public class LogTest : MonoBehaviour
{

    public int x = 25;
    bool f = true;
    void Update()
    {
        if (x != 0)
        {
            Debug.Log("Hello there my name is Lisa!fffffffffffffff");
            x--;
        }
        else if ((x == 0) && f)
        {
            Debug.Log("weeee");
            f = false;
        }
    }
}
