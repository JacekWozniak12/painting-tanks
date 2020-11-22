namespace PaintingTanks.Entities
{
    using System;
    using System.Collections.Generic;
    using PaintingTanks.Entities.MapItems;
    using UnityEngine;

    [Serializable]
    public class PaintBit
    {
        public PaintBit(Vector2Int size, int textureSize, Color32 color)
        {
            Size = size;
            TextureSize = textureSize;
            Color = color;
        }

        public Vector2Int Size = new Vector2Int(1, 1);
        public int TextureSize = 2;
        public Color32 Color = new Color32(255, 255, 255, 1);
    }

    [Serializable]
    public class PaintAmount
    {
        public PaintAmount(Color color) => Color = color;
        public PaintAmount(Color32 color) => Color = color;
        public Color32 Color;
        public ulong Amount = 0;

    }

    [Serializable]
    public class LocalPaintAmount : PaintAmount
    {
        public PaintAmount Global;
        public LocalPaintAmount(PaintAmount global) : base(global.Color) => Global = global;
        public void StartUpdate() => Global.Amount -= Amount;
        public void FinishUpdate() => Global.Amount += Amount;
    }

    [Serializable]
    public class PaintableMeshAmountTulpsGroup
    {
        public List<PaintableMeshAmountTulp> Tulps = new List<PaintableMeshAmountTulp>();
    }

    [Serializable]
    public class PaintableMeshAmountTulp
    {
        public PaintableMesh Mesh;
        public LocalPaintAmount[] PaintAmounts;
    }
}
