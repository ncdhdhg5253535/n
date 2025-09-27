using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

namespace AnimalCompanyVR
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class AnimalCompanyVRPlugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        private static AnimalCompanyVRPlugin Instance;
        
        // VR Management
        private VRManager vrManager;
        private bool isVRInitialized = false;

        void Awake()
        {
            Logger = base.Logger;
            Instance = this;
            
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} is loaded!");
            
            // Apply Harmony patches
            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
            
            // Initialize VR components
            InitializeVR();
        }

        void Start()
        {
            // Create VR Manager
            GameObject vrManagerObject = new GameObject("AnimalCompanyVRManager");
            DontDestroyOnLoad(vrManagerObject);
            vrManager = vrManagerObject.AddComponent<VRManager>();
        }

        private void InitializeVR()
        {
            Logger.LogInfo("Initializing VR subsystem...");
            
            // Check if XR is already initialized
            if (XRGeneralSettings.Instance != null && XRGeneralSettings.Instance.Manager != null)
            {
                if (XRGeneralSettings.Instance.Manager.activeLoader != null)
                {
                    Logger.LogInfo("XR already initialized");
                    isVRInitialized = true;
                    return;
                }
            }

            // Initialize XR
            StartCoroutine(InitializeXR());
        }

        private System.Collections.IEnumerator InitializeXR()
        {
            Logger.LogInfo("Starting XR initialization...");
            
            if (XRGeneralSettings.Instance != null && XRGeneralSettings.Instance.Manager != null)
            {
                yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
                
                if (XRGeneralSettings.Instance.Manager.activeLoader != null)
                {
                    Logger.LogInfo("XR initialized successfully");
                    XRGeneralSettings.Instance.Manager.StartSubsystems();
                    isVRInitialized = true;
                }
                else
                {
                    Logger.LogError("Failed to initialize XR");
                }
            }
            else
            {
                Logger.LogError("XRGeneralSettings not found");
            }
        }

        void OnDestroy()
        {
            if (isVRInitialized && XRGeneralSettings.Instance?.Manager != null)
            {
                XRGeneralSettings.Instance.Manager.StopSubsystems();
                XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            }
        }

        public static AnimalCompanyVRPlugin GetInstance()
        {
            return Instance;
        }
    }
}