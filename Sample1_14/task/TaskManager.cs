﻿using System.Collections.Generic;
using Sample1_14.core;

namespace Sample1_14.task
{

	/// <summary>
	/// タスク管理クラス。
	/// </summary>
	class TaskManager<T>
		: ITask
		where T : ITask
	{

		/// <summary>タスク一覧。</summary>
		public readonly List<T> tasks = new List<T>();

		/// <summary>タスクをリセットします。</summary>
		public void release()
		{
			int length = tasks.Count;
			for (int i = 0; i < length; i++)
			{
				tasks[i].release();
			}
		}

		/// <summary>1フレーム分の更新を行います。</summary>
		public void update()
		{
			int length = tasks.Count;
			for (int i = 0; i < length; i++)
			{
				tasks[i].update();
			}
		}

		/// <summary>描画します。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			int length = tasks.Count;
			for (int i = 0; i < length; i++)
			{
				tasks[i].draw(graphics);
			}
		}
	}
}
