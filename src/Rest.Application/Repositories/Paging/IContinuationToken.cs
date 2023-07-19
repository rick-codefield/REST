namespace Rest.Application.Repositories;

public enum ContinuationTokenType : byte
{
    Offset,
    Cursor,
    Seek,
}

public interface IContinuationToken
{
    ContinuationTokenType Type { get; }
    byte[] Serialize();
}

public static class ContinuationToken
{
    public static IContinuationToken Deserialize(ReadOnlySpan<byte> data)
    {
        switch ((ContinuationTokenType)data[0])
        {
            case ContinuationTokenType.Offset:
            return OffsetContinuationToken.Deserialize(data);

            case ContinuationTokenType.Seek:
            return SeekContinuationToken.Deserialize(data);

            default:
            throw new ArgumentOutOfRangeException();
        }
    }
}