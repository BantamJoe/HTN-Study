using UnityEngine;
using System.Collections;


public class ThirdPersonCameraController : MonoBehaviour {

    public float speed = 2.3f;
    public Quaternion rotation;
    public string defaultFocus = "PlayerFocus";

    private GameObject focus;
    private float angle;

    void Awake()
    {
        InternalEventManager.Instance.AddListener<CharacterFocusChangeEvent>(changeFocus);
    }

    void Start()
    {
        focus = (GameObject)GameObject.FindGameObjectWithTag(defaultFocus);
        //angle = focus.transform.eulerAngles.y;
        rotation = Quaternion.Euler(focus.transform.eulerAngles);
    }

    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, focus.transform.position, Time.deltaTime * speed);

        var wantedRotation = Quaternion.LookRotation(focus.transform.position - focus.transform.position, focus.transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * 10);

        Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.red);
    }

    void OnDestroy()
    {
    }

    private void changeFocus(CharacterFocusChangeEvent e)
    {
        focus = (GameObject)GameObject.FindGameObjectWithTag(e.Tag);
        angle = focus.transform.eulerAngles.y;
        rotation = Quaternion.Euler(0f, angle, 0f);
        Debug.Log("Camera Focus Changed to: " + e.Tag);
    }

}