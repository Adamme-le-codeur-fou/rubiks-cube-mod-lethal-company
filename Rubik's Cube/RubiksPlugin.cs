using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        void Awake()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("RUBIK'S CUBE MOD BY POUTRE56");

            harmony.PatchAll(typeof(RubiksPlugin));
        }
    }
}
