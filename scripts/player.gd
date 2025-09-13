extends CharacterBody2D

@export var move_speed: float = 150.0
@export var jump_velocity: float = 600.0
@export var double_jump_velocity: float = 500.0
@export var max_jumps: int = 2
@export var gravity: float = ProjectSettings.get_setting("physics/2d/default_gravity")
@export var camera: Camera2D
@export var animated_sprite: AnimatedSprite2D

var _jump_counter: int = 0
var _is_falling: bool = false
var _is_jumping: bool = false
var _is_double_jumping: bool = false
var _is_jump_cancelled: bool = false
var _is_idling: bool = false
var _is_running: bool = false

func _physics_process(delta: float) -> void:
	process_input(delta)
	camera.position = position

func process_input(delta: float) -> void:
	# Horizontal movement
	var _input_x: float = Input.get_axis("move_left", "move_right");
	velocity.x = _input_x * move_speed

	# Apply gravity
	velocity.y += gravity * delta

	# State checks
	_is_falling = velocity.y > 0.0 and not is_on_floor()
	_is_jumping = Input.is_action_just_pressed("jump") and is_on_floor()
	_is_double_jumping = Input.is_action_just_pressed("jump") and _is_falling
	_is_jump_cancelled = Input.is_action_just_released("jump") and velocity.y < 0.0
	_is_idling = is_equal_approx(velocity.x, 0.0) and is_on_floor()
	_is_running = not is_equal_approx(velocity.x, 0.0) and is_on_floor()

	# Jumping logic
	if _is_jumping:
		_jump_counter += 1
		velocity.y = -jump_velocity
	elif _is_double_jumping:
		_jump_counter += 1
		if _jump_counter <= max_jumps:
			velocity.y = -double_jump_velocity
	elif _is_jump_cancelled:
		velocity.y = 0.0
	elif _is_idling or _is_running:
		_jump_counter = 0

	# Sprite direction
	if not is_zero_approx(velocity.x):
		animated_sprite.flip_h = velocity.x < 0
	else:
		animated_sprite.flip_h = get_global_mouse_position().x < position.x 

	move_and_slide()

	apply_animations()

func apply_animations() -> void:
	if _is_idling:
		animated_sprite.play("idle")
