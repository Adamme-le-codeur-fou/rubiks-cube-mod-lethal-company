using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Rubik_s_Cube
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class RubiksPlugin : BaseUnityPlugin
    {
        private const string modGUID = "Poutre56.RubiksCube";
        private const string modName = "Rubik's Cube";
        private const string modVersion = "0.1.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        internal ManualLogSource mls;

        public static AssetBundle MyCustomAssets;

        void Awake()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("RUBIK'S CUBE MOD BY POUTRE56");

            string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            MyCustomAssets = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "rubikscubebundle"));
            if (MyCustomAssets == null)
            {
                mls.LogError("POUTRE56 <><><><><><><><><><><><> Failed to load rubiks bundle.");
                return;
            }
            else
            {
                mls.LogError("POUTRE56 <><><><><><><><><><><><> Rubiks bundle loaded successfully");
            }

             harmony.PatchAll(typeof(RubiksPlugin));
        }
    }
}
