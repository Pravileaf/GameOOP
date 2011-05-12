﻿using Microsoft.Xna.Framework.Graphics;
using Sample1_15.task.entity;
using Sample1_15.task.entity.chr;

namespace Sample1_15.state.chr
{

	/// <summary>正確に自機を狙う敵機の状態。</summary>
	sealed class StateEnemyStraight
		: StateEnemy
	{

		/// <summary>クラス オブジェクト。</summary>
		internal static readonly StateEnemy normal = new StateEnemyStraight(Color.Red);

		/// <summary>ホーミングした後のオブジェクト。</summary>
		internal static readonly StateEnemy homing =
			new StateEnemyStraight(StateEnemyHoming.instance.color);

		/// <summary>コンストラクタ。</summary>
		/// <param name="color">乗算色。</param>
		private StateEnemyStraight(Color color)
			: base(50, color)
		{
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
	}
}
