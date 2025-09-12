using Godot;

namespace ZeroBreak
{
    public partial class MenuManager : Control
    {
        [Export] private Button campaignButton;
        [Export] private Button optionsButton;
        [Export] private Button exitButton;
        [Export] private PackedScene levelScene;

        public override void _Ready()
        {
            Input.SetMouseMode(Input.MouseModeEnum.Hidden);
            
            campaignButton.GrabFocus();
            campaignButton.Pressed += () => LoadScene(levelScene);
            exitButton.Pressed += () => GetTree().Quit();
        }
        
        private void LoadScene(PackedScene scene)
        {
            GetTree().ChangeSceneToPacked(scene);
        }
    }
}