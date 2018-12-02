using System;
using System.Collections;

public class ProcessCollection
{
	public ProcessCollection()
	{
        Queue q = new Queue();
	}

    public Process getNextProcess()
    {
        return q.dequeue();
    }

    public void addProcess(Process p1)
    {
        q.enqueue(p);
    }

}
