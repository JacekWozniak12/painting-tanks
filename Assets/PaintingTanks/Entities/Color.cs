namespace PaintingTanks.Entities
{
    using System;
    using System.Collections.Generic;
    using PaintingTanks.Entities.MapItems;
    using UnityEngine;

    [Serializable]
    public class PaintAmount
    {
        public PaintAmount(Color32 color) => Color = color;
        public Color32 Color;
        public ulong MaxAmount = 0;
        public ulong Amount = 0;
    }

    [Serializable]
    public class LocalPaintAmount : PaintAmount
    {
        public PaintAmount Global;
        public LocalPaintAmount(PaintAmount global) : base(global.Color) => Global = global;

        public void StartUpdate() => StartUpdate(Amount);
        public void StartUpdate(ulong amount) => Global.Amount -= amount;

        public void FinishUpdate() => FinishUpdate(Amount);
        public void FinishUpdate(ulong amount) => Global.Amount += amount;
    }

    [Serializable]
    public class ObjectPaintGroups { public List<ObjectPaint> Tulps = new List<ObjectPaint>(); }

    [Serializable]
    public class ObjectPaint
    {
        public PaintableMesh Mesh;
        public LocalPaintAmount[] PaintAmounts;
    }
}
