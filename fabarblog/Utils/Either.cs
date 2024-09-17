namespace fabarblog.Utils;

public abstract class Either<L, R>
{
	public abstract bool IsLeft();
	public abstract bool IsRight();
	public abstract object GetValue();
}

#region Left
public sealed class Left<L, R>(L value) : Either<L, R>
{
	public L Value { get; } = value;
	public override object GetValue() => Value!;

	public override bool IsLeft()
	{
		return true;
	}

	public override bool IsRight()
	{
		return false;
	}
}
#endregion

#region Right
public sealed class Right<L, R>(R value) : Either<L, R>
{
	public R Value { get; } = value;
	public override object GetValue() => Value!;

	public override bool IsLeft()
	{
		return false;
	}

	public override bool IsRight()
	{
		return true;
	}
}
#endregion

public static class Either
{
	public static Either<L, R> Instanciate<L, R>(L value)
	{
		return new Left<L, R>(value);
	}

	public static Either<L, R> Instanciate<L, R>(R value)
	{
		return new Right<L, R>(value);
	}
}