﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_15.core;
using Sample1_15.task.entity;
using Sample1_15.task.entity.score;

namespace Sample1_15.state.score
{

	/// <summary>スコア全情報を描画する状態。</summary>
	sealed class StateAllScore
		: IState
	{

		/// <summary>クラス オブジェクト。</summary>
		internal static readonly IState instance = new StateAllScore();

		/// <summary>コンストラクタ。</summary>
		private StateAllScore()
		{
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="accessor">隠蔽されたメンバへのアクセサ。</param>
		public void setup(Entity entity, object accessor)
		{
		}

		/// <summary>1フレーム分の更新処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="accessor">隠蔽されたメンバへのアクセサ。</param>
		public void update(Entity entity, object accessor)
		{
		}

		/// <summary>1フレーム分の描画処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="graphics">グラフィック データ。</param>
		/// <param name="accessor">隠蔽されたメンバへのアクセサ。</param>
		public void draw(Entity entity, Graphics graphics, object accessor)
		{
			Score score = (Score)entity;
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"SCORE: " + score.now.ToString(), new Vector2(300, 560), Color.Black);
			StateHiScoreOnly.instance.draw(entity, graphics, accessor);
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="accessor">隠蔽されたメンバへのアクセサ。</param>
		public void teardown(Entity entity, object accessor)
		{
		}
	}
}
