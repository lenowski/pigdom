using Godot;

namespace Pigdom.Actors.BumpingPig.Commands;

public interface ICommand
{
  void Execute();

  void Undo() { }
}

public interface ICommand<T> : ICommand
    where T : Node
{
  T Receiver { get; set; }
}
