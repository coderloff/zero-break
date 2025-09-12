extends Control

@export var levelButtons: Array[Button]

func _ready() -> void:
	for button in levelButtons:
		button.pressed.connect(func(): on_level_button_pressed(button.name))

func on_level_button_pressed(button_name: String) -> void:
	var level_path = "res://scenes/levels/%s.tscn" % button_name
	var level_scene: PackedScene = ResourceLoader.load(level_path)
	if level_scene:
		get_tree().change_scene_to_packed(level_scene)
	else:
		push_error("Could not load level scene at: %s" % level_path)
