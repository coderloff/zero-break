extends Control

@export var campaign_button: Button
@export var options_button: Button
@export var exit_button: Button
@export var level_scene: PackedScene

func _ready() -> void:
	Input.set_mouse_mode(Input.MOUSE_MODE_HIDDEN)

	campaign_button.grab_focus()
	campaign_button.pressed.connect(func(): load_scene(level_scene))
	exit_button.pressed.connect(func(): get_tree().quit())

func load_scene(scene: PackedScene) -> void:
	get_tree().change_scene_to_packed(scene)
