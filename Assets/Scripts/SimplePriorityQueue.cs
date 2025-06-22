using System.Collections.Generic;

public class SimplePriorityQueue<T>
{
    // 存所有的點與它的 priority
    private List<KeyValuePair<T, float>> elements = new List<KeyValuePair<T, float>>();

    public int Count => elements.Count;

    // Enqueue，把 (item, priority) 加進去
    public void Enqueue(T item, float priority)
    {
        elements.Add(new KeyValuePair<T, float>(item, priority));
    }

    // Dequeue，取出 priority 最小的那個
    public T Dequeue()
    {
        int bestIndex = 0;
        float bestPriority = elements[0].Value;

        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].Value < bestPriority)
            {
                bestPriority = elements[i].Value;
                bestIndex = i;
            }
        }

        T bestItem = elements[bestIndex].Key;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }
}
