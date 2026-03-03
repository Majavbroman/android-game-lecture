using System.Collections.Generic;
using UnityEngine;

public static class QueueExtensions
{
    public static void EnqueueFront<T>(this Queue<T> queue, T item)
    {
        var tempArray = queue.ToArray();
        queue.Clear();
        queue.Enqueue(item);
        foreach (var element in tempArray){
            queue.Enqueue(element);
        }
    }
}
