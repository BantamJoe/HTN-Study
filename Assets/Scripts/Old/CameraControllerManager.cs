using UnityEngine;
using System;
using System.Collections.Generic;


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
            Debug.Log("Camera Focus Manager initalized");
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
                        InternalEventManager.Instance.Raise(new CharacterFocusChangeEvent(key, focusTag));
                    }
                }
            }
        }

        private void loadCameraControlMap()
        {
            cameraFocusControlMap.Add(KeyCode.Escape, "PlayerFocus");
            cameraFocusControlMap.Add(KeyCode.F1, "AIFocus_1");
            cameraFocusControlMap.Add(KeyCode.F2, "AIFocus_2");
            cameraFocusControlMap.Add(KeyCode.F3, "AIFocus_3");
            cameraFocusControlMap.Add(KeyCode.F4, "AIFocus_4");
            cameraFocusControlMap.Add(KeyCode.F5, "AIFocus_5");
            cameraFocusControlMap.Add(KeyCode.Tab, "WorldPerspective");
        }
    }