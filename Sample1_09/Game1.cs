using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sample1_09
{

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1
		: Game
	{

		/// <summary>��ʋ�`���B</summary>
		public static readonly Rectangle SCREEN = new Rectangle(0, 0, 800, 600);

		/// <summary>�Q�[�������ǂ����B</summary>
		private bool game;

		/// <summary>�Q�[���̐i�s�J�E���^�B</summary>
		private int counter;

		/// <summary>�`�����f�[�^�B</summary>
		private Graphics graphics;

		/// <summary>�L�[���͊Ǘ��N���X�B</summary>
		private readonly KeyState mgrInput = new KeyState();

		/// <summary>�X�R�A �f�[�^�B</summary>
		private readonly Score score = new Score();

		/// <summary>�G�@�ꗗ�f�[�^�B</summary>
		private readonly Enemies enemies = new Enemies();

		/// <summary>���@�f�[�^�B</summary>
		private readonly Player player = new Player();

		/// <summary>�^�C�g����ʂ̃^�X�N�ꗗ�B</summary>
		private readonly ITask[] taskTitle;

		/// <summary>�Q�[���v���C��ʂ̃^�X�N�ꗗ�B</summary>
		private readonly ITask[] taskGame;

		/// <summary>
		/// Constructor.
		/// </summary>
		public Game1()
		{
			new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			taskTitle = new ITask[] { score, mgrInput };
			taskGame = new ITask[] { enemies, player, score, mgrInput };
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			graphics = new Graphics(this);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			KeyboardState keyState = mgrInput.keyboardState;
			ITask[] tasks = game ? taskGame : taskTitle;
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i].update(keyState);
			}
			if (game)
			{
				createEnemy();
				if (enemies.hitTest(player.position))
				{
					game = player.miss();
					score.drawNowScore = game;
				}
				counter++;
			}
			else
			{
				updateTitle(keyState);
			}
			base.Update(gameTime);
		}

		/// <summary>
		/// �G�@���쐬���܂��B
		/// </summary>
		private void createEnemy()
		{
			if (counter % (int)MathHelper.Max(60 - counter * 0.01f, 1) == 0 &&
				enemies.create(player.position, counter * 0.001f) &&
				score.add(10))
			{
				player.extend();
			}
		}

		/// <summary>
		/// �^�C�g����ʂ��X�V���܂��B
		/// </summary>
		/// <param name="keyState">���݂̃L�[���͏�ԁB</param>
		private void updateTitle(KeyboardState keyState)
		{
			if (keyState.IsKeyDown(Keys.Escape))
			{
				Exit();
			}
			if (keyState.IsKeyDown(Keys.Space))
			{
				// �Q�[���J�n
				game = true;
				counter = 0;
				for (int i = 0; i < taskGame.Length; i++)
				{
					taskGame[i].reset();
				}
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			graphics.spriteBatch.Begin();
			ITask[] tasks = game ? taskGame : taskTitle;
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i].draw(graphics);
			}
			if (!game)
			{
				drawTitle();
			}
			graphics.spriteBatch.End();
			base.Draw(gameTime);
		}

		/// <summary>
		/// �^�C�g����ʂ�`�悵�܂��B
		/// </summary>
		private void drawTitle()
		{
			graphics.spriteBatch.DrawString(
				graphics.spriteFont, "SAMPLE 1", new Vector2(200, 100),
				Color.Black, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0f);
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"PUSH SPACE KEY.", new Vector2(340, 400), Color.Black);
		}
	}
}
