using Godot;

namespace Pigdom.Shared.Prefabs;

public partial class PassThroughCharacter2D : BasicMovingCharacter2D
{
    [Export(PropertyHint.Layers2DPhysics)]
    private int PassThroughLayers { get; set; } = 2;

    public void EnablePassThrough()
    {
        SetCollisionMaskValue(PassThroughLayers, false);
    }

    public void DisablePassThrough()
    {
        SetCollisionMaskValue(PassThroughLayers, true);
    }
}
