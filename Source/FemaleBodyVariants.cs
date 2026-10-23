using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace FemaleBodyVariants;

public class FemaleBodyVariantsMod : Mod
{
    public FemaleBodyVariantsMod(ModContentPack content) : base(content)
    {
        new Harmony("tiagocc0.femalebodyvariants").PatchAll();
    }
}

public static class Variants
{
    static readonly Dictionary<string, bool> exists = new Dictionary<string, bool>();

    // path + "_Female" if a mod has that texture, otherwise null
    public static string FemalePath(string path)
    {
        if (path.NullOrEmpty() || path.EndsWith("_Female"))
        {
            return null;
        }
        string female = path + "_Female";
        if (!exists.TryGetValue(female, out bool found))
        {
            found = ContentFinder<Texture2D>.Get(female + "_south", reportFailure: false) != null;
            exists[female] = found;
        }
        return found ? female : null;
    }

    public static bool IsFemale(Pawn pawn) => pawn?.gender == Gender.Female;
}

// only when the game is using the normal naked body, so races, mutants, creepjoiners and dessicated bodies keep their own graphic
[HarmonyPatch(typeof(PawnRenderNode_Body), nameof(PawnRenderNode_Body.GraphicFor))]
public static class PawnRenderNode_Body_GraphicFor_Patch
{
    public static void Postfix(Pawn pawn, ref Graphic __result)
    {
        if (__result == null || !Variants.IsFemale(pawn))
        {
            return;
        }
        string naked = pawn.story?.bodyType?.bodyNakedGraphicPath;
        if (naked == null || __result.path != naked)
        {
            return;
        }
        string female = Variants.FemalePath(naked);
        if (female != null)
        {
            __result = GraphicDatabase.Get<Graphic_Multi>(female, __result.Shader, __result.drawSize, __result.color, __result.colorTwo);
        }
    }
}

// fur covered bodies (Biotech genes), e.g. FurCovered_Thin_Female
[HarmonyPatch(typeof(FurDef), nameof(FurDef.GetFurBodyGraphicPath))]
public static class FurDef_GetFurBodyGraphicPath_Patch
{
    public static void Postfix(Pawn pawn, ref string __result)
    {
        if (!Variants.IsFemale(pawn))
        {
            return;
        }
        string female = Variants.FemalePath(__result);
        if (female != null)
        {
            __result = female;
        }
    }
}

// body tattoos use the body texture as a mask, so they follow the female body shape too
[HarmonyPatch(typeof(TattooDef), nameof(TattooDef.GraphicFor))]
public static class TattooDef_GraphicFor_Patch
{
    public static void Postfix(TattooDef __instance, Pawn pawn, Color color, ref Graphic __result)
    {
        if (__result == null || __instance.tattooType != TattooType.Body || !Variants.IsFemale(pawn))
        {
            return;
        }
        string female = Variants.FemalePath(pawn.story?.bodyType?.bodyNakedGraphicPath);
        if (female != null)
        {
            __result = GraphicDatabase.Get<Graphic_Multi>(__instance.texPath, __result.Shader, Vector2.one, color, Color.white, null, female);
        }
    }
}
