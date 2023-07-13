using System.Runtime.InteropServices;

namespace Rest.Application.Repositories;

public sealed record SeekContinuationToken(int Id) : IContinuationToken
{
    public ContinuationTokenType Type => ContinuationTokenType.Seek;

    public byte[] Serialize()
    {
        var data = new byte[5];
        using var stream = new MemoryStream(data);
        using var writer = new BinaryWriter(stream);

        writer.Write((byte)Type);
        writer.Write(Id);
        return data;
    }

    public static IContinuationToken Deserialize(ReadOnlySpan<byte> data)
    {
        var type = MemoryMarshal.Read<ContinuationTokenType>(data);
        if (type != ContinuationTokenType.Seek)
        {
            throw new ArgumentException("Token type mismatch");
        }

        var id = MemoryMarshal.Read<int>(data.Slice(1));

        return new SeekContinuationToken(id);
    }
}