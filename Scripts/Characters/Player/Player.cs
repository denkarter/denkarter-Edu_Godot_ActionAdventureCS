using Edu_Godot_ActionAdventureCS.Scripts.General;
using Godot;

namespace Edu_Godot_ActionAdventureCS.Scripts.Characters.Player;

public partial class Player : CharacterBody3D
{
	[ExportGroup("Required Nodes")]
	[Export] private AnimationPlayer _animationPlayerNode;
	public float MoveSpeed = 5f;
	
	private Sprite3D _playerSprite3DNode;
	private Label _labelNode;

	private Vector2 _direction = Vector2.Zero;
	private NodePath _sprite3dNodePath = "Sprite3D";
	private NodePath _sprite3dLabelNodePath = "Sprite3D/LabelNode";

	public override void _Ready()
	{
		_playerSprite3DNode = GetNode<Sprite3D>(_sprite3dNodePath);
		GD.Print(_playerSprite3DNode.Name);
		_labelNode = GetNode<Label>(_sprite3dLabelNodePath);
		GD.Print(_labelNode.Name);
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = (new Vector3(_direction.X, 0, _direction.Y)) * MoveSpeed;
		MoveAndSlide();
		Flip();
	}

	public override void _Input(InputEvent @event)
	{
		_direction = Input.GetVector(GameConstants.InputMoveLeft, GameConstants.InputMoveRight,
			GameConstants.InputMoveForward, GameConstants.InputMoveBackward);

		if (_direction == Vector2.Zero)
		{
			_animationPlayerNode.Play(GameConstants.AnimIdle);
		}
		else
		{
			_animationPlayerNode.Play(GameConstants.AnimMove);
		}
	}

	private void Flip()
	{
		bool isNoMovingHorizontally = Velocity.X == 0;
		if (isNoMovingHorizontally)
			return;
		
		bool isMovingLeft = Velocity.X < 0;
		_playerSprite3DNode.FlipH = isMovingLeft;
	}
}