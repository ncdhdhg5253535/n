using UnityEngine;
using UnityEngine.XR;
using BepInEx.Logging;

namespace AnimalCompanyVR
{
    public class VRControllerHandler : MonoBehaviour
    {
        private static ManualLogSource Logger => AnimalCompanyVRPlugin.Logger;
        
        private XRNode controllerNode;
        private InputDevice inputDevice;
        
        // Controller state
        private bool isTracked = false;
        private Vector3 position;
        private Quaternion rotation;
        
        // Interaction components
        private LineRenderer laserPointer;
        private GameObject interactionSphere;

        public void Initialize(XRNode node)
        {
            controllerNode = node;
            SetupVisuals();
            Logger.LogInfo($"Initialized controller for {node}");
        }

        void Start()
        {
            // Get the input device
            inputDevice = InputDevices.GetDeviceAtXRNode(controllerNode);
        }

        void Update()
        {
            // Refresh device if needed
            if (!inputDevice.isValid)
            {
                inputDevice = InputDevices.GetDeviceAtXRNode(controllerNode);
            }
        }

        private void SetupVisuals()
        {
            // Create laser pointer
            GameObject laserObject = new GameObject($"LaserPointer_{controllerNode}");
            laserObject.transform.SetParent(transform);
            
            laserPointer = laserObject.AddComponent<LineRenderer>();
            laserPointer.material = CreateLaserMaterial();
            laserPointer.startWidth = 0.01f;
            laserPointer.endWidth = 0.005f;
            laserPointer.positionCount = 2;
            laserPointer.enabled = false;
            
            // Create interaction sphere (visual feedback)
            interactionSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            interactionSphere.transform.SetParent(transform);
            interactionSphere.transform.localScale = Vector3.one * 0.05f;
            
            // Make sphere semi-transparent
            var renderer = interactionSphere.GetComponent<Renderer>();
            var material = new Material(Shader.Find("Standard"));
            material.color = new Color(0.2f, 0.8f, 1.0f, 0.6f);
            material.SetFloat("_Mode", 2); // Fade mode
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
            renderer.material = material;
            
            // Remove collider from visual sphere
            Destroy(interactionSphere.GetComponent<Collider>());
        }

        public void UpdateTracking()
        {
            if (inputDevice.isValid)
            {
                // Update position and rotation
                if (inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out position))
                {
                    transform.localPosition = position;
                    isTracked = true;
                }

                if (inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out rotation))
                {
                    transform.localRotation = rotation;
                }

                // Update visuals based on tracking state
                if (interactionSphere != null)
                {
                    interactionSphere.SetActive(isTracked);
                }
            }
            else
            {
                isTracked = false;
                if (interactionSphere != null)
                    interactionSphere.SetActive(false);
            }
        }

        public void OnTriggerPressed()
        {
            Logger.LogInfo($"Trigger pressed on {controllerNode} controller");
            
            // Perform raycast for interactions
            PerformInteractionRaycast();
        }

        private void PerformInteractionRaycast()
        {
            RaycastHit hit;
            Vector3 rayOrigin = transform.position;
            Vector3 rayDirection = transform.forward;
            
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, 10f))
            {
                Logger.LogInfo($"Hit object: {hit.collider.gameObject.name}");
                
                // Show laser pointer
                ShowLaserPointer(rayOrigin, hit.point);
                
                // Handle specific object interactions
                HandleObjectInteraction(hit.collider.gameObject);
            }
            else
            {
                // Show laser pointing into distance
                ShowLaserPointer(rayOrigin, rayOrigin + rayDirection * 10f);
            }
        }

        private void HandleObjectInteraction(GameObject obj)
        {
            // Check for Animal Company specific objects
            if (obj.name.ToLower().Contains("animal"))
            {
                Logger.LogInfo($"Interacting with animal: {obj.name}");
                // Add animal-specific VR interactions here
                InteractWithAnimal(obj);
            }
            else if (obj.GetComponent<Collider>() != null)
            {
                Logger.LogInfo($"Generic object interaction: {obj.name}");
                // Add generic object interactions here
            }
        }

        private void InteractWithAnimal(GameObject animal)
        {
            // Example animal interaction - make them respond to VR presence
            var animalScript = animal.GetComponent<MonoBehaviour>();
            if (animalScript != null)
            {
                // Send VR interaction event
                Logger.LogInfo($"VR interaction with {animal.name}");
                
                // Add haptic feedback
                TriggerHapticFeedback(0.3f, 0.1f);
            }
        }

        private void ShowLaserPointer(Vector3 start, Vector3 end)
        {
            if (laserPointer != null)
            {
                laserPointer.enabled = true;
                laserPointer.SetPosition(0, start);
                laserPointer.SetPosition(1, end);
                
                // Hide laser after short delay
                Invoke(nameof(HideLaserPointer), 0.5f);
            }
        }

        private void HideLaserPointer()
        {
            if (laserPointer != null)
            {
                laserPointer.enabled = false;
            }
        }

        private void TriggerHapticFeedback(float amplitude, float duration)
        {
            if (inputDevice.isValid)
            {
                HapticCapabilities capabilities;
                if (inputDevice.TryGetHapticCapabilities(out capabilities))
                {
                    if (capabilities.supportsImpulse)
                    {
                        inputDevice.SendHapticImpulse(0, amplitude, duration);
                    }
                }
            }
        }

        private Material CreateLaserMaterial()
        {
            Material material = new Material(Shader.Find("Sprites/Default"));
            material.color = Color.cyan;
            return material;
        }

        public bool IsTracked()
        {
            return isTracked;
        }

        public Vector3 GetPosition()
        {
            return position;
        }

        public Quaternion GetRotation()
        {
            return rotation;
        }
    }
}