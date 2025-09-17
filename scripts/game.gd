extends Node2D

@export var cursorSprite: Resource

func _ready() -> void:
	Input.set_mouse_mode(Input.MouseMode.MOUSE_MODE_VISIBLE)
	Input.set_custom_mouse_cursor(cursorSprite)
	
	