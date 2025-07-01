using System.Collections.Generic;

public class SimplePriorityQueue<T>
{
    private readonly List<(T item, float priority)> data = new List<(T, float)>();

    public int Count => data.Count;

    public void Enqueue(T item, float priority)
    {
        data.Add((item, priority));
    }

    public T Dequeue()
    {
        int best = 0;
        for (int i = 1; i < data.Count; i++)
            if (data[i].priority < data[best].priority)
                best = i;
        var val = data[best].item;
        data.RemoveAt(best);
        return val;
    }
}
