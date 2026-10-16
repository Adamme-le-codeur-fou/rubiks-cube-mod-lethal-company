using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LobbyCompatibility.Attributes;
using LobbyCompatibility.Enums;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

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

            //Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");



            string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            AssetBundle RubiksBundle = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "rubikscubebundle"));

            if (RubiksBundle == null)
            {
                Logger.LogDebug("FAILED TO LOAD RUBIKS CUBE BUNDLE");
                return;
            }
            else
            {
                Logger.LogDebug("RUBIKS CUBE BUNDLE LOADED SUCCEFULLY");
            }

            Item RubiksItem = RubiksBundle.LoadAsset<Item>("Assets/RubiksCube/Rubik's Cube.asset");
            RotatingScrap rotatingScrap = RubiksItem.spawnPrefab.AddComponent<RotatingScrap>();
            rotatingScrap.itemProperties = RubiksItem;
            rotatingScrap.grabbable = true;
            rotatingScrap.isInFactory = true;
            rotatingScrap.grabbableToEnemies = true;

            int iRarity = 30;
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(RubiksItem.spawnPrefab);
            LethalLib.Modules.Items.RegisterScrap(RubiksItem, iRarity, LethalLib.Modules.Levels.LevelTypes.All);



            Harmony.PatchAll();

            Logger.LogDebug("Finished patching!");
        }

        //internal static void Unpatch()
        //{
        //    Logger.LogDebug("Unpatching...");

        //    Harmony?.UnpatchSelf();

        //    Logger.LogDebug("Finished unpatching!");
        //}
    }
}
