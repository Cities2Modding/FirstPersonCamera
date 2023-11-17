using FirstPersonCamera.MonoBehaviours;
using Game;
using Game.Common;
using Game.Rendering;
using Game.Tools;
using UnityEngine;

namespace FirstPersonCamera.Systems
{
    public class FirstPersonCameraSystem : GameSystemBase
    {
        private FirstPersonCameraController Controller
        {
            get;
            set;
        }

        private RenderingSystem _renderingSystem;
        private ToolRaycastSystem _toolRaycastSystem;
        private ToolSystem _toolSystem;

        protected override void OnCreate( )
        {
            base.OnCreate( );

            UnityEngine.Debug.Log( "FirstPersonCamera loaded!" );

            _renderingSystem = World.GetOrCreateSystemManaged<RenderingSystem>( );
            _toolSystem = World.GetExistingSystemManaged<ToolSystem>( );
            _toolRaycastSystem = World.GetExistingSystemManaged<ToolRaycastSystem>( );

            CreateOrGetController( );
        }

        protected override void OnUpdate( )
        {
        }

        private void CreateOrGetController()
        {
            var existingObj = GameObject.Find( nameof( FirstPersonCameraController ) );

            if ( existingObj != null )
                Controller = existingObj.GetComponent<FirstPersonCameraController>();
            else
                Controller = new GameObject( nameof( FirstPersonCameraController ) ).AddComponent<FirstPersonCameraController>();

            Controller.Initialise( this );
        }

        public void ToggleUI( bool hidden )
        {
            _renderingSystem.hideOverlay = hidden;
            Colossal.UI.UIManager.defaultUISystem.enabled = !hidden;

            if ( hidden )
            {
                _toolRaycastSystem.raycastFlags |= RaycastFlags.FreeCameraDisable;
                _toolSystem.activeTool = World.GetExistingSystemManaged<DefaultToolSystem>( );
            }
            else
                _toolRaycastSystem.raycastFlags &= ~RaycastFlags.FreeCameraDisable;
        }
    }
}
