using System.Collections.Generic;
using System.Text.Json;
using Godot;

namespace Pigdom.Screens.InputRemap;

public partial class InputRemap : Control
{
  private const string ControlSettingsFilePath = "user://control_settings.json";

  public PackedScene _mainMenuScene = GD.Load<PackedScene>("uid://bpnmgpokt7ayl");
  private Button _backButton;
  private VBoxContainer _remappingContainer;

  public override void _Ready()
  {
    _backButton = GetNode<Button>("BackButton");
    _remappingContainer = GetNode<VBoxContainer>("RemappingControl/VBoxContainer");

    _backButton.Pressed += OnBackButtonPressed;
    (FindChild("RemapButton") as Control)?.GrabFocus();
  }

  private void OnBackButtonPressed()
  {
    if (TrySaveInputSettings())
    {
      GetTree().ChangeSceneToPacked(_mainMenuScene);
    }
  }

  private bool TrySaveInputSettings()
  {
    try
    {
      using var file = FileAccess.Open(ControlSettingsFilePath, FileAccess.ModeFlags.Write);
      if (file == null)
      {
        GD.PrintErr($"Could not open {ControlSettingsFilePath} for writing");
        return false;
      }

      var mappings = CollectInputMappings();
      var jsonString = JsonSerializer.Serialize(
          mappings,
          new JsonSerializerOptions { WriteIndented = true }
      );
      file.StoreString(jsonString);
      return true;
    }
    catch (System.Exception ex)
    {
      GD.PrintErr($"Failed to save input settings: {ex.Message}");
      return false;
    }
  }

  private Dictionary<string, uint> CollectInputMappings()
  {
    var mappings = new Dictionary<string, uint>();

    if (_remappingContainer == null)
    {
      return mappings;
    }

    foreach (Node child in _remappingContainer.GetChildren())
    {
      if (child.GetChildCount() > 1)
      {
        var remapButton = child.GetChild(1);
        var action = remapButton.Get("binding_action").AsString();
        var events = InputMap.ActionGetEvents(action);

        if (events.Count > 0 && events[0] is InputEventKey keyEvent)
        {
          mappings[action] = (uint)keyEvent.PhysicalKeycode;
        }
      }
    }

    return mappings;
  }
}
