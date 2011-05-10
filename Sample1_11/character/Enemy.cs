﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_11.core;

namespace Sample1_11.character
{

	/// <summary>
	/// 敵機の情報。
	/// </summary>
	abstract class Enemy
		: ITask
	{

		/// <summary>大きさ。</summary>
		private const float SIZE = 32;

		/// <summary>疑似乱数ジェネレータ。</summary>
		protected static readonly Random rnd = new Random();

		/// <summary>移動速度と方角。</summary>
		protected Vector2 velocity;

		/// <summary>現在座標。</summary>
		private Vector2 position;

		/// <summary>色。</summary>
		private Color color;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		/// <param name="color">色。</param>
		public Enemy(float speed, Color color)
		{
			this.color = color;
			float AROUND_HALF = Game1.SCREEN.Width + Game1.SCREEN.Height;
			float AROUND_HALF_QUARTER = Game1.SCREEN.Width * 2 + Game1.SCREEN.Height;
			int AROUND = (int)AROUND_HALF * 2;
			int p = rnd.Next(AROUND);
			Vector2 pos;
			if (p < Game1.SCREEN.Width || p >= AROUND_HALF &&
				p < AROUND_HALF_QUARTER)
			{
				pos.X = p % Game1.SCREEN.Width;
				pos.Y = p < AROUND_HALF ? 0 : Game1.SCREEN.Height - 1;
			}
			else
			{
				pos.X = p < AROUND_HALF ? 0 : Game1.SCREEN.Width - 1;
				pos.Y = p % Game1.SCREEN.Height;
			}
			position = pos;
			initVelocity(rnd.Next(1, 3) + speed);
		}

		/// <summary>
		/// タスクを開始します。
		/// </summary>
		public void setup()
		{
			// 特にすることはない。
		}

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		public virtual void update()
		{
			position += velocity;
		}

		/// <summary>
		/// 敵機の移動、及び接触判定をします。
		/// </summary>
		/// <returns>接触した場合、true。</returns>
		public bool hitTest()
		{
			const float HITAREA = Player.SIZE * 0.5f + SIZE * 0.5f;
			const float HITAREA_SQUARED = HITAREA * HITAREA;
			bool hit = (HITAREA_SQUARED > Vector2.DistanceSquared(position, Player.instance.position));
			return hit;
		}

		/// <summary>
		/// 1フレーム分の描画を行います。
		/// </summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			const float SCALE = SIZE / Graphics.RECT;
			Vector2 origin = new Vector2(Graphics.RECT * 0.5f);
			graphics.spriteBatch.Draw(graphics.gameThumbnail, position, null, color,
				0f, new Vector2(Graphics.RECT * 0.5f), SCALE, SpriteEffects.None, 0f);
		}

		/// <summary>
		/// 敵機の移動速度と方角を初期化します。
		/// </summary>
		/// <param name="speed">速度。</param>
		protected virtual void initVelocity(float speed)
		{
			Vector2 v = Player.instance.position - position;
			if (v == Vector2.Zero)
			{
				// 長さが0だと単位ベクトル計算時にNaNが出るため対策
				v = Vector2.UnitX;
			}
			v.Normalize();
			velocity = v * speed;
		}
	}
}
