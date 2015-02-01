using UnityEngine;
using System.Collections;

public class LogTest : MonoBehaviour
{

    public int x = 25;
    void Update()
    {
        if (x != 0)
        {
            LogManager.Instance.Log("Hello there my name is Lisa!fffffffffffffff");
            x--;
        }
    }
}
