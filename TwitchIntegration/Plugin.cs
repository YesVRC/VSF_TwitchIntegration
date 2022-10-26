using BepInEx;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using BepInEx.Configuration;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using uOSC;


namespace TwitchIntegration
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {

        public ConfigEntry<string> baseColorKeywords;
        public ConfigEntry<string> emissiveColorKeywords;

        public uOscServer server;
        public GameObject Avatar;
        public List<SkinnedMeshRenderer> SMR;

        public string[] baseColorArray;
        public string[] emissiveColorArray;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            //MainChanges();
            Logger.LogInfo("Setting up Config");
            ConfigInit();


            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnload;
            if (baseColorKeywords.Value.Contains(' ')){
                baseColorArray = baseColorKeywords.Value.Split(' ');
            }
            else
            {
                baseColorArray = new string[1] { baseColorKeywords.Value };
            }

            if (emissiveColorKeywords.Value.Contains(' '))
            {
                emissiveColorArray = emissiveColorKeywords.Value.Split(' ');
            }
            else
            {
                emissiveColorArray = new string[1] { emissiveColorKeywords.Value };
            }

        }

        public void OnDataRecieved(Message message)
        {
            //OSC Reciever
            Logger.LogMessage($"Got message to {message.address}, with {message.values.Length}");
            
            if(Avatar.transform.childCount == 0)
            {
                Logger.LogError("No Avatar Loaded!");
                return;
            }

            SMR = Avatar.GetComponentsInChildren<SkinnedMeshRenderer>(true).ToList<SkinnedMeshRenderer>();

            switch (message.address)
            {
                case "TOsc/color":

                    var color = new Color((float)message.values[0], (float)message.values[1], (float)message.values[2]);

                    ColorSet(color, message);

                    break;

                case "TOsc/rainbow":
                    var colorR = new Color();
                    var random = new System.Random();
                    colorR.r = (float)random.NextDouble();
                    colorR.g = (float)random.NextDouble();
                    colorR.b = (float)random.NextDouble();
                    ColorSet(colorR, message);

                    break;
                case "TOsc/Test":
                    string logMessage = "";
                    foreach (var logPrint in Avatar.GetComponentsInChildren<Renderer>(true))
                    {
                        logMessage += $"{logPrint.gameObject.name}, ";
                    }
                    Logger.LogMessage($"Current bodies: {logMessage}");
                    break;
                case "TOsc/Reset":

                    break;


            }
            
        }

        public void ColorSet(Color color, Message message)
        {
            foreach (SkinnedMeshRenderer target in SMR)
            {
                foreach (Material mat in target.materials)
                {
                    if (message.values[3] != null && ((int)message.values[3] == 1 || (int)message.values[3] == 0))
                    {
                        foreach (string s in baseColorArray)
                        {
                            mat.SetColor(s, color);
                        }
                    }

                    if (message.values[3] != null && ((int)message.values[3] == 2 || (int)message.values[3] == 0))
                    {
                        foreach (string s in emissiveColorArray)
                        {
                            mat.SetColor(s, color * 2);
                        }
                    }
                }
            }
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            //Literally the entire mod
            Logger.LogInfo($"Scene Changed to {scene.name}");

            Avatar = GameObject.Find("AvatarContainer");
            Avatar.AddComponent<uOscServer>();
            server = Avatar.GetComponent<uOscServer>();
            server.port = 3334;
            server.onDataReceived.AddListener(OnDataRecieved);

            Logger.LogMessage($"OSC Server Init on {Avatar.name}, port:{server.port}");

        }

        public void OnSceneUnload(Scene scene)
        {

        }

        public void ConfigInit()
        {
            baseColorKeywords = Config.Bind("Material_Settings", "Base_Color_Keywords", "_Color", new ConfigDescription("Base Color for materials", null, null));
            emissiveColorKeywords = Config.Bind("Material_Settings", "Emissive_Color_Keywords", "_Color", new ConfigDescription("Emissive Color for materials", null, null));
        }

        
    }
}
