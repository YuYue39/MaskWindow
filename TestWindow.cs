using Godot;
using System;

public partial class TestWindow : Window
{
    [Export] public Panel HiddenContentPanel;
    [Export] public Sprite2D HiddenContent;
    public override void _Ready()
    {
        Transparent = true;
        AlwaysOnTop = true;
        Size = new Vector2I(300, 300);
        SharpCorners = true;
        Title = "看看";
    }

    private void _on_close_requested()
    {
        Hide();
    }
}
