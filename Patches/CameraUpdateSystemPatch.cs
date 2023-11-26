using HarmonyLib;
using FirstPersonCamera.Systems;
using Game.Rendering;

namespace FirstPersonCamera.Patches
{
    [HarmonyPatch( typeof( CameraUpdateSystem ), "OnUpdate" )]
    class CameraUpdateSystem_Patch
    {
        static void Prefix( CameraUpdateSystem __instance )
        {
            var camera = __instance.World.GetExistingSystemManaged<FirstPersonCameraSystem>( );

            if ( camera == null )
                return;

            camera.UpdateCamera();
        }
    }
}
