using System;
using Godot;

namespace ZeroBreak
{
    public partial class Player : CharacterBody2D
    {
        [Export] private float moveSpeed = 150.0f;
        [Export] private float jumpVelocity = 600.0f;
        [Export] private float doubleJumpVelocity = 500.0f;
        [Export] private int maxJumps = 2;
        [Export] private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
        [Export] private Camera2D camera;
        [Export] private AnimatedSprite2D animatedSprite;
        
        private Vector2 _velocity = Vector2.Zero;
        private int _jumpCounter;
        private bool _isFalling;
        private bool _isJumping;
        private bool _isDoubleJumping;
        private bool _isJumpCancelled;
        private bool _isIdling;
        private bool _isRunning;

        public override void _PhysicsProcess(double delta)
        {
            ProcessInput(delta);

            camera.Position = Position;
        }

        private void ProcessInput(double delta)
        {
            _velocity = Velocity;

            // Horizontal movement
            _velocity.X = Input.GetAxis("move_left", "move_right") * moveSpeed;
            
            // Apply gravity
            _velocity.Y += gravity * (float)delta;

            // Set variables
            _isFalling = _velocity.Y > 0.0 && !IsOnFloor();
            _isJumping = Input.IsActionJustPressed("jump") && IsOnFloor();
            _isDoubleJumping = Input.IsActionJustPressed("jump") && _isFalling;
            _isJumpCancelled = Input.IsActionJustReleased("jump") && _velocity.Y < 0.0;
            _isIdling = Mathf.IsZeroApprox(_velocity.X) && IsOnFloor();
            _isRunning = !Mathf.IsZeroApprox(_velocity.X) && IsOnFloor();
            
            if (_isJumping)
            {
                _jumpCounter++;
                _velocity.Y = -jumpVelocity;
            }
            else if (_isDoubleJumping)
            {
                _jumpCounter++;
                if (_jumpCounter <= maxJumps)
                {
                    _velocity.Y = -doubleJumpVelocity;
                }
            }
            else if (_isJumpCancelled) _velocity.Y = 0.0f;
            else if (_isIdling || _isRunning) _jumpCounter = 0;

            if (!Mathf.IsZeroApprox(_velocity.X))
            {
                animatedSprite.FlipH = _velocity.X < 0;
            }
            
            Velocity = _velocity;
            MoveAndSlide();

            ApplyAnimations();
        }

        private void ApplyAnimations()
        {
            if (_isIdling) animatedSprite.Play("idle");
        }
    }
}
