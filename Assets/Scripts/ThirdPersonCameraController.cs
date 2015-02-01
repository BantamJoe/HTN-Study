using UnityEngine;
using System.Collections;
using Managers;

public class ThirdPersonCameraController : MonoBehaviour {

    public float speed = 1.5f;
    public Vector3 followOffset;
    public Quaternion rotation;

    private GameObject focus;

    void Start()
    {
        focus = (GameObject)GameObject.FindGameObjectWithTag("PlayerHead");
        float angle = focus.transform.eulerAngles.y;
        rotation = Quaternion.Euler(0f, angle, 0f);
        rotation.y += 2.3f;
        followOffset = new Vector3(0.0f, 0.0f, -1.5f);

    }

    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, focus.transform.position - followOffset, Time.deltaTime * speed);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, Time.deltaTime * speed);
    }

    public void ChangeFocus(string focusTag)
    {
        focus = (GameObject)GameObject.FindGameObjectWithTag(focusTag);
        LogManager.Instance.Log("Camera Focus Changed to: " + focusTag);
    }

}