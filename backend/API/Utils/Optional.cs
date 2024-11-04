namespace API.Utils
{
  public class Optional<T>
  {
    public T Value { get; private set; }
    public bool HasValue { get; private set; }

    private Optional(T value, bool hasValue)
    {
      Value = value;
      HasValue = hasValue;
    }

    public static Optional<T> Create(T value)
    {
      return new Optional<T>(value, value != null);
    }

    public static Optional<T> Empty()
    {
      return new Optional<T>(default(T), false);
    }
  }
}
