using UnityEngine;
using UnityEngine.XR;
using BepInEx.Logging;

namespace AnimalCompanyVR
{
    public class VRManager : MonoBehaviour
    {
        private static ManualLogSource Logger => AnimalCompanyVRPlugin.Logger;
        
        // VR Components
        private Camera vrCamera;
        private GameObject vrRig;
        private VRControllerHandler leftController;
        private VRControllerHandler rightController;
        
        // VR Settings
        public bool enableTeleportation = true;
        public bool enableSmoothLocomotion = true;
        public float locomotionSpeed = 3.0f;
        public float rotationSpeed = 45.0f;

        void Start()
        {
            Logger.LogInfo("VRManager starting...");
            SetupVRRig();
            SetupControllers();
        }

        void Update()
        {
            HandleVRInput();
            UpdateVRTracking();
        }

        private void SetupVRRig()
        {
            Logger.LogInfo("Setting up VR rig...");
            
            // Create VR rig
            vrRig = new GameObject("VRRig");
            vrRig.transform.position = Vector3.zero;
            
            // Setup VR camera
            GameObject cameraObject = new GameObject("VRCamera");
            cameraObject.transform.SetParent(vrRig.transform);
            
            vrCamera = cameraObject.AddComponent<Camera>();
            vrCamera.tag = "MainCamera";
            
            // Add XR components
            var inputModule = cameraObject.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            
            Logger.LogInfo("VR rig setup complete");
        }

        private void SetupControllers()
        {
            Logger.LogInfo("Setting up VR controllers...");
            
            // Left Controller
            GameObject leftControllerObject = new GameObject("LeftController");
            leftControllerObject.transform.SetParent(vrRig.transform);
            leftController = leftControllerObject.AddComponent<VRControllerHandler>();
            leftController.Initialize(XRNode.LeftHand);
            
            // Right Controller
            GameObject rightControllerObject = new GameObject("RightController");
            rightControllerObject.transform.SetParent(vrRig.transform);
            rightController = rightControllerObject.AddComponent<VRControllerHandler>();
            rightController.Initialize(XRNode.RightHand);
            
            Logger.LogInfo("VR controllers setup complete");
        }

        private void HandleVRInput()
        {
            // Handle locomotion
            if (enableSmoothLocomotion)
            {
                HandleSmoothLocomotion();
            }
            
            // Handle interactions
            HandleControllerInteractions();
        }

        private void HandleSmoothLocomotion()
        {
            // Get thumbstick input from right controller
            Vector2 thumbstick = Vector2.zero;
            InputDevice rightDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            
            if (rightDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out thumbstick))
            {
                if (thumbstick.magnitude > 0.1f)
                {
                    Vector3 movement = vrCamera.transform.TransformDirection(new Vector3(thumbstick.x, 0, thumbstick.y));
                    movement.y = 0; // Remove vertical component
                    vrRig.transform.position += movement * locomotionSpeed * Time.deltaTime;
                }
            }
        }

        private void HandleControllerInteractions()
        {
            // Handle trigger press for interactions
            bool leftTrigger, rightTrigger;
            
            InputDevice leftDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            InputDevice rightDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            
            leftDevice.TryGetFeatureValue(CommonUsages.triggerButton, out leftTrigger);
            rightDevice.TryGetFeatureValue(CommonUsages.triggerButton, out rightTrigger);
            
            if (leftTrigger)
            {
                leftController.OnTriggerPressed();
            }
            
            if (rightTrigger)
            {
                rightController.OnTriggerPressed();
            }
        }

        private void UpdateVRTracking()
        {
            // Update controller positions and rotations
            if (leftController != null)
                leftController.UpdateTracking();
                
            if (rightController != null)
                rightController.UpdateTracking();
        }

        public Camera GetVRCamera()
        {
            return vrCamera;
        }

        public Transform GetVRRig()
        {
            return vrRig?.transform;
        }
    }
}