using System;
using System.Collections.Generic;

// 這是一個通用的優先佇列類別，專為 A* 演算法設計
public class PriorityQueue<T>
{
    // 內部用一個列表來儲存元素
    private List<Tuple<T, float>> elements = new List<Tuple<T, float>>();

    // 回傳佇列中的元素數量
    public int Count
    {
        get { return elements.Count; }
    }

    // 將一個元素以及它的優先級（在 A* 中就是 F-score）加入佇列
    public void Enqueue(T item, float priority)
    {
        elements.Add(Tuple.Create(item, priority));
    }

    // 從佇列中取出並移除優先級最高（即 F-score 最低）的元素
    public T Dequeue()
    {
        int bestIndex = 0;

        // 遍歷所有元素，找出優先級最高的（數值最小的）
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Item2 < elements[bestIndex].Item2)
            {
                bestIndex = i;
            }
        }

        // 獲取該元素
        T bestItem = elements[bestIndex].Item1;
        // 將它從列表中移除
        elements.RemoveAt(bestIndex);
        // 回傳該元素
        return bestItem;
    }
}