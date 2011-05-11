﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_15.state.chr;

namespace Sample1_15.task.entity.chr
{
	class Character
		: Entity
	{

		/// <summary>判定の大きさ。</summary>
		internal float size;

		/// <summary>現在座標。</summary>
		internal Vector2 position;

		/// <summary>移動速度と方角。</summary>
		internal Vector2 velocity;

		/// <summary>色。</summary>
		internal Color color;

		/// <summary>コンストラクタ。</summary>
		internal Character()
			: base(null)
		{
			reset();
		}

		/// <summary>画面内に収まっているかどうか。</summary>
		internal bool contains
		{
			get
			{
				return Game1.SCREEN.Contains((int)position.X, (int)position.Y);
			}
		}

		/// <summary>オブジェクトをリセットします。</summary>
		public override void reset()
		{
			position = -Vector2.One;
			velocity = Vector2.Zero;
			base.reset();
		}

		/// <summary>
		/// 敵機の接触判定をします。
		/// </summary>
		/// <param name="expr">対象キャラクタ。</param>
		/// <returns>接触した場合、true。</returns>
		internal bool hitTest(Character expr)
		{
			float hitarea = expr.size * 0.5f + size * 0.5f;
			bool hit = ((hitarea * hitarea) > Vector2.DistanceSquared(position, expr.position));
			return hit;
		}

		/// <summary>ダメージを与えます。</summary>
		/// <param name="value">ダメージ値(負数で回復)。</param>
		/// <returns>続行可能な場合、true。</returns>
		internal bool damage(int value)
		{
			bool resullt = true;
			StateCharacter state = currentState as StateCharacter;
			if (state != null)
			{
				resullt = state.damage(this, value);
			}
			return resullt;
		}
	}
}
