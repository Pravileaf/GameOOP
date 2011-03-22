using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sample1_07
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
		bool game;

		/// <summary>�Q�[���̐i�s�J�E���^�B</summary>
		int counter;

		/// <summary>�`�����f�[�^�B</summary>
		Graphics graphics;

		/// <summary>�X�R�A �f�[�^�B</summary>
		Score score = new Score();

		/// <summary>�G�@�ꗗ�f�[�^�B</summary>
		Enemies enemies = new Enemies();

		/// <summary>���@�f�[�^�B</summary>
		Player player = new Player();

		/// <summary>HUD�\���̂��߂̃N���X�B</summary>
		HUD hud;

		/// <summary>
		/// Constructor.
		/// </summary>
		public Game1()
		{
			new GraphicsDeviceManager(this);
			hud = new HUD(player, score);
			Content.RootDirectory = "Content";
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
			KeyboardState keyState = Keyboard.GetState();
			if (game)
			{
				player.move(keyState);
				createEnemy();
				if (enemies.moveAndHitTest(player.position))
				{
					game = --player.amount >= 0;
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
				player.amount++;
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
				Point center = SCREEN.Center;
				player.position = new Vector2(center.X, center.Y);
				counter = 0;
				score.reset();
				player.amount = Player.DEFAULT_AMOUNT;
				enemies.reset();
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
			if (game)
			{
				drawGame();
			}
			else
			{
				drawTitle();
			}
			hud.draw(graphics, game);
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

		/// <summary>
		/// �Q�[����ʂ�`�悵�܂��B
		/// </summary>
		private void drawGame()
		{
			player.draw(graphics);
			enemies.draw(graphics);
		}
	}
}
