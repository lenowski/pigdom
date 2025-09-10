using System.Collections.Generic;
using System.Text.Json;
using Godot;

namespace Pigdom.Screens.MainMenu;

public partial class MainMenu : Control
{
    private const string ControlSettingsFilePath = "user://control_settings.json";

    [Export]
    public PackedScene StartScene { get; private set; }

    [Export]
    public PackedScene ControlsScene { get; private set; }

    private Button _startButton;
    private Button _quitButton;
    private Button _controlsButton;

    public override void _Ready()
    {
        StartScene = GD.Load<PackedScene>("res://levels/Level.tscn");
        ControlsScene = GD.Load<PackedScene>("res://screens/input_remap/InputRemap.tscn");

        _startButton = GetNode<Button>("VBoxContainer/StartButton");
        _quitButton = GetNode<Button>("VBoxContainer/QuitButton");
        _controlsButton = GetNode<Button>("VBoxContainer/ControlsButton");

        _startButton.Pressed += OnStartButtonPressed;
        _quitButton.Pressed += OnQuitButtonPressed;
        _controlsButton.Pressed += OnControlsButtonPressed;

        _startButton.GrabFocus();

        LoadInputMap();
    }

    private void LoadInputMap()
    {
        if (!FileAccess.FileExists(ControlSettingsFilePath))
        {
            GD.Print($"File doesn't exists: {ControlSettingsFilePath}");
            return;
        }

        using var file = FileAccess.Open(ControlSettingsFilePath, FileAccess.ModeFlags.Read);
        if (file is null)
        {
            GD.PrintErr($"Failed to open file: {ControlSettingsFilePath}");
            return;
        }

        var jsonText = file.GetAsText();
        try
        {
            var controlMappings = JsonSerializer.Deserialize<Dictionary<string, uint>>(jsonText);
            if (controlMappings is not null)
            {
                ApplyControlMappings(controlMappings);
            }
        }
        catch (JsonException ex)
        {
            GD.PrintErr($"Failed to parse control settings JSON: {ex.Message}");
        }
    }

    private void ApplyControlMappings(Dictionary<string, uint> controlMappings)
    {
        foreach (var (actionName, keyCode) in controlMappings)
        {
            var inputEvent = new InputEventKey { PhysicalKeycode = (Key)keyCode };

            InputMap.ActionEraseEvents(actionName);
            InputMap.ActionAddEvent(actionName, inputEvent);
        }
    }

    private void OnStartButtonPressed()
    {
        if (StartScene is null)
        {
            GD.PrintErr("StartScene is not assigned.");
            return;
        }
        GetTree().ChangeSceneToPacked(StartScene);
    }

    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }

    private void OnControlsButtonPressed()
    {
        if (ControlsScene is null)
        {
            GD.PrintErr("ControlsScene is not assigned.");
            return;
        }
        GetTree().ChangeSceneToPacked(ControlsScene);
    }
}
