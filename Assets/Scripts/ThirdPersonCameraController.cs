using UnityEngine;
using System.Collections;
using Managers;

public class ThirdPersonCameraController : MonoBehaviour {

    public float speed = 1.5f;
    //public Vector3 followOffset;
    public Quaternion rotation;
    public string defaultFocus = "PlayerFocus";

    private GameObject focus;
    private float angle;

    void Awake()
    {
        InternalEventManager.Instance.AddListener<InputEvent>(changeFocus);
    }

    void Start()
    {
        focus = (GameObject)GameObject.FindGameObjectWithTag(defaultFocus);
        angle = focus.transform.eulerAngles.y;
        rotation = Quaternion.Euler(0f, angle, 0f);
    }

    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, focus.transform.position, Time.deltaTime * speed);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, Time.deltaTime * speed);
        Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.red);
    }

    void OnDestroy()
    {
    }

    private void changeFocus(InputEvent e)
    {
        focus = (GameObject)GameObject.FindGameObjectWithTag(e.Tag);
        angle = focus.transform.eulerAngles.y;
        rotation = Quaternion.Euler(0f, angle, 0f);
        LogManager.Instance.Log("Camera Focus Changed to: " + e.Tag);
    }

}