using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoScroll : MonoBehaviour
{

    private Scrollbar scrollbar;

    void Start()
    {
        scrollbar = gameObject.GetComponent<Scrollbar>();
    }
    void Update()
    {
        scrollbar.value = 0.0f;
    }
}
