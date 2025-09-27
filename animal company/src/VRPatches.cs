using HarmonyLib;
using UnityEngine;
using BepInEx.Logging;

namespace AnimalCompanyVR.Patches
{
    /// <summary>
    /// Patches for Animal Company camera system to integrate VR
    /// </summary>
    [HarmonyPatch]
    public class CameraPatches
    {
        private static ManualLogSource Logger => AnimalCompanyVRPlugin.Logger;
        
        /// <summary>
        /// Patch the main camera to work with VR
        /// This is a generic patch - you'll need to replace with actual Animal Company camera class
        /// </summary>
        [HarmonyPatch(typeof(Camera), "Start")]
        [HarmonyPostfix]
        public static void PatchCameraStart(Camera __instance)
        {
            if (__instance.tag == "MainCamera")
            {
                Logger.LogInfo("Patching main camera for VR...");
                
                // Disable original camera if VR is active
                var vrManager = Object.FindObjectOfType<VRManager>();
                if (vrManager != null)
                {
                    Logger.LogInfo("VR Manager found, setting up camera integration");
                    
                    // Optionally disable original camera or modify it for VR
                    // __instance.enabled = false;
                    
                    // Or copy settings to VR camera
                    var vrCamera = vrManager.GetVRCamera();
                    if (vrCamera != null)
                    {
                        vrCamera.fieldOfView = __instance.fieldOfView;
                        vrCamera.nearClipPlane = __instance.nearClipPlane;
                        vrCamera.farClipPlane = __instance.farClipPlane;
                        vrCamera.cullingMask = __instance.cullingMask;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Patches for Animal Company player controller to integrate VR movement
    /// Replace "PlayerController" with actual Animal Company player class
    /// </summary>
    [HarmonyPatch]
    public class PlayerPatches
    {
        private static ManualLogSource Logger => AnimalCompanyVRPlugin.Logger;
        
        /// <summary>
        /// Example patch for player movement - replace with actual Animal Company player class
        /// </summary>
        [HarmonyPatch(typeof(MonoBehaviour), "Update")] // Replace with actual player class
        [HarmonyPrefix]
        public static bool PatchPlayerUpdate(MonoBehaviour __instance)
        {
            // Only patch if this is the player controller
            if (__instance.name.ToLower().Contains("player"))
            {
                var vrManager = Object.FindObjectOfType<VRManager>();
                if (vrManager != null)
                {
                    // VR is active, let VRManager handle movement
                    // Return false to skip original Update method
                    // return false;
                }
            }
            
            return true; // Continue with original method
        }
    }

    /// <summary>
    /// Patches for Animal Company UI to work in VR
    /// </summary>
    [HarmonyPatch]
    public class UIPatches
    {
        private static ManualLogSource Logger => AnimalCompanyVRPlugin.Logger;
        
        /// <summary>
        /// Patch Canvas components to work in VR world space
        /// </summary>
        [HarmonyPatch(typeof(Canvas), "Start")]
        [HarmonyPostfix]
        public static void PatchCanvasStart(Canvas __instance)
        {
            var vrManager = Object.FindObjectOfType<VRManager>();
            if (vrManager != null && __instance.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                Logger.LogInfo($"Converting Canvas {__instance.name} to world space for VR");
                
                // Convert to world space canvas
                __instance.renderMode = RenderMode.WorldSpace;
                __instance.worldCamera = vrManager.GetVRCamera();
                
                // Position canvas in front of player
                var vrRig = vrManager.GetVRRig();
                if (vrRig != null)
                {
                    __instance.transform.SetParent(vrRig);
                    __instance.transform.localPosition = new Vector3(0, 1.5f, 2f);
                    __instance.transform.localRotation = Quaternion.identity;
                    __instance.transform.localScale = Vector3.one * 0.001f; // Scale down for world space
                }
            }
        }
    }

    /// <summary>
    /// Patches for Animal Company animals to respond to VR interactions
    /// Replace with actual Animal Company animal classes
    /// </summary>
    [HarmonyPatch]
    public class AnimalPatches
    {
        private static ManualLogSource Logger => AnimalCompanyVRPlugin.Logger;
        
        /// <summary>
        /// Example patch for animal behavior in VR
        /// Replace with actual Animal Company animal class
        /// </summary>
        [HarmonyPatch(typeof(MonoBehaviour), "Start")] // Replace with actual animal class
        [HarmonyPostfix]
        public static void PatchAnimalStart(MonoBehaviour __instance)
        {
            // Only patch objects that appear to be animals
            if (__instance.name.ToLower().Contains("animal") || 
                __instance.gameObject.tag == "Animal") // Adjust based on actual game
            {
                Logger.LogInfo($"Adding VR interaction to animal: {__instance.name}");
                
                // Add VR interaction component
                var vrInteraction = __instance.gameObject.AddComponent<VRAnimalInteraction>();
                vrInteraction.Initialize(__instance);
            }
        }
    }
}