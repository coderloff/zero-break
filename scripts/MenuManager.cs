using Godot;

namespace ZeroBreak
{
    public partial class MenuManager : Control
    {
        public void _on_campaign_pressed()
        {
            LoadScene("res://scenes/Levels.tscn");
        }
        
        private void LoadScene(string name)
        {
            GetTree().ChangeSceneToFile(name);
        }
    }
}