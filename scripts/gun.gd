extends Node2D

@export var sprite: Sprite2D

func _process(delta: float) -> void:
	sprite.flip_v = get_global_position().x > get_global_mouse_position().x
	look_at(get_global_mouse_position())
