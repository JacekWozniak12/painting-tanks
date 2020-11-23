namespace PaintingTanks.Managers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Interfaces;
    using Library;
    using Entities.MapItems;
    using Entities;
    using UnityEngine;

    // Calculates amount of paint
    public class Paint : MonoBehaviour, IValueProvider<ulong>, IValueProvider<Color32>
    {
        [SerializeField] List<PaintableMesh> PaintablePlanes = new List<PaintableMesh>();
        [SerializeField] List<Color32> AllColors = default(List<Color32>);
        [SerializeField] Color32 playerColor;
        [SerializeField] int GroupAmount = 1;
        [SerializeField] List<ObjectPaintGroups> TulpGroups = new List<ObjectPaintGroups>();
        [SerializeField] float RefreshRate = 1;

        void Awake()
        {
            FindObjects();
            FindColors();
            CreateGroups();
        }

        private void FindObjects()
        {
            PaintablePlanes.AddRange(FindObjectsOfType<PaintableMesh>());
        }

        private void FindColors()
        {
            AllColors.Add(ALPHA_COLOR);
            AllColors.Add(playerColor);
            ColorUpdated?.Invoke(playerColor);
        }

        private void Start() => StartCoroutine(Calculate());

        private IEnumerator Calculate()
        {
            foreach (var Group in TulpGroups)
            {
                foreach (var tulp in Group.Tulps) HandleObject(tulp);
                yield return new WaitForSeconds(RefreshPerGroupRate);
            }
            yield return new WaitForEndOfFrame();
            AmountUpdated?.Invoke(GlobalPaintAmounts[1].Amount);
            StartCoroutine(Calculate());
        }

        private void HandleObject(ObjectPaint tulp)
        {
            if (tulp.Mesh.Changed)
            {
                if (tulp.Mesh.TextureSize < 256) CheckAllAtOnce(tulp);
                else CheckModifiedParts(tulp);
            }
        }

        private static void CheckModifiedParts(ObjectPaint tulp)
        {
            var L = tulp.Mesh.GetChangedPartsOfCountableTexture();
            tulp.Mesh.ClearChangedPartsToCheck();
            foreach (var item in L)
            {
                PreCheck(tulp, item);
                print("t");
                try
                {
                    CheckObject(tulp, item);
                }
                catch (NullReferenceException e)
                {
                    Debug.Log("Property is null", tulp.Mesh.gameObject);
                    throw new NullReferenceException(e.Message);
                }
                finally
                {
                    PostCheck(tulp, item);
                    print("f");
                }
            }
        }

        private static void CheckAllAtOnce(ObjectPaint tulp)
        {
            try
            {
                PreCheck(tulp);
                CheckObject(tulp);
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Property is null", tulp.Mesh.gameObject);
                throw new NullReferenceException(e.Message);
            }
            finally
            {
                PostCheck(tulp);
            }
        }

        private static void CheckObject(ObjectPaint tulp, TexturePartInfo item = null)
        {
            Color32[] textureToCheck;

            if (item != null)
            {
                var temporary = tulp.Mesh.Countable.GetPixels32();
                textureToCheck = GraphicsL.GetPartOfArray(
                    temporary, tulp.Mesh.Countable.height,
                    item.Start.x, item.Start.y, item.Finish.x, item.Finish.y);
            }
            else
                textureToCheck = tulp.Mesh.Countable.GetPixels32();

            foreach (var colorToCheck in textureToCheck)
            {
                foreach (var checkFor in tulp.PaintAmounts)
                {
                    if (GraphicsL.CheckIfEqualColors(colorToCheck, checkFor.Color))
                        checkFor.Amount++;
                }
            }
        }

        private static void PreCheck(ObjectPaint tulp, TexturePartInfo item = null)
        {
            foreach (var paint in tulp.PaintAmounts)
            {
                paint.StartUpdate();
                if (item == null) paint.Amount = 0;
                else paint.Amount -= (ulong)item.GetSize();
            }
        }

        private static void PostCheck(ObjectPaint tulp, TexturePartInfo item = null)
        {
            foreach (var paint in tulp.PaintAmounts)
            {
                if (item != null) paint.Amount += (ulong)item.GetSize();
                paint.FinishUpdate();
            }
            tulp.Mesh.CheckedForPaint();
        }

        [SerializeField]
        PaintAmount[] GlobalPaintAmounts = new PaintAmount[0];
        readonly Color32 ALPHA_COLOR = new Color(0, 0, 0, 0);
        float RefreshPerGroupRate;

        void CreateGroups()
        {
            var globalPaintAmounts = new List<PaintAmount>();
            foreach (var color in AllColors) globalPaintAmounts.Add(new PaintAmount(color));

            GlobalPaintAmounts = globalPaintAmounts.ToArray();

            int entitiesAmount = PaintablePlanes.Count;

            float amountPerGroupF = entitiesAmount / GroupAmount;

            int amountPerGroup = (int)Math.Ceiling((decimal)amountPerGroupF);

            RefreshPerGroupRate = RefreshRate / GroupAmount;

            for (int j = 0; j < GroupAmount; j++) TulpGroups.Add(new ObjectPaintGroups());

            int g = 0;

            for (int i = 0; i < entitiesAmount; i++)
            {
                TulpGroups[g].Tulps.Add(CreateTulp(PaintablePlanes[i]));
                if (i != 0 && i % (amountPerGroup + 1) == 0) g++;
            }
        }


        private ObjectPaint CreateTulp(PaintableMesh mesh)
        {
            var Tulp = new ObjectPaint();
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
    }
}
