namespace PaintingTanks.Managers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Interfaces;
    using PaintingTanks.Entities.MapItems;
    using PaintingTanks.Entities;
    using UnityEngine;

    // Calculates amount of paint
    public class Paint : MonoBehaviour, IValueProvider<ulong>, IValueProvider<Color32>
    {
        [SerializeField] List<PaintableMesh> PaintablePlanes = new List<PaintableMesh>();
        [SerializeField] List<Color32> AllColors = default(List<Color32>);
        [SerializeField] Color32 playerColor;
        [SerializeField] int GroupAmount = 8;
        [SerializeField] List<PaintableMeshAmountTulpsGroup> TulpGroups = new List<PaintableMeshAmountTulpsGroup>();
        [SerializeField] float RefreshRate = 1;

        public event Action<ulong> AmountUpdated;
        public event Action<Color32> ColorUpdated;

        public void SubscribeToProvider(IValueReceiver<ulong> receiver)
        {
            AmountUpdated += receiver.OnChange;
        }

        public void SubscribeToProvider(IValueReceiver<Color32> receiver)
        {
            ColorUpdated += receiver.OnChange;
        }

        void Awake()
        {
            PaintablePlanes.AddRange(FindObjectsOfType<PaintableMesh>());
            FindPlayerColors();
            CreateGroups();
        }

        private void FindPlayerColors()
        {
            AllColors.Add(COLOR_BLACK);
            AllColors.Add(playerColor);
            ColorUpdated?.Invoke(playerColor);
        }

        void Start()
        {
            StartCoroutine(Calculate());
        }

        IEnumerator Calculate()
        {
            foreach (var Group in TulpGroups)
            {
                foreach (var tulp in Group.Tulps) GetColor(tulp);
                yield return new WaitForSeconds(RefreshPerGroupRate);
            }
            yield return new WaitForEndOfFrame();
            AmountUpdated?.Invoke(GlobalPaintAmounts[1].Amount);
            StartCoroutine(Calculate());
        }

        void GetColor(PaintableMeshAmountTulp tulp)
        {
            if (tulp.Mesh.Changed)
            {
                foreach (var paint in tulp.PaintAmounts)
                {
                    paint.StartUpdate();
                    paint.Amount = 0;
                }
                try
                {
                    Color32[] c = tulp.Mesh.Countable.GetPixels32();
                    foreach (var color in c)
                    {
                        foreach (var paint in tulp.PaintAmounts)
                        {
                            var paintColor = paint.Color;
                            if (CheckIfEqualPaints(color, paintColor))
                                paint.Amount++;
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    Debug.Log("Property is null", tulp.Mesh.gameObject);
                    throw new NullReferenceException(e.Message);
                }
                finally
                {
                    foreach (var paint in tulp.PaintAmounts) paint.FinishUpdate();
                    tulp.Mesh.CheckedForPaint();
                }
            }
        }

        PaintAmount[] GlobalPaintAmounts = new PaintAmount[0];
        readonly Color32 COLOR_BLACK = new Color(0, 0, 0, 0);
        float RefreshPerGroupRate;

        void CreateGroups()
        {
            var globalPaintAmounts = new List<PaintAmount>();

            foreach (var color in AllColors)
                globalPaintAmounts.Add(new PaintAmount(color));

            GlobalPaintAmounts = globalPaintAmounts.ToArray();

            int entitiesAmount = PaintablePlanes.Count;
            float amountPerGroupF = entitiesAmount / GroupAmount;
            int amountPerGroup = (int)Math.Ceiling((decimal)amountPerGroupF);
            RefreshPerGroupRate = RefreshRate / GroupAmount;

            for (int j = 0; j < GroupAmount; j++) TulpGroups.Add(new PaintableMeshAmountTulpsGroup());

            int g = 0;
            for (int i = 0; i < entitiesAmount; i++)
            {
                TulpGroups[g].Tulps.Add(CreateTulp(PaintablePlanes[i]));
                if (i != 0 && i % (amountPerGroup + 1) == 0) g++;
            }
        }

        private static bool CheckIfEqualPaints(Color32 color, Color32 paintColor)
        {
            return color.r == paintColor.r &&
                    color.g == paintColor.g &&
                    color.b == paintColor.b &&
                    color.a == paintColor.a;
        }

        PaintableMeshAmountTulp CreateTulp(PaintableMesh mesh)
        {
            var Tulp = new PaintableMeshAmountTulp();
            Tulp.Mesh = mesh;

            var list = new List<LocalPaintAmount>();
            foreach (var Global in GlobalPaintAmounts)
            {
                LocalPaintAmount a = new LocalPaintAmount(Global);
                list.Add(a);
            }
            Tulp.PaintAmounts = list.ToArray();
            return Tulp;
        }
    }
}
