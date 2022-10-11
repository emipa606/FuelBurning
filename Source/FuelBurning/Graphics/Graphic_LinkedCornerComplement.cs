using UnityEngine;
using Verse;

namespace FuelBurning;

internal class Graphic_LinkedCornerComplement : Graphic_Linked
{
    private const float slope = 0f;
    private readonly Color32[] colors;

    private readonly Vector2[][] compUVs;
    private readonly float coverOffsetDist;
    private readonly float distCenterCorner;

    private readonly Vector2 size = new Vector2(0.2f, 0.2f);
    private readonly int[] tris;
    private readonly Vector3[][] vecs;
    protected string compTexPath;
    protected Material cornerMat;

    public Graphic_LinkedCornerComplement()
    {
        distCenterCorner = new Vector2(0.5f, 0.5f).magnitude;
        coverOffsetDist = distCenterCorner - (size.magnitude * 0.5f);
        vecs = new[]
        {
            new[]
            {
                new Vector3(0.5f * size.x, 0f, -0.5f * size.y),
                new Vector3(-0.5f * size.x, 0f, -0.5f * size.y),
                new Vector3(-0.5f * size.x, slope, 0.5f * size.y)
            },
            new[]
            {
                new Vector3(-0.5f * size.x, 0f, -0.5f * size.y),
                new Vector3(-0.5f * size.x, slope, 0.5f * size.y),
                new Vector3(0.5f * size.x, slope, 0.5f * size.y)
            },
            new[]
            {
                new Vector3(-0.5f * size.x, slope, 0.5f * size.y),
                new Vector3(0.5f * size.x, slope, 0.5f * size.y),
                new Vector3(0.5f * size.x, 0f, -0.5f * size.y)
            },
            new[]
            {
                new Vector3(0.5f * size.x, slope, 0.5f * size.y),
                new Vector3(0.5f * size.x, 0f, -0.5f * size.y),
                new Vector3(-0.5f * size.x, 0f, -0.5f * size.y)
            }
        };
        compUVs = new[]
        {
            new[]
            {
                new Vector2(0.5f, 0f),
                new Vector2(0f, 0f),
                new Vector2(0f, 1f)
            },
            new[]
            {
                new Vector2(0.5f, 0f),
                new Vector2(0.5f, 1f),
                new Vector2(1f, 1f)
            },
            new[]
            {
                new Vector2(0f, 1f),
                new Vector2(0.5f, 1f),
                new Vector2(0.5f, 0f)
            },
            new[]
            {
                new Vector2(1f, 1f),
                new Vector2(1f, 0f),
                new Vector2(0.5f, 0f)
            }
        };
        tris = new[] { 0, 1, 2 };
        colors = new[]
        {
            new Color32(255, 255, 255, 255),
            new Color32(255, 255, 255, 255),
            new Color32(255, 255, 255, 255)
        };
    }

    public override void Init(GraphicRequest req)
    {
        path = req.path;
        subGraphic = GraphicDatabase.Get<Graphic_Single>(req.path, ShaderDatabase.Transparent);
        compTexPath = $"{req.path}_Comp";
        cornerMat = LoadCompMaterial(compTexPath);
    }

    public override void Print(SectionLayer layer, Thing thing, float extraRotation)
    {
        base.Print(layer, thing, extraRotation);
        var position = thing.Position;
        for (var i = 0; i < 4; i++)
        {
            var c = thing.Position + GenAdj.DiagonalDirectionsAround[i];
            if (ShouldLinkWith(c, thing))
            {
                continue;
            }

            var unused = thing.DrawPos +
                         (GenAdj.DiagonalDirectionsAround[i].ToVector3().normalized * coverOffsetDist);
            if (CheckLinkedFourWays(position, thing, i))
            {
                Printer_Mesh.PrintMesh(layer, default, GetCornerMesh(compUVs[i], i), cornerMat);
            }
        }
    }

    protected bool CheckLinkedFourWays(IntVec3 position, Thing thing, int i)
    {
        switch (i)
        {
            case 0 when !ShouldLinkWith(position + IntVec3.West, thing) ||
                        !ShouldLinkWith(position + IntVec3.South, thing):
            case 1 when !ShouldLinkWith(position + IntVec3.West, thing) ||
                        !ShouldLinkWith(position + IntVec3.North, thing):
            case 2 when !ShouldLinkWith(position + IntVec3.East, thing) ||
                        !ShouldLinkWith(position + IntVec3.North, thing):
                return false;
        }

        return i != 3 || ShouldLinkWith(position + IntVec3.East, thing) &&
            ShouldLinkWith(position + IntVec3.South, thing);
    }

    protected Material LoadCompMaterial(string localPath)
    {
        var mat = MaterialPool.MatFrom(localPath, ShaderDatabase.Transparent);
        if (mat != null)
        {
            return mat;
        }

        Log.Error($"Could not found the texture. path \"{compTexPath}\"");
        mat = BaseContent.BadMat;

        return mat;
    }

    protected Mesh GetCornerMesh(Vector2[] uvs, int direction)
    {
        var mesh = new Mesh { name = "ComplementMesh", vertices = vecs[direction], uv = uvs, colors32 = colors };
        mesh.SetTriangles(tris, 0);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}