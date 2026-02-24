using MapEditTool.View3D;
using MapEditTool.ViewModel3D;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace MapEditTool.Services.Tools
{
    public abstract class EditorTool
    {
        protected readonly EditorState State;
        protected readonly LevelData Level;
        protected readonly LevelEditorService Service;
        protected readonly CommandManager CommandManager;
        protected readonly Map3DViewModel Map3D;

        protected EditorTool(EditorState state, LevelData level, LevelEditorService service, CommandManager commands, Map3DViewModel map3D)
        {
            State = state;
            Level = level;
            Service = service;
            CommandManager = commands;
            Map3D = map3D;
        }
        public virtual void OnActivated() { }
        public abstract void OnLeftClick(Viewport3D viewport, Point mousePos, RayMeshGeometry3DHitTestResult? hit, Map3DViewModel map3D);
        public virtual void OnMouseMove(Viewport3D viewport, Point mousePos, RayMeshGeometry3DHitTestResult? hit, Map3DViewModel map3D) { }
        public virtual void OnLeftRelease(Viewport3D viewport, Point mousePos, RayMeshGeometry3DHitTestResult? hit, Map3DViewModel map3D) { }
    }

}
