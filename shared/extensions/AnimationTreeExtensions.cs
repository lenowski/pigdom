using Godot;

namespace Pigdom.Extensions;

public static class AnimationTreeExtensions
{
    public static void SetCondition(
        this AnimationTree animationTree,
        StringName condition,
        bool value
    )
    {
        animationTree.Set($"parameters/conditions/{condition}", value);
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
