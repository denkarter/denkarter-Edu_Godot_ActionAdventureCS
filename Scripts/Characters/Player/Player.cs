using Godot;

namespace Edu_Godot_ActionAdventureCS.Scripts.Characters.Player;

public partial class Player : CharacterBody3D
{
	[ExportGroup("Required Nodes")]
	[Export] private AnimationPlayer _animationPlayerNode;
	private Sprite3D _playerSprite3DNode;
	private Label _labelNode;
	public float MoveSpeed = 5f;
	
	private StringName _negativeX = "MoveLeft";
	private StringName _positiveX = "MoveRight";
	private StringName _negativeY = "MoveForward";
	private StringName _positiveY = "MoveBackward";
	private Vector2 _direction = Vector2.Zero;
	private StringName _idleAnimation = "Idle";
	private StringName _moveAnimation = "Move";

	public override void _Ready()
	{
		_playerSprite3DNode = GetNode<Sprite3D>("Sprite3D");
		GD.Print(_playerSprite3DNode.Name);
		_labelNode = GetNode<Label>("Sprite3D/LabelNode");
		GD.Print(_labelNode.Name);
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = (new Vector3(_direction.X, 0, _direction.Y)) * MoveSpeed;
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		_direction = Input.GetVector(_negativeX, _positiveX,
			_negativeY, _positiveY);

		if (_direction == Vector2.Zero)
		{
			_animationPlayerNode.Play(_idleAnimation);
		}
		else
		{
			_animationPlayerNode.Play(_moveAnimation);
		}
	}
}