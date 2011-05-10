﻿using Microsoft.Xna.Framework.Graphics;
using Sample1_14.task.entity;
using Sample1_14.task.entity.chr;

namespace Sample1_14.state.chr
{

	/// <summary>自機をめがけホーミングする敵機の状態。</summary>
	sealed class StateEnemyHoming
		: StateEnemy
	{

		/// <summary>ホーミング時間。</summary>
		private const int HOMING_LIMIT = 60;

		/// <summary>クラス オブジェクト。</summary>
		public static readonly StateEnemy instance = new StateEnemyHoming();

		/// <summary>コンストラクタ。</summary>
		private StateEnemyHoming()
			: base(50, Color.Yellow)
		{
		}

		/// <summary>
		/// 1フレーム分の更新を行う時に呼び出されます。
		/// </summary>
		/// <param name="entity">対象の敵機。</param>
		public override void update(Entity entity)
		{
			if (entity.counter >= HOMING_LIMIT)
			{
				entity.nextState = StateEnemyStraight.homing;
			}
			initVelocity(entity, ((Character)entity).velocity.Length());
			base.update(entity);
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public override void teardown(Entity entity)
		{
		}
	}
}
