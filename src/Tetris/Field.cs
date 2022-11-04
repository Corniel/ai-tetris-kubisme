using System.Runtime.CompilerServices;

namespace Tetris;

/// <summary>Represents Testis field.</summary>
public readonly struct Field
{
    public static readonly Field Start = new(Array.Empty<Row>(), 20, 0);

    private Field(Row[] rows, int height, int filled)
    {
        this.rows = rows;
        Height = height;
        Filled = filled;
    }

    private readonly Row[] rows;

    /// <summary>Gets the height of the field.</summary>
    public readonly int Height;

    /// <summary>Gets total of filled rows.</summary>
    public readonly int Filled;
    
    /// <summary>Gets the number of free rows.</summary>
    public int Free => Height - Filled;

    /// <summary>Gets a <see cref="Row"/> based on a (zero based) row number.</summary>
    public Row this[int row] => row >= Filled ? Row.Empty : rows[row];

    /// <summary>Investigates if the block fit.</summary>
    /// <returns>
    /// Returns false if there is overlap.
    /// Else, returns true if solid ground, or retry if not.
    /// </returns>
    [Pure]
    public Fit Fits(Block block)
    {
        // Not enough space.
        if (block.Height > Height) return Fit.False;

        // Nothing to match yet
        if (block.Offset > Filled) return Fit.Maybe;

        // the bottom of the field is a solid floor too.
        var hasFloor = block.Offset == 0;

        var row = block.Offset;
        var end = block.Height;

        while (row < end)
        {
            var block_row = block[row];
            if (block_row.HasOverlapWith(this[row]))
            {
                return Fit.False;
            }
            else if (!hasFloor)
            {
                hasFloor = block_row.HasOverlapWith(this[row - 1]);
            }
            row++;
        }

        return hasFloor ? Fit.True : Fit.Maybe;
    }

    /// <summary>Applies the move described by a <see cref="Block"/> and <see cref="Steps"/>.</summary>
    /// <remarks>
    /// This validates the steps. This is expensive, and only for testing purposes.
    /// </remarks>
    [Pure]
    public Move Move(Block block, Steps steps)
    {
        var fit = Fit.Maybe;

        foreach (var step in steps)
        {
            fit = Fits(block);
            if (fit == Fit.False) { return default; }
            else if (fit == Fit.True) { return Move(block, step.IsRotation()); }
            else if (block.Next(step) is { } next)
            {
                block = next;
            }
            // ignore further steps.
            else break;
        }
        while (fit == Fit.Maybe)
        {
            fit = Fits(block);
            block = block.Down!;
        }
        return Move(block, isRotation: false);
    }

    /// <summary>Applies the move described by a <see cref="Block"/>.</summary>
    [Pure]
    public Move Move(Block block, bool isRotation)
    {
        var source = block.Offset;
        var target = source;
        var moved = new Row[Math.Max(Filled, block.Height)];

        // copy untouched rows
        Array.Copy(rows, moved, source);

        // merge touched rows
        while (source < moved.Length)
        {
            var merge = this[source].Merge(block[source++]);
            if (!merge.IsFull())
            {
                moved[target++] = merge;
            }
         }

        var field = new Field(
            rows: moved,
            height: Height,
            filled: target);

        var cleared = source - target;

        if (cleared == 0)
        {
            return new(Clearing.None, field);
        }
        else if (moved[0].IsEmpty())
        {
            return new(Clearing.Perfect(cleared), field);
        }
        else if (isRotation && TSpin.Is(block, rows))
        {
            return new(Clearing.TSpin(cleared), field);
        }
        else
        {
            return new(new Clearing(cleared), field);
        }
    }
   
    /// <inheritdoc />
    public override string ToString()
    {
        var sb = new StringBuilder();

        for (var h = Height - 1; h >= 0; h--)
        {
            if (h > Filled)
            {
                sb.AppendLine(Row.Empty.ToString());
            }
            else
            {
                sb.AppendLine(this[h].ToString());
            }
        }
        return sb.ToString();
    }

    public static Field New(params ushort[] rows)
    {
        var h = rows.Length;
        var rs = Rows.Trim(rows);
        return new Field(rs, h, rs.Length);
    }

    public static Field Parse(string str)
        => New(str
        .Split(new string[] { "|", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
        .Select(line => Row.Parse(line))
        .ToArray());
}
