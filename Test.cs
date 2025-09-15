using Godot;
using System;

public partial class Test : Node2D
{
    [Export] private TestWindow[] maskWindows;
    public override void _Ready()
    {
        GetViewport().SetTransparentBackground(false);
        GetWindow().Title = "猜猜有啥";
        InitMaskWindows();
    }

    public override void _Process(double delta)
    {
        UpdateMaskWindows();
    }

    // 初始化遮罩窗口的位置和可见性
    private void InitMaskWindows()
    {
        var windowXOffset = 30;
        var windowYOffset = 30;
        var windowCount = 1;
        foreach (var window in maskWindows)
        {
            window.Hide();
            var mainWindowPos = GetWindow().Position;
            window.SetPosition(new Vector2I(mainWindowPos.X + windowCount * windowXOffset, mainWindowPos.Y + windowCount * windowYOffset));
            windowCount++;
        }
    }

    // 更新遮罩位置
    private void UpdateMaskWindows()
    {
        foreach (var window in maskWindows)
        {
            // 将窗口坐标转换为游戏世界坐标
            var panelPos = Vector2I.Zero - (GetWindow().Position - window.Position);
            
            window.HiddenContentPanel.Position = panelPos;
            if (window.IsVisible())
            {
                window.HiddenContentPanel.Size = window.Size;
            }
            else
            {
                window.HiddenContentPanel.Size = Vector2.Zero;
            }

            if (window.Mode == Window.ModeEnum.Minimized)
            {
                window.HiddenContentPanel.Size = Vector2.Zero;
            }
            
            // 被裁剪内容是遮罩的子集，移动遮罩会移动内容位置，需要对位移进行补偿
            window.HiddenContent.Position = Vector2I.Zero - panelPos;
        }
    }
    
    private void _on_button_pressed()
    {
        foreach (var window in maskWindows)
        {
            window.Show();
        }
    }

    // 如果窗口是当前焦点，则将该窗口显示的内容置顶
    private void _on_test_window_1_focus_entered()
    {
        var count = 1;
        foreach (var window in maskWindows)
        {
            if (count == 1)
            {
                window.HiddenContentPanel.ZIndex = 1;
            }
            else
            {
                window.HiddenContentPanel.ZIndex = 0;
            }
            count++;
        }
    }
    
    // 如果窗口是当前焦点，则将该窗口显示的内容置顶
    private void _on_test_window_2_focus_entered()
    {
        var count = 1;
        foreach (var window in maskWindows)
        {
            if (count == 2)
            {
                window.HiddenContentPanel.ZIndex = 1;
            }
            else
            {
                window.HiddenContentPanel.ZIndex = 0;
            }
            count++;
        }
    }
}
