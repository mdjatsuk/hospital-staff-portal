using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Data;

public sealed class TestingData : EntityData<TestingData>
{
    public bool Boolean { get; set; }
    public char Char { get; set; }
    public DateTime DateTime { get; set; }
    [Column(TypeName = "decimal(18, 2)")] public decimal Decimal { get; set; }
    public double Double { get; set; }
    public float Float { get; set; }
    public sbyte Int8 { get; set; }
    public short Int16 { get; set; }
    public int Int32 { get; set; }
    public long Int64 { get; set; }
    public string String { get; set; } = string.Empty;
    public byte UInt8 { get; set; }
    public ushort UInt16 { get; set; }
    public uint UInt32 { get; set; }
    public ulong UInt64 { get; set; }
    public bool? NullBoolean { get; set; }
    public char? NullChar { get; set; }
    public DateTime? NullDateTime { get; set; }
    [Column(TypeName = "decimal(18, 2)")] public decimal? NullDecimal { get; set; }
    public double? NullDouble { get; set; }
    public float? NullFloat { get; set; }
    public sbyte? NullInt8 { get; set; }
    public short? NullInt16 { get; set; }
    public int? NullInt32 { get; set; }
    public long? NullInt64 { get; set; }
    public string? NullString { get; set; }
    public byte? NullUInt8 { get; set; }
    public ushort? NullUInt16 { get; set; }
    public uint? NullUInt32 { get; set; }
    public ulong? NullUInt64 { get; set; }
}
