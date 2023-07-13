using System.Runtime.InteropServices;

namespace Rest.Application.Repositories;

public sealed record OffsetContinuationToken(int Offset) : IContinuationToken
{
    public ContinuationTokenType Type => ContinuationTokenType.Offset;

    public byte[] Serialize()
    {
        var data = new byte[5];
        using var stream = new MemoryStream(data);
        using var writer = new BinaryWriter(stream);

        writer.Write((byte)Type);
        writer.Write(Offset);
        return data;
    }

    public static IContinuationToken Deserialize(ReadOnlySpan<byte> data)
    {
        var type = MemoryMarshal.Read<ContinuationTokenType>(data);
        if (type != ContinuationTokenType.Offset)
        {
            throw new ArgumentException("Token type mismatch");
        }

        var offset = MemoryMarshal.Read<int>(data.Slice(1));

        return new OffsetContinuationToken(offset);
    }
}
