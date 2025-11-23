using System.Collections.Generic;
using Godot;

namespace Pigdom.Extensions;

public static class AnimationTreeExtensions
{
    private static readonly Dictionary<AnimationTree, string> _paths =
        new Dictionary<AnimationTree, string>();

    private static string _defaultPath = "parameters/conditions";

    public static void SetConditionPath(this AnimationTree animationTree, string newPath)
    {
        if (string.IsNullOrEmpty(newPath))
        {
            _paths.Remove(animationTree);
        }
        else
        {
            _paths[animationTree] = newPath;
        }
    }

    public static string GetConditionPath(this AnimationTree animationTree)
    {
        return _paths.TryGetValue(animationTree, out var value) ? value : _defaultPath;
    }

    public static void SetCondition(
        this AnimationTree animationTree,
        StringName condition,
        bool value
    )
    {
        animationTree.Set($"{animationTree.GetConditionPath()}/{condition}", value);
    }

    public static void EnableCondition(this AnimationTree animationTree, StringName condition)
    {
        animationTree.SetCondition(condition, true);
    }

    public static void DisableCondition(this AnimationTree animationTree, StringName condition)
    {
        animationTree.SetCondition(condition, false);
    }
}
