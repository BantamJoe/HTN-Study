using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Managers
{
    [DebuggerDisplay("Camerca Focus Map ={CameraFocusMap}")]
    public class CameraControllerManager : MonoBehaviour
    {
        private Dictionary<KeyCode, string> cameraFocusControlMap = new Dictionary<KeyCode, string>();

        public static CameraControllerManager Instance { get; private set; }
        public Dictionary<KeyCode, string> CameraFocusMap { get { return cameraFocusControlMap; } }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            Instance = this;
            loadCameraControlMap();
            LogManager.Instance.Log("Camera Focus Manager initalized");
            DontDestroyOnLoad(gameObject);
        }

        void FixedUpdate()
        {
            if (Input.anyKeyDown)
            {

                foreach (KeyCode key in cameraFocusControlMap.Keys)
                {
                    if (Input.GetKeyDown(key))
                    {
                        string focusTag;
                        cameraFocusControlMap.TryGetValue(key, out focusTag);
                        InternalEventManager.Instance.Raise(new InputEvent(key, focusTag));
                    }
                }
            }
        }

        private void loadCameraControlMap()
        {
            cameraFocusControlMap.Add(KeyCode.Escape, "PlayerHead");
            cameraFocusControlMap.Add(KeyCode.F1, "AIHead1");
            cameraFocusControlMap.Add(KeyCode.F2, "AIHead2");
            cameraFocusControlMap.Add(KeyCode.F3, "AIHead3");
            cameraFocusControlMap.Add(KeyCode.F4, "AIHead4");
            cameraFocusControlMap.Add(KeyCode.F5, "AIHead5");
            cameraFocusControlMap.Add(KeyCode.Tab, "WorldPerspective");
        }
    }
}