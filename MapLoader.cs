using Godot;
using System;

public partial class MapLoader : Node3D
{

    private Node3D? CurrentMap;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Right now MapLoader just loads placeholder map
		//TODO: Remove this call once things are good to go
		LoadMap("PlaceholderMap");
	}


	public Node3D LoadMap(string MapName)
	{
		if(!(CurrentMap == null))
		{
            CurrentMap.QueueFree();
        }
		
		PackedScene scene = ResourceLoader.Load<PackedScene>("res://Maps/" + MapName + "/" + MapName + ".tscn");

		CurrentMap = scene.Instantiate<Node3D>();

		AddChild(CurrentMap);

		return CurrentMap;
	}
}
