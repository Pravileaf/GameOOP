﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sample1_15.core;
using Sample1_15.task;
using Sample1_15.task.entity;
using Sample1_15.task.entity.chr;

namespace Sample1_15.state.chr
{

	/// <summary>自機の状態。</summary>
	sealed class StatePlayer
		: StateCharacter
	{

		/// <summary>大きさ。</summary>
		private const float SIZE = 64;

		/// <summary>移動速度。</summary>
		private const float SPEED = 3;

		/// <summary>自機の初期残機。</summary>
		private const int DEFAULT_AMOUNT = 2;

		/// <summary>クラス オブジェクト。</summary>
		internal static readonly IState instance = new StatePlayer();

		/// <summary>入力を受け付けるキー一覧。</summary>
		private readonly Keys[] acceptInputKeyList;

		/// <summary>キー入力に対応した移動方向。</summary>
		private readonly Dictionary<Keys, Vector2> velocity;

		/// <summary>初期位置。</summary>
		private readonly Vector2 defaultPosition;

		/// <summary>ミス猶予(残機)数。</summary>
		private int m_amount;

		/// <summary>ミス猶予(残機)数の文字列による表現。</summary>
		private string m_amountString;

		/// <summary>コンストラクタ。</summary>
		private StatePlayer()
		{
			acceptInputKeyList =
				new Keys[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right };
			velocity = new Dictionary<Keys, Vector2>();
			velocity.Add(Keys.Up, new Vector2(0, -SPEED));
			velocity.Add(Keys.Down, new Vector2(0, SPEED));
			velocity.Add(Keys.Left, new Vector2(-SPEED, 0));
			velocity.Add(Keys.Right, new Vector2(SPEED, 0));
			Point center = Game1.SCREEN.Center;
			defaultPosition = new Vector2(center.X, center.Y);
		}

		/// <summary>ミス猶予(残機)数。</summary>
		private int amount
		{
			get
			{
				return m_amount;
			}
			set
			{
				m_amount = value;
				m_amountString = value < 0 ? string.Empty : new string('*', value);
			}
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="accessor">隠蔽されたメンバへのアクセサ。</param>
		public override void setup(Entity entity, object accessor)
		{
			Character chr = (Character)entity;
			Character.Accessor writer = (Character.Accessor)accessor;
			chr.color = Color.White;
			writer.size = SIZE;
			writer.position = defaultPosition;
			amount = DEFAULT_AMOUNT;
		}

		/// <summary>1フレーム分の更新処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="accessor">隠蔽されたメンバへのアクセサ。</param>
		public override void update(Entity entity, object accessor)
		{
			Character chr = (Character)entity;
			Character.Accessor writer = (Character.Accessor)accessor;
			chr.velocity = createVelocity();
			Vector2 prev = chr.position;
			base.update(entity, accessor);
			if (!chr.contains)
			{
				writer.position = prev;
				chr.velocity = Vector2.Zero;
			}
		}

		/// <summary>1フレーム分の描画処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="graphics">グラフィック データ。</param>
		/// <param name="accessor">隠蔽されたメンバへのアクセサ。</param>
		public override void draw(Entity entity, Graphics graphics, object accessor)
		{
			Character chr = (Character)entity;
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"PLAYER: " + m_amountString, new Vector2(600, 560), Color.Black);
			base.draw(entity, graphics, accessor);
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="accessor">隠蔽されたメンバへのアクセサ。</param>
		public override void teardown(Entity entity, object accessor)
		{
		}

		/// <summary>ダメージを与えます。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="value">ダメージ値(負数で回復)。</param>
		/// <returns>続行可能な場合、true。</returns>
		public override bool damage(Character entity, int value)
		{
			if (value > 0)
			{
				entity.position = defaultPosition;
			}
			amount -= value;
			return amount >= 0;
		}

		/// <summary>キー入力から進行方向を計算します。</summary>
		/// <returns>進行方向。</returns>
		private Vector2 createVelocity()
		{
			KeyboardState keyState = KeyStatus.instance.keyboardState;
			Vector2 vel = Vector2.Zero;
			for (int i = 0; i < acceptInputKeyList.Length; i++)
			{
				Keys key = acceptInputKeyList[i];
				if (keyState.IsKeyDown(key))
				{
					vel += velocity[key];
				}
			}
			return vel;
		}
	}
}
