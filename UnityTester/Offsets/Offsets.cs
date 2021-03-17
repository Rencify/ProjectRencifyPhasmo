using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityTester.Offsets
{
    class Offsets
    {
        // Modules
        public static string gameModule = "GameAssembly.dll+";
        public static string unityModule = "UnityPlayer.dll+";

        // Entities
        public static string playerStatsEntity = gameModule + "029CFCB8,";
        public static string playerCoordsEntity = unityModule + "0176B300,";

        public static string ghostEntity = gameModule + "029B2B28,";

        public static string torchEntity = unityModule + "01805C78,";
        public static string fullbright = unityModule + "01806148,";

        public static string mouseEntity = gameModule + "029B4250,";
        public static string mouseEntity2 = gameModule + "029B3B90,";

        // Player Values
        public static string Name = gameModule + "027CF970,BA0,18,10,48,F8,20,14";
        public static string Money = playerStatsEntity + "18,58,B8,10,18,28,18";
        public static string Rank = playerStatsEntity + "18,58,B8,10,18,20,18";
        public static string Basketball = unityModule + "018711C0,430,228,10,98,88,60,18";
        public static string Height = playerCoordsEntity + "CC8,0,370,10,68,0,264";

        public static string GrabDistance = gameModule + "029E91E0,18,B8,0,18,10,148,18";
        public static string Sanity = gameModule + "029BC0C8,B8,0,140,198,40,C0,28";

        public static string FOV = unityModule + "017C2288,0,10,20,15C";

        // Player Coords
        public static string XValue = playerCoordsEntity + "CC8,0,370,10,68,0,204";
        public static string YValue = playerCoordsEntity + "CC8,0,370,10,68,0,1FC";
        public static string ZValue = playerCoordsEntity + "CC8,0,370,10,68,0,1F4";
        public static string Collision = playerCoordsEntity + "CC8,0,370,10,68,0,248";

        // Player Speed
        public static string WalkSpeed = "GameAssembly.dll+212B938";
        public static string RunSpeed = "GameAssembly.dll+21264A4";

        // Environment
        public static string Gravity = gameModule + "027CF970,C30,178,300,198,40,140,28";

        // Torch Entity
        public static string TorchIntensity = torchEntity + "0,30,10,3C";
        public static string TorchRange = torchEntity + "0,30,10,40";
        public static string TorchSpotAngle = torchEntity + "0,30,10,4C";

        // Torch Entity
        public static string FullbrightIntensity = fullbright + "70,30,10,3C";
        public static string FullbrightRange = fullbright + "70,30,10,40";
        public static string FullbrightSpotAngle = fullbright + "70,30,10,4C";

        // Mouse Settings
        public static string cam1 = mouseEntity + "B8,0,C8,140,30,20";
        public static string camIngame1 = mouseEntity2 + "B8,0,40,140,30,20";
        public static string cam2 = mouseEntity + "B8,0,C8,140,30,1C";
        public static string camIngame2 = mouseEntity2 + "B8,0,40,140,30,1C";

        // Visions
        public static string Visions = "GameAssembly.dll+2118960";
        public static string BrightnessVisions = "GameAssembly.dll+2125644";
        public static string ColourVision = "GameAssembly.dll+212562C";

        //Ghost Info
        public static string ghostName = ghostEntity + "B8,20,30,10,28,38,30,14";
        public static string ghostAge = ghostEntity + "B8,20,30,10,28,38,24";
        public static string ghostType = ghostEntity + "B8,20,30,10,28,38,20";
        public static string ghostFavRoom = ghostEntity + "B8,20,30,10,28,38,58,50,14";
        public static string ghostSpeed = ghostEntity + "B8,20,30,10,28,88";
        public static string ghostIsHunting = ghostEntity + "B8,20,30,10,28,94";
        public static string ghostIsAppearing = ghostEntity + "B8,20,30,10,28,70";
        public static string ghostAppearTimer = ghostEntity + "B8,20,30,10,28,90";
        public static string ghostCanHunt = ghostEntity + "B8,20,30,10,28,8C";
        public static string ghostCanAttack = ghostEntity + "B8,20,30,10,28,95";
        public static string ghostCanWander = ghostEntity + "B8,20,30,10,28,97";
    }
}
