using Godot;

namespace ZeroBreak
{
    public partial class LevelManager : Control
    {
        public override void _Ready()
        {
            foreach (var child in GetNode("CanvasLayer/GridContainer").GetChildren())
            {
                if (child is Button button)
                {
                    button.Pressed += () => OnLevelButtonPressed(button.Name);
                }
            }
        }

        private void OnLevelButtonPressed(string buttonName)
        {
            var levelPath = $"res://scenes/levels/{buttonName}.tscn";
            PackedScene levelScene = (PackedScene)ResourceLoader.Load(levelPath);
            GetTree().ChangeSceneToPacked(levelScene);
        }
    }
}
