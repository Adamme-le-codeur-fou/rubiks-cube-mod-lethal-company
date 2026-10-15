using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LobbyCompatibility.Attributes;
using LobbyCompatibility.Enums;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Rubiks_Cube
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency(LethalLib.Plugin.ModGUID)]
    public class Rubiks_Cube : BaseUnityPlugin
    {
        public static Rubiks_Cube Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");



            string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            AssetBundle MyAssetBundle = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "rubikscubebundle"));

            if (MyAssetBundle == null)
            {
                Logger.LogDebug("FAILED TO LOAD RUBIKS CUBE BUNDLE");
                return;
            }
            else
            {
                Logger.LogDebug("RUBIKS CUBE BUNDLE LOADED SUCCEFULLY");
            }

            int iRarity = 30;
            Item MyCustomItem = MyAssetBundle.LoadAsset<Item>("Assets/RubiksCube/Rubik's Cube.asset");
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(MyCustomItem.spawnPrefab);
            LethalLib.Modules.Items.RegisterScrap(MyCustomItem, iRarity, LethalLib.Modules.Levels.LevelTypes.All);



            Harmony.PatchAll();

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch()
        {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }
    }
}
