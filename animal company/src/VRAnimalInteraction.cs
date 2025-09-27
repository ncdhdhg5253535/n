using UnityEngine;
using BepInEx.Logging;

namespace AnimalCompanyVR
{
    /// <summary>
    /// Component added to animals to enable VR-specific interactions
    /// </summary>
    public class VRAnimalInteraction : MonoBehaviour
    {
        private static ManualLogSource Logger => AnimalCompanyVRPlugin.Logger;
        
        private MonoBehaviour originalAnimalScript;
        private Renderer animalRenderer;
        private Color originalColor;
        private bool isHighlighted = false;
        
        // VR interaction settings
        public float interactionDistance = 2.0f;
        public float highlightIntensity = 1.5f;
        
        public void Initialize(MonoBehaviour animalScript)
        {
            originalAnimalScript = animalScript;
            animalRenderer = GetComponent<Renderer>();
            
            if (animalRenderer != null && animalRenderer.material != null)
            {
                originalColor = animalRenderer.material.color;
            }
            
            Logger.LogInfo($"VR interaction initialized for {gameObject.name}");
        }

        void Update()
        {
            CheckVRProximity();
        }

        private void CheckVRProximity()
        {
            var vrManager = FindObjectOfType<VRManager>();
            if (vrManager == null) return;

            var vrRig = vrManager.GetVRRig();
            if (vrRig == null) return;

            float distance = Vector3.Distance(transform.position, vrRig.position);
            
            if (distance <= interactionDistance)
            {
                if (!isHighlighted)
                {
                    HighlightAnimal();
                }
            }
            else
            {
                if (isHighlighted)
                {
                    UnhighlightAnimal();
                }
            }
        }

        private void HighlightAnimal()
        {
            if (animalRenderer != null && animalRenderer.material != null)
            {
                Color highlightColor = originalColor * highlightIntensity;
                animalRenderer.material.color = highlightColor;
                isHighlighted = true;
                
                Logger.LogInfo($"Highlighting animal: {gameObject.name}");
            }
        }

        private void UnhighlightAnimal()
        {
            if (animalRenderer != null && animalRenderer.material != null)
            {
                animalRenderer.material.color = originalColor;
                isHighlighted = false;
            }
        }

        public void OnVRInteraction(Vector3 controllerPosition)
        {
            Logger.LogInfo($"VR interaction with {gameObject.name} at {controllerPosition}");
            
            // Make animal look towards the controller
            Vector3 lookDirection = controllerPosition - transform.position;
            lookDirection.y = 0; // Keep on same horizontal plane
            
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
            
            // Trigger animal-specific behavior
            TriggerAnimalResponse();
        }

        private void TriggerAnimalResponse()
        {
            // Example responses - customize based on animal type
            StartCoroutine(AnimateResponse());
        }

        private System.Collections.IEnumerator AnimateResponse()
        {
            Vector3 originalScale = transform.localScale;
            Vector3 targetScale = originalScale * 1.2f;
            
            // Scale up
            float time = 0;
            while (time < 0.2f)
            {
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(originalScale, targetScale, time / 0.2f);
                yield return null;
            }
            
            // Scale back down
            time = 0;
            while (time < 0.2f)
            {
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(targetScale, originalScale, time / 0.2f);
                yield return null;
            }
            
            transform.localScale = originalScale;
        }

        void OnDrawGizmosSelected()
        {
            // Draw interaction radius in editor
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, interactionDistance);
        }
    }
}