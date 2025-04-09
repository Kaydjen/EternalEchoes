using ProceduralGeneration.Algorithm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class Grid2Int : IEnumerable<Vector2Int>
{
    private HashSet<Vector2Int> set = new HashSet<Vector2Int>();
    public UnityEvent changed = new UnityEvent();

    private Vector2Int max = new Vector2Int(int.MinValue, int.MinValue), min = new Vector2Int(int.MaxValue, int.MaxValue);
    private void UpdateMin(in Vector2Int v) => min = Vector2Int.Min(min, v);
    private void UpdateMax(in Vector2Int v) => max = Vector2Int.Max(max, v);
    private void Update(in Vector2Int v)
    {
        UpdateMin(v);
        UpdateMax(v);
    }
    private void Update()
    {
        for (int i = 0; i < Count; i++)
            Update(set.ElementAt(i));

        changed.Invoke();
    }
  
    public void Add(in Vector2Int v)
    {
        int count = Count;

        set.Add(v);
        if (Count != count)
        {
            Update(v);
            changed.Invoke();
        }
    }
    public void Add(in Grid2Int other)
    {
        int count = Count;

        set.UnionWith(other.set);
        if (count != Count)
        {
            Update(other.min);
            Update(other.max);
            changed.Invoke();
        }
    }

    static public Vector2Int ToGridPosition(Vector3 position, float? scale)
    {
        if (scale == null) return Vector2Int.zero;
        if (scale <= 0f) return Vector2Int.zero;

        Vector3 positionV3 = position / (float)scale;
        return Vector2Int.RoundToInt(new Vector2(positionV3.x, positionV3.z));
    }

    public bool TryParseToGridPosition(in Vector3 position, float? scale) => Contains(ToGridPosition(position, scale));

    public bool TryParseToGridPosition(in Vector3 position, float? scale, out Vector2Int parsedPosition)
    {
        parsedPosition = ToGridPosition(position, scale);

        return Contains(parsedPosition);
    }

    public Vector2Int Min { get { return min; } }
    public Vector2Int Max { get { return max; } }

    public int Count { get => set.Count; }
    public Vector2Int Find(Predicate<Vector2Int> match) => set.ToList().Find(match);
    public List<Vector2Int> FindAll(Predicate<Vector2Int> match) => set.ToList().FindAll(match);

    public bool Contains(in Vector2Int v) => set.Contains(v);
    public List<Vector2Int> GetNeighbours(in Vector2Int position)
    {
        if (!set.Contains(position)) return null;

        List<Vector2Int> neighbours = new List<Vector2Int>(), directions = Directions.directions.Keys.ToList();

        for (int i = 0; i < directions.Count; i++)
            if (set.Contains(directions[i] + position)) neighbours.Add(directions[i] + position);

        return neighbours;
    }
    public List<Vector2Int> GetNeighboursDirections(in Vector2Int position)
    {
        if (!set.Contains(position)) return null;

        List<Vector2Int> neighbours = new List<Vector2Int>(), directions = Directions.directions.Keys.ToList();

        for (int i = 0; i < directions.Count; i++)
            if (set.Contains(directions[i] + position)) neighbours.Add(directions[i]);

        return neighbours;
    }

    public List<Vector2Int> GetComplexNeighbours(in Vector2Int position)
    {
        if (!set.Contains(position)) return null;

        List<Vector2Int> neighbours = new List<Vector2Int>(), directions = Directions.complexDirections.Keys.ToList();

        for (int i = 0; i < directions.Count; i++)
            if (set.Contains(directions[i] + position)) neighbours.Add(directions[i] + position);

        return neighbours;
    }
    public List<Vector2Int> GetComplexNeighboursDirections(in Vector2Int position)
    {
        if (!set.Contains(position)) return null;

        List<Vector2Int> neighbours = new List<Vector2Int>(), directions = Directions.complexDirections.Keys.ToList();

        for (int i = 0; i < directions.Count; i++)
            if (set.Contains(directions[i] + position)) neighbours.Add(directions[i]);

        return neighbours;
    }
    public static Grid2Int operator+ (Grid2Int lhs, in Grid2Int rhs)
    {
        lhs.Add(rhs);
        return lhs;
    }
    public static Grid2Int operator+ (Grid2Int lhs, in Vector2Int vector2Int)
    {
        lhs.Add(vector2Int);
        return lhs;
    }
    public static Grid2Int operator- (Grid2Int lhs, in Grid2Int rhs)
    {
        int count = lhs.Count;

        lhs.set.ExceptWith(rhs.set);
        if (lhs.Count != count) lhs.Update();
        return lhs;
    }
    public static Grid2Int operator- (Grid2Int lhs, in Vector2Int vector2Int)
    {
        int count = lhs.Count;

        lhs.set.Remove(vector2Int);
        if (lhs.Count != count) lhs.Update();
        return lhs;
    }

    public Vector2Int this[in int key]
    {
        get => set.ElementAt(key);
    }
    public IEnumerator<Vector2Int> GetEnumerator() => set.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    public Grid2Int() { }
    public Grid2Int(in HashSet<Vector2Int> set) {
       
        if (set != null) this.set = set;
        Update();
    }
    static public Grid2Int StringToGrid(in string layoutString, in Vector2Int absolutePosition = default)
    {
        Grid2Int grid = new Grid2Int();
        string[] layoutArray = layoutString.Replace("\r", "").Split('\n');

        for (int y = 0; y < layoutArray.Length; y++)
        {
            for (int x = 0; x < layoutArray[y].Length; x++)
            {
                if (layoutArray[y][x] != '■') continue;
                grid += new Vector2Int(x, y) + absolutePosition;
            }
        }

        return grid;
    }
    static public Grid2Int LayoutArrayToGrid(in List<string> layoutArray, in Vector2Int absolutePosition = default)
    {
        Grid2Int layout = new Grid2Int();

        for (int y = 0; y < layoutArray.Count; y++)
        {
            for (int x = 0; x < layoutArray[y].Length; x++)
            {
                if (layoutArray[y][x] != '■') continue;

                layout += new Vector2Int(x, y) + absolutePosition;
            }
        }

        return layout;
    }
    static public Grid2Int LayoutArrayToGrid(in List<string> layoutArray)
    {
        Grid2Int layout = new Grid2Int();

        for (int y = 0; y < layoutArray.Count; y++)
        {
            for (int x = 0; x < layoutArray[y].Length; x++)
            {
                if (layoutArray[y][x] != '■') continue;

                layout += new Vector2Int(x, y);
            }
        }

        return layout;
    }
}
