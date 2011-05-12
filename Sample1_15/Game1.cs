using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_15.core;
using Sample1_15.state;
using Sample1_15.state.scene;
using Sample1_15.task;
using Sample1_15.task.entity;
using Sample1_15.task.entity.score;

namespace Sample1_15
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	internal class Game1
		: Game
	{

		/// <summary>��ʋ�`���B</summary>
		internal static readonly Rectangle SCREEN = new Rectangle(0, 0, 800, 600);

		/// <summary>�`�����f�[�^�B</summary>
		private Graphics graphics;

		/// <summary>�V�[���Ǘ��N���X�B</summary>
		private Entity mgrScene = new Entity(SceneTitle.instance);

		/// <summary>�^�X�N�Ǘ��N���X�B</summary>
		private readonly TaskManager<ITask> mgrTask = new TaskManager<ITask>();

		/// <summary>Constructor.</summary>
		internal Game1()
		{
			new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			mgrTask.tasks.AddRange(new ITask[] { KeyStatus.instance, mgrScene, Score.instance });
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
			mgrTask.update();
			if (mgrScene.currentState == StateEmpty.instance)
			{
				mgrTask.reset();
				Exit();
			}
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			graphics.spriteBatch.Begin();
			mgrTask.draw(graphics);
			graphics.spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
