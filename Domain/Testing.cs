using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Domain;

public sealed class Testing(TestingData? d) : Entity<TestingData>(d)
{
    public bool Boolean => data?.Boolean ?? false;
    public char Char => data?.Char ?? '?';
    public DateTime DateTime => data?.DateTime ?? DateTime.MinValue;
    public decimal Decimal => data?.Decimal ?? 0;
    public double Double => data?.Double ?? 0;
    public float Float => data?.Float ?? 0;
    public sbyte Int8 => data?.Int8 ?? 0;
    public short Int16 => data?.Int16 ?? 0;
    public int Int32 => data?.Int32 ?? 0;
    public long Int64 => data?.Int64 ?? 0;
    public string String => data?.String ?? string.Empty;
    public byte UInt8 => data?.UInt8 ?? 0;
    public ushort UInt16 => data?.UInt16 ?? 0;
    public uint UInt32 => data?.UInt32 ?? 0;
    public ulong UInt64 => data?.UInt64 ?? 0;
    public bool? NullBoolean => data?.NullBoolean;
    public char? NullChar => data?.NullChar;
    public DateTime? NullDateTime => data?.NullDateTime;
    public decimal? NullDecimal => data?.NullDecimal;
    public double? NullDouble => data?.NullDouble;
    public float? NullFloat => data?.NullFloat;
    public sbyte? NullInt8 => data?.NullInt8;
    public short? NullInt16 => data?.NullInt16;
    public int? NullInt32 => data?.NullInt32;
    public long? NullInt64 => data?.NullInt64;
    public string? NullString => data?.NullString;
    public byte? NullUInt8 => data?.NullUInt8;
    public ushort? NullUInt16 => data?.NullUInt16;
    public uint? NullUInt32 => data?.NullUInt32;
    public ulong? NullUInt64 => data?.NullUInt64;
}
