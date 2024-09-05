using System;

public interface ISubscribe
{
    public void Subscribe(Action name);

    public void Unsubscribe(Action name);
}